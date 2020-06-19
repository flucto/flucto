// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Graphics.Primitives;
using osuTK.Graphics.ES30;
using SixLabors.ImageSharp.PixelFormats;

namespace flucto.Graphics.Textures
{
    public interface ITextureUpload : IDisposable
    {
        ReadOnlySpan<Rgba32> Data { get; }
        int Level { get; }
        RectangleI Bounds { get; set; }
        PixelFormat Format { get; }
    }
}
