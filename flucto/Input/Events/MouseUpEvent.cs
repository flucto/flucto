﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Input.States;
using osuTK;
using osuTK.Input;

namespace flucto.Input.Events
{
    /// <summary>
    /// An event representing a release of a mouse button.
    /// </summary>
    public class MouseUpEvent : MouseButtonEvent
    {
        public MouseUpEvent(InputState state, MouseButton button, Vector2? screenSpaceMouseDownPosition = null)
            : base(state, button, screenSpaceMouseDownPosition)
        {
        }
    }
}
