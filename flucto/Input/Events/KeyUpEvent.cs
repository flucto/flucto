// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Input.States;
using osuTK.Input;

namespace flucto.Input.Events
{
    /// <summary>
    /// An event representing a release of a keyboard key.
    /// </summary>
    public class KeyUpEvent : KeyboardEvent
    {
        public KeyUpEvent(InputState state, Key key)
            : base(state, key)
        {
        }
    }
}
