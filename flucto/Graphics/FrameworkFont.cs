// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.Sprites;

namespace flucto.Graphics
{
    public static class FrameworkFont
    {
        public static FontUsage Condensed => new FontUsage("RobotoCondensed", weight: "Regular");

        public static FontUsage Regular => new FontUsage("Roboto", weight: "Regular");
    }
}
