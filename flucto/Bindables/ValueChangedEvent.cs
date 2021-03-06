// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Bindables
{
    /// <summary>
    /// An event fired when a <see cref="Bindable{T}.Value"/> changes.
    /// </summary>
    /// <typeparam name="T">The type of bindable.</typeparam>
    public class ValueChangedEvent<T>
    {
        /// <summary>
        /// The old value.
        /// </summary>
        public readonly T OldValue;

        /// <summary>
        /// The new (and current) value.
        /// </summary>
        public readonly T NewValue;

        public ValueChangedEvent(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
