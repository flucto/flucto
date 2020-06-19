// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace flucto.Testing.Drawables.Steps
{
    public class SingleStepButton : StepButton
    {
        public new Action Action;

        public SingleStepButton()
        {
            base.Action = () =>
            {
                Action?.Invoke();
                Success();
            };
        }
    }
}
