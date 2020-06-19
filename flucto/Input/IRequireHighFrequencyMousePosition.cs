// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics;

namespace flucto.Input
{
    /// <summary>
    /// Guarantees that a drawable will receive at least one OnMouseMove position update
    /// per update frame (in addition to any input-triggered occurrences).
    /// </summary>
    public interface IRequireHighFrequencyMousePosition : IDrawable
    {
    }
}
