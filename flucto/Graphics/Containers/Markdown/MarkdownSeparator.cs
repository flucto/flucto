// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Allocation;
using flucto.Graphics.Shapes;
using osuTK.Graphics;

namespace flucto.Graphics.Containers.Markdown
{
    /// <summary>
    /// Visualises a horizontal separator.
    /// </summary>
    /// <code>
    /// ---
    /// </code>
    public class MarkdownSeparator : CompositeDrawable
    {
        public MarkdownSeparator()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = CreateSeparator();
        }

        protected virtual Drawable CreateSeparator() => new Box
        {
            RelativeSizeAxes = Axes.X,
            Height = 1,
            Colour = Color4.Gray,
        };
    }
}
