// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace flucto.Graphics.UserInterface
{
    public class DropdownMenuItem<T> : MenuItem
    {
        public readonly T Value;

        public DropdownMenuItem(string text, T value)
            : base(text)
        {
            Value = value;
        }

        public DropdownMenuItem(string text, T value, Action action)
            : base(text, action)
        {
            Value = value;
        }
    }
}
