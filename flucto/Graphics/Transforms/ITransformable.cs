// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Allocation;
using flucto.Timing;

namespace flucto.Graphics.Transforms
{
    public interface ITransformable
    {
        InvokeOnDisposal BeginDelayedSequence(double delay, bool recursive = false);

        InvokeOnDisposal BeginAbsoluteSequence(double newTransformStartTime, bool recursive = false);

        /// <summary>
        /// The current frame's time as observed by this class's <see cref="Transform"/>s.
        /// </summary>
        FrameTimeInfo Time { get; }

        double TransformStartTime { get; }

        void AddTransform(Transform transform, ulong? customTransformID = null);

        void RemoveTransform(Transform toRemove);
    }
}
