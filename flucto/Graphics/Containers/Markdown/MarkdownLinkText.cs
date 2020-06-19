// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Markdig.Syntax.Inlines;
using flucto.Allocation;
using flucto.Graphics.Cursor;
using flucto.Graphics.Sprites;
using flucto.Platform;
using osuTK.Graphics;

namespace flucto.Graphics.Containers.Markdown
{
    /// <summary>
    /// Visualises a link.
    /// </summary>
    /// <code>
    /// [link text](url)
    /// </code>
    public class MarkdownLinkText : CompositeDrawable, IHasTooltip, IMarkdownTextComponent
    {
        public string TooltipText => Url;

        [Resolved]
        private IMarkdownTextComponent parentTextComponent { get; set; }

        [Resolved]
        private GameHost host { get; set; }

        private readonly string text;

        protected readonly string Url;

        public MarkdownLinkText(string text, LinkInline linkInline)
        {
            this.text = text;
            Url = linkInline.Url ?? string.Empty;

            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            SpriteText spriteText;
            InternalChildren = new Drawable[]
            {
                new ClickableContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Child = spriteText = CreateSpriteText(),
                    Action = OnLinkPressed,
                }
            };

            spriteText.Text = text;
        }

        protected virtual void OnLinkPressed() => host.OpenUrlExternally(Url);

        public virtual SpriteText CreateSpriteText()
        {
            var spriteText = parentTextComponent.CreateSpriteText();
            spriteText.Colour = Color4.DodgerBlue;
            return spriteText;
        }
    }
}
