// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.Textures;

namespace flucto.Graphics.Lines
{
    public class TexturedPath : Path
    {
        public new Texture Texture
        {
            get => base.Texture;
            set => base.Texture = value;
        }
    }
}
