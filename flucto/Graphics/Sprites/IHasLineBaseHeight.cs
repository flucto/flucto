// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Graphics.Sprites
{
    /// <summary>
    /// Objects implementing this interface have a line base height when used in a CustomizableTextContainer.
    /// </summary>
    public interface IHasLineBaseHeight
    {
        /// <summary>
        /// The line base height this object has.
        /// </summary>
        float LineBaseHeight { get; }
    }
}
