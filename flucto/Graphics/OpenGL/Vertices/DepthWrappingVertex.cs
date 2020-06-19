// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Runtime.InteropServices;
using osuTK.Graphics.ES30;

namespace flucto.Graphics.OpenGL.Vertices
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DepthWrappingVertex<TVertex> : IVertex, IEquatable<DepthWrappingVertex<TVertex>>
        where TVertex : IVertex, IEquatable<TVertex>
    {
        public TVertex Vertex;

        [VertexMember(1, VertexAttribPointerType.Float)]
        public float BackbufferDrawDepth;

        public bool Equals(DepthWrappingVertex<TVertex> other)
            => Vertex.Equals(other.Vertex)
               && BackbufferDrawDepth.Equals(other.BackbufferDrawDepth);
    }
}
