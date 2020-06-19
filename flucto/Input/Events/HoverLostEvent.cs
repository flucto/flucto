﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Input.States;

namespace flucto.Input.Events
{
    /// <summary>
    /// An event representing the end of mouse hover.
    /// Triggered when mouse cursor moved out of a drawable.
    /// </summary>
    public class HoverLostEvent : MouseEvent
    {
        public HoverLostEvent(InputState state)
            : base(state)
        {
        }
    }
}
