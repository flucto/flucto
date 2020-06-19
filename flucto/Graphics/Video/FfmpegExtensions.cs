// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using FFmpeg.AutoGen;

namespace flucto.Graphics.Video
{
    internal static class FfmpegExtensions
    {
        internal static double GetValue(this AVRational rational) => rational.num / (double)rational.den;
    }
}
