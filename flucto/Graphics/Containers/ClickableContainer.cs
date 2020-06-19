// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Bindables;
using flucto.Input.Events;

namespace flucto.Graphics.Containers
{
    public class ClickableContainer : Container
    {
        private Action action;

        public Action Action
        {
            get => action;
            set
            {
                action = value;
                Enabled.Value = action != null;
            }
        }

        public readonly BindableBool Enabled = new BindableBool();

        protected override bool OnClick(ClickEvent e)
        {
            if (Enabled.Value)
                Action?.Invoke();
            return true;
        }
    }
}
