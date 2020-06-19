// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Bindables
{
    /// <summary>
    /// Interface for objects that have a description.
    /// </summary>
    public interface IHasDescription
    {
        /// <summary>
        /// The description for this object.
        /// </summary>
        string Description { get; }
    }
}
