// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using flucto.Graphics.Primitives;
using flucto.Graphics.Textures;

namespace flucto.Text
{
    /// <summary>
    /// A <see cref="ITexturedCharacterGlyph"/> provided as final output from a <see cref="TextBuilder"/>.
    /// </summary>
    public struct TextBuilderGlyph : ITexturedCharacterGlyph
    {
        public Texture Texture => Glyph.Texture;
        public float XOffset => ((fixedWidth - Glyph.Width) / 2 ?? Glyph.XOffset) * textSize;
        public float YOffset => Glyph.YOffset * textSize;
        public float XAdvance => (fixedWidth ?? Glyph.XAdvance) * textSize;
        public float Width => Glyph.Width * textSize;
        public float Height => Glyph.Height * textSize;
        public char Character => Glyph.Character;

        public readonly ITexturedCharacterGlyph Glyph;

        /// <summary>
        /// The rectangle for the character to be drawn in.
        /// </summary>
        public RectangleF DrawRectangle { get; internal set; }

        /// <summary>
        /// Whether this is the first character on a new line.
        /// </summary>
        public bool OnNewLine { get; internal set; }

        private readonly float textSize;
        private readonly float? fixedWidth;

        internal TextBuilderGlyph(ITexturedCharacterGlyph glyph, float textSize, float? fixedWidth = null)
        {
            this = default;
            this.textSize = textSize;
            this.fixedWidth = fixedWidth;

            Glyph = glyph;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetKerning<T>(T lastGlyph)
            where T : ICharacterGlyph
            => fixedWidth != null ? 0 : Glyph.GetKerning(lastGlyph);
    }
}
