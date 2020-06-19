// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Input.States
{
    public class InputState
    {
        public readonly MouseState Mouse;
        public readonly KeyboardState Keyboard;
        public readonly JoystickState Joystick;

        public InputState(MouseState mouse = null, KeyboardState keyboard = null, JoystickState joystick = null)
        {
            Mouse = mouse ?? new MouseState();
            Keyboard = keyboard ?? new KeyboardState();
            Joystick = joystick ?? new JoystickState();
        }
    }
}
