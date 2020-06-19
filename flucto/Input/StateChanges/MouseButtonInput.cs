// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using flucto.Input.States;
using osuTK.Input;

namespace flucto.Input.StateChanges
{
    public class MouseButtonInput : ButtonInput<MouseButton>
    {
        public MouseButtonInput(IEnumerable<ButtonInputEntry<MouseButton>> entries)
            : base(entries)
        {
        }

        public MouseButtonInput(MouseButton button, bool isPressed)
            : base(button, isPressed)
        {
        }

        public MouseButtonInput(ButtonStates<MouseButton> current, ButtonStates<MouseButton> previous)
            : base(current, previous)
        {
        }

        protected override ButtonStates<MouseButton> GetButtonStates(InputState state) => state.Mouse.Buttons;
    }
}
