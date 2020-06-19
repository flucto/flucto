// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Graphics.UserInterface;

namespace flucto.Bindables
{
    /// <summary>
    /// A bindable which holds a reference to a bound target, allowing switching between targets and handling unbind/rebind.
    /// </summary>
    /// <typeparam name="T">The type of our stored <see cref="Bindable{T}.Value"/>.</typeparam>
    public class BindableWithCurrent<T> : Bindable<T>, IHasCurrentValue<T>
    {
        private Bindable<T> currentBound;

        public Bindable<T> Current
        {
            get => this;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (currentBound != null) UnbindFrom(currentBound);
                BindTo(currentBound = value);
            }
        }
    }
}
