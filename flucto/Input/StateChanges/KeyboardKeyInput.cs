// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using flucto.Input.States;
using osuTK.Input;

namespace flucto.Input.StateChanges
{
    public class KeyboardKeyInput : ButtonInput<Key>
    {
        public KeyboardKeyInput(IEnumerable<ButtonInputEntry<Key>> entries)
            : base(entries)
        {
        }

        public KeyboardKeyInput(Key button, bool isPressed)
            : base(button, isPressed)
        {
        }

        public KeyboardKeyInput(ButtonStates<Key> current, ButtonStates<Key> previous)
            : base(current, previous)
        {
        }

        protected override ButtonStates<Key> GetButtonStates(InputState state) => state.Keyboard.Keys;
    }
}
