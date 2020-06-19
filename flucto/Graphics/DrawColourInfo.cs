// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Graphics.Colour;
using osuTK.Graphics;

namespace flucto.Graphics
{
    public struct DrawColourInfo : IEquatable<DrawColourInfo>
    {
        public ColourInfo Colour;
        public BlendingParameters Blending;

        public DrawColourInfo(ColourInfo? colour = null, BlendingParameters? blending = null)
        {
            Colour = colour ?? ColourInfo.SingleColour(Color4.White);
            Blending = blending ?? BlendingParameters.Inherit;
        }

        public bool Equals(DrawColourInfo other) => Colour.Equals(other.Colour) && Blending.Equals(other.Blending);
    }
}
