// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Audio
{
    public interface IHasPitchAdjust
    {
        /// <summary>
        /// The pitch this track is playing at, relative to original.
        /// </summary>
        double PitchAdjust { get; set; }
    }
}
