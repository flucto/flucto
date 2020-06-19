// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.Sprites;

namespace flucto.Graphics.Containers.Markdown
{
    public interface IMarkdownTextComponent
    {
        /// <summary>
        /// Creates a <see cref="SpriteText"/> to display text within this <see cref="IMarkdownTextFlowComponent"/>.
        /// </summary>
        /// <remarks>
        /// The <see cref="SpriteText"/> defined by the <see cref="MarkdownContainer"/> is used by default,
        /// but may be overridden via this method to provide additional styling local to this <see cref="IMarkdownTextFlowComponent"/>.
        /// </remarks>
        /// <returns>The <see cref="SpriteText"/>.</returns>
        SpriteText CreateSpriteText();
    }
}
