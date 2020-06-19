// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Graphics.Cursor
{
    /// <summary>
    /// A tooltip which provides a custom delay until it appears, override the <see cref="TooltipContainer"/>-wide default.
    /// </summary>
    public interface IHasAppearDelay : ITooltipContentProvider
    {
        /// <summary>
        /// The delay until the tooltip should be displayed.
        /// </summary>
        double AppearDelay { get; }
    }
}
