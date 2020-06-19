// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.Transforms;
using osuTK;

namespace flucto.Graphics.Containers
{
    public interface IFillFlowContainer : ITransformable
    {
        Vector2 Spacing { get; set; }
    }
}
