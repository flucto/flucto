// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Graphics.Cursor
{
    /// <summary>
    /// Implementing this interface allows the implementing <see cref="Drawable"/> to display a tooltip if it is the child of a <see cref="TooltipContainer"/>. The tooltip used is
    /// dependent on the implementation of the <see cref="TooltipContainer.CreateTooltip"/> method of the <see cref="TooltipContainer"/> containing this <see cref="Drawable"/>.
    /// </summary>
    public interface IHasTooltip : ITooltipContentProvider
    {
        /// <summary>
        /// Tooltip text that shows when hovering the drawable.
        /// </summary>
        string TooltipText { get; }
    }
}
