// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Input.States;
using osuTK;

namespace flucto.Input.StateChanges.Events
{
    public class MousePositionChangeEvent : InputStateChangeEvent
    {
        /// <summary>
        /// The last mouse position.
        /// </summary>
        public readonly Vector2 LastPosition;

        public MousePositionChangeEvent(InputState state, IInput input, Vector2 lastPosition)
            : base(state, input)
        {
            LastPosition = lastPosition;
        }
    }
}
