// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using JetBrains.Annotations;
using flucto.Bindables;

namespace flucto.Localisation
{
    /// <summary>
    /// An <see cref="IBindable{T}"/> which has its value set based on the user's unicode preference.
    /// </summary>
    public interface IUnicodeBindableString : IBindable<string>
    {
        /// <summary>
        /// The text to use if unicode can be displayed. Can be null, in which case <see cref="NonUnicodeText"/> will be used.
        /// </summary>
        string UnicodeText
        {
            [CanBeNull]
            set;
        }

        /// <summary>
        /// The text to use if unicode should not be displayed. Can be null, in which case <see cref="UnicodeText"/> will be used.
        /// </summary>
        string NonUnicodeText
        {
            [CanBeNull]
            set;
        }
    }
}
