// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Input
{
    public struct JoystickAxis
    {
        public readonly int Axis;
        public readonly float Value;

        public JoystickAxis(int axis, float value)
        {
            Axis = axis;
            Value = value;
        }
    }
}
