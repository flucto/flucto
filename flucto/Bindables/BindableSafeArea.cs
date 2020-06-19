// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics;

namespace flucto.Bindables
{
    /// <summary>
    /// A subclass of <see cref="Bindable{MarginPadding}"/> specifically for representing the "safe areas" of a device.
    /// It exists to prevent regular <see cref="MarginPadding"/>s from being globally cached.
    /// </summary>
    public class BindableSafeArea : Bindable<MarginPadding>
    {
        public BindableSafeArea(MarginPadding value = default)
            : base(value)
        {
        }
    }
}
