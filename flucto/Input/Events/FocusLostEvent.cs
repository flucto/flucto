// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Input.States;

namespace flucto.Input.Events
{
    /// <summary>
    /// An event represeting that a drawable lost the focus.
    /// </summary>
    public class FocusLostEvent : UIEvent
    {
        public FocusLostEvent(InputState state)
            : base(state)
        {
        }
    }
}
