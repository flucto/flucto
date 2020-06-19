// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;

namespace flucto.Input.States
{
    public class JoystickState
    {
        public readonly ButtonStates<JoystickButton> Buttons = new ButtonStates<JoystickButton>();
        public readonly List<JoystickAxis> Axes = new List<JoystickAxis>();
    }
}
