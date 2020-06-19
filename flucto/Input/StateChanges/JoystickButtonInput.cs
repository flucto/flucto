// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using flucto.Input.States;

namespace flucto.Input.StateChanges
{
    public class JoystickButtonInput : ButtonInput<JoystickButton>
    {
        public JoystickButtonInput(IEnumerable<ButtonInputEntry<JoystickButton>> entries)
            : base(entries)
        {
        }

        public JoystickButtonInput(JoystickButton button, bool isPressed)
            : base(button, isPressed)
        {
        }

        public JoystickButtonInput(ButtonStates<JoystickButton> current, ButtonStates<JoystickButton> previous)
            : base(current, previous)
        {
        }

        protected override ButtonStates<JoystickButton> GetButtonStates(InputState state) => state.Joystick.Buttons;
    }
}
