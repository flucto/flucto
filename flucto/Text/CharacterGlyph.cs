// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using flucto.IO.Stores;

namespace flucto.Text
{
    public sealed class CharacterGlyph : ICharacterGlyph
    {
        public float XOffset { get; }
        public float YOffset { get; }
        public float XAdvance { get; }
        public char Character { get; }

        private readonly IGlyphStore containingStore;

        public CharacterGlyph(char character, float xOffset, float yOffset, float xAdvance, [CanBeNull] IGlyphStore containingStore)
        {
            this.containingStore = containingStore;

            Character = character;
            XOffset = xOffset;
            YOffset = yOffset;
            XAdvance = xAdvance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetKerning<T>(T lastGlyph)
            where T : ICharacterGlyph
            => containingStore?.GetKerning(lastGlyph.Character, Character) ?? 0;
    }
}
