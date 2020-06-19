// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Input.StateChanges.Events;
using flucto.Input.States;

namespace flucto.Input.StateChanges
{
    /// <summary>
    /// An object which can handle <see cref="InputState"/> changes.
    /// </summary>
    public interface IInputStateChangeHandler
    {
        /// <summary>
        /// Handles an input state change event.
        /// </summary>
        void HandleInputStateChange(InputStateChangeEvent inputStateChange);
    }
}
