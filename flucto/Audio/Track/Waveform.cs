// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ManagedBass;
using osuTK;
using flucto.MathUtils;
using flucto.Audio.Callbacks;

namespace flucto.Audio.Track
{
    /// <summary>
    /// Procsses audio sample data such that it can then be consumed to generate waveform plots of the audio.
    /// </summary>
    public class Waveform : IDisposable
    {
        /// <summary>
        /// <see cref="Point"/>s are initially generated to a 1ms resolution to cover most use cases.
        /// </summary>
        private const float resolution = 0.001f;

        /// <summary>
        /// The data stream is iteratively decoded to provide this many points per iteration so as to not exceed BASS's internal buffer size.
        /// </summary>
        private const int points_per_iteration = 100000;

        /// <summary>
        /// FFT1024 gives ~40hz accuracy.
        /// </summary>
        private const DataFlags fft_samples = DataFlags.FFT1024;

        /// <summary>
        /// Number of bins generated by the FFT. Must correspond to <see cref="fft_samples"/>.
        /// </summary>
        private const int fft_bins = 512;

        /// <summary>
        /// Minimum frequency for low-range (bass) frequencies. Based on lower range of bass drum fallout.
        /// </summary>
        private const double low_min = 20;

        /// <summary>
        /// Minimum frequency for mid-range frequencies. Based on higher range of bass drum fallout.
        /// </summary>
        private const double mid_min = 100;

        /// <summary>
        /// Minimum frequency for high-range (treble) frequencies.
        /// </summary>
        private const double high_min = 2000;

        /// <summary>
        /// Maximum frequency for high-range (treble) frequencies. A sane value.
        /// </summary>
        private const double high_max = 12000;

        private int channels;
        private List<Point> points = new List<Point>();

        private readonly CancellationTokenSource cancelSource = new CancellationTokenSource();
        private readonly Task readTask;

        private FileCallbacks fileCallbacks;

        /// <summary>
        /// Constructs a new <see cref="Waveform"/> from provided audio data.
        /// </summary>
        /// <param name="data">The sample data stream. If null, an empty waveform is constructed.</param>
        public Waveform(Stream data)
        {
            if (data == null) return;

            readTask = Task.Run(() =>
            {
                // for the time being, this code cannot run if there is no bass device available.
                if (Bass.CurrentDevice <= 0)
                    return;

                fileCallbacks = new FileCallbacks(new DataStreamFileProcedures(data));

                int decodeStream = Bass.CreateStream(StreamSystem.NoBuffer, BassFlags.Decode | BassFlags.Float, fileCallbacks.Callbacks, fileCallbacks.Handle);

                Bass.ChannelGetInfo(decodeStream, out ChannelInfo info);

                long length = Bass.ChannelGetLength(decodeStream);

                // Each "point" is generated from a number of samples, each sample contains a number of channels
                int samplesPerPoint = (int)(info.Frequency * resolution * info.Channels);

                int bytesPerPoint = samplesPerPoint * TrackBass.BYTES_PER_SAMPLE;

                points.Capacity = (int)(length / bytesPerPoint);

                // Each iteration pulls in several samples
                int bytesPerIteration = bytesPerPoint * points_per_iteration;
                var sampleBuffer = new float[bytesPerIteration / TrackBass.BYTES_PER_SAMPLE];

                // Read sample data
                while (length > 0)
                {
                    length = Bass.ChannelGetData(decodeStream, sampleBuffer, bytesPerIteration);
                    int samplesRead = (int)(length / TrackBass.BYTES_PER_SAMPLE);

                    // Each point is composed of multiple samples
                    for (int i = 0; i < samplesRead; i += samplesPerPoint)
                    {
                        // Channels are interleaved in the sample data (data[0] -> channel0, data[1] -> channel1, data[2] -> channel0, etc)
                        // samplesPerPoint assumes this interleaving behaviour
                        var point = new Point(info.Channels);

                        for (int j = i; j < i + samplesPerPoint; j += info.Channels)
                        {
                            // Find the maximum amplitude for each channel in the point
                            for (int c = 0; c < info.Channels; c++)
                                point.Amplitude[c] = Math.Max(point.Amplitude[c], Math.Abs(sampleBuffer[j + c]));
                        }

                        // BASS may provide unclipped samples, so clip them ourselves
                        for (int c = 0; c < info.Channels; c++)
                            point.Amplitude[c] = Math.Min(1, point.Amplitude[c]);

                        points.Add(point);
                    }
                }

                Bass.ChannelSetPosition(decodeStream, 0);
                length = Bass.ChannelGetLength(decodeStream);

                // Read FFT data
                float[] bins = new float[fft_bins];
                int currentPoint = 0;
                long currentByte = 0;

                while (length > 0)
                {
                    length = Bass.ChannelGetData(decodeStream, bins, (int)fft_samples);
                    currentByte += length;

                    double lowIntensity = computeIntensity(info, bins, low_min, mid_min);
                    double midIntensity = computeIntensity(info, bins, mid_min, high_min);
                    double highIntensity = computeIntensity(info, bins, high_min, high_max);

                    // In general, the FFT function will read more data than the amount of data we have in one point
                    // so we'll be setting intensities for all points whose data fits into the amount read by the FFT
                    // We know that each data point required sampleDataPerPoint amount of data
                    for (; currentPoint < points.Count && currentPoint * bytesPerPoint < currentByte; currentPoint++)
                    {
                        points[currentPoint].LowIntensity = lowIntensity;
                        points[currentPoint].MidIntensity = midIntensity;
                        points[currentPoint].HighIntensity = highIntensity;
                    }
                }

                channels = info.Channels;
            }, cancelSource.Token);
        }

        private double computeIntensity(ChannelInfo info, float[] bins, double startFrequency, double endFrequency)
        {
            int startBin = (int)(fft_bins * 2 * startFrequency / info.Frequency);
            int endBin = (int)(fft_bins * 2 * endFrequency / info.Frequency);

            startBin = MathHelper.Clamp(startBin, 0, bins.Length);
            endBin = MathHelper.Clamp(endBin, 0, bins.Length);

            double value = 0;
            for (int i = startBin; i < endBin; i++)
                value += bins[i];
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Waveform"/> containing a specific number of data points by selecting the average value of each sampled group.
        /// </summary>
        /// <param name="pointCount">The number of points the resulting <see cref="Waveform"/> should contain.</param>
        /// <param name="cancellationToken">The token to cancel the task.</param>
        /// <returns>An async task for the generation of the <see cref="Waveform"/>.</returns>
        public async Task<Waveform> GenerateResampledAsync(int pointCount, CancellationToken cancellationToken = default)
        {
            if (pointCount < 0) throw new ArgumentOutOfRangeException(nameof(pointCount));

            if (pointCount == 0 || readTask == null)
                return new Waveform(null);

            await readTask;

            return await Task.Run(() =>
            {
                var generatedPoints = new List<Point>();
                float pointsPerGeneratedPoint = (float)points.Count / pointCount;

                // Determines at which width (relative to the resolution) our smoothing filter is truncated.
                // Should not effect overall appearance much, except when the value is too small.
                // A gaussian contains almost all its mass within its first 3 standard deviations,
                // so a factor of 3 is a very good choice here.
                const int kernel_width_factor = 3;

                int kernelWidth = (int)(pointsPerGeneratedPoint * kernel_width_factor) + 1;

                float[] filter = new float[kernelWidth + 1];

                for (int i = 0; i < filter.Length; ++i)
                {
                    filter[i] = (float)Blur.EvalGaussian(i, pointsPerGeneratedPoint);
                }

                for (float i = 0; i < points.Count; i += pointsPerGeneratedPoint)
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    int startIndex = (int)i - kernelWidth;
                    int endIndex = (int)i + kernelWidth;

                    var point = new Point(channels);
                    float totalWeight = 0;

                    for (int j = startIndex; j < endIndex; j++)
                    {
                        if (j < 0 || j >= points.Count) continue;

                        float weight = filter[Math.Abs(j - startIndex - kernelWidth)];
                        totalWeight += weight;

                        for (int c = 0; c < channels; c++)
                            point.Amplitude[c] += weight * points[j].Amplitude[c];
                        point.LowIntensity += weight * points[j].LowIntensity;
                        point.MidIntensity += weight * points[j].MidIntensity;
                        point.HighIntensity += weight * points[j].HighIntensity;
                    }

                    // Means
                    for (int c = 0; c < channels; c++)
                        point.Amplitude[c] /= totalWeight;
                    point.LowIntensity /= totalWeight;
                    point.MidIntensity /= totalWeight;
                    point.HighIntensity /= totalWeight;

                    generatedPoints.Add(point);
                }

                return new Waveform(null)
                {
                    points = generatedPoints,
                    channels = channels
                };
            }, cancellationToken);
        }

        /// <summary>
        /// Gets all the points represented by this <see cref="Waveform"/>.
        /// </summary>
        public List<Point> GetPoints() => GetPointsAsync().Result;

        /// <summary>
        /// Gets all the points represented by this <see cref="Waveform"/>.
        /// </summary>
        public async Task<List<Point>> GetPointsAsync()
        {
            if (readTask == null)
                return points;

            await readTask;
            return points;
        }

        /// <summary>
        /// Gets the number of channels represented by each <see cref="Point"/>.
        /// </summary>
        public int GetChannels() => GetChannelsAsync().Result;

        /// <summary>
        /// Gets the number of channels represented by each <see cref="Point"/>.
        /// </summary>
        public async Task<int> GetChannelsAsync()
        {
            if (readTask == null)
                return channels;

            await readTask;
            return channels;
        }

        #region Disposal

        ~Waveform()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            isDisposed = true;

            cancelSource?.Cancel();
            cancelSource?.Dispose();
            points = null;

            fileCallbacks?.Dispose();
            fileCallbacks = null;
        }

        #endregion

        /// <summary>
        /// Represents a singular point of data in a <see cref="Waveform"/>.
        /// </summary>
        public class Point
        {
            /// <summary>
            /// An array of amplitudes, one for each channel.
            /// </summary>
            public readonly float[] Amplitude;

            /// <summary>
            /// Unnormalised total intensity of the low-range (bass) frequencies.
            /// </summary>
            public double LowIntensity;

            /// <summary>
            /// Unnormalised total intensity of the mid-range frequencies.
            /// </summary>
            public double MidIntensity;

            /// <summary>
            /// Unnormalised total intensity of the high-range (treble) frequencies.
            /// </summary>
            public double HighIntensity;

            /// <summary>
            /// Cconstructs a <see cref="Point"/>.
            /// </summary>
            /// <param name="channels">The number of channels that contain data.</param>
            public Point(int channels)
            {
                Amplitude = new float[channels];
            }
        }
    }
}
