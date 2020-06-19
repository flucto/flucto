﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osuTK;

namespace flucto.Graphics.Cursor
{
    /// <summary>
    /// A tooltip that can be used in conjunction with a <see cref="TooltipContainer"/> and/or <see cref="IHasCustomTooltip"/> implementation.
    /// </summary>
    public interface ITooltip : IDrawable
    {
        /// <summary>
        /// Set new content be displayed on this tooltip.
        /// </summary>
        /// <param name="content">The content to be displayed.</param>
        /// <returns>Whether this <see cref="ITooltip"/> can display the provided content.</returns>
        bool SetContent(object content);

        /// <summary>
        /// Moves the tooltip to the given position. May use easing.
        /// </summary>
        /// <param name="pos">The position the tooltip should be moved to.</param>
        void Move(Vector2 pos);
    }
}
