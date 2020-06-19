﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osuTK;
using System;
using System.Runtime.CompilerServices;

namespace flucto.Graphics.Primitives
{
    public struct Triangle : IConvexPolygon, IEquatable<Triangle>
    {
        public Vector2 P0;
        public Vector2 P1;
        public Vector2 P2;

        public Triangle(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;
        }

        public ReadOnlySpan<Vector2> GetAxisVertices() => GetVertices();

        public unsafe ReadOnlySpan<Vector2> GetVertices()
        {
            return new ReadOnlySpan<Vector2>(Unsafe.AsPointer(ref this), 3);
        }

        public bool Equals(Triangle other) =>
            P0 == other.P0 &&
            P1 == other.P1 &&
            P2 == other.P2;

        /// <summary>
        /// Checks whether a point lies within the triangle.
        /// </summary>
        /// <param name="pos">The point to check.</param>
        /// <returns>Outcome of the check.</returns>
        public bool Contains(Vector2 pos)
        {
            // This code parametrizes pos as a linear combination of 2 edges s*(p1-p0) + t*(p2->p0).
            // pos is contained if s>0, t>0, s+t<1
            float area2 = P0.Y * (P2.X - P1.X) + P0.X * (P1.Y - P2.Y) + P1.X * P2.Y - P1.Y * P2.X;
            if (area2 == 0)
                return false;

            float s = (P0.Y * P2.X - P0.X * P2.Y + (P2.Y - P0.Y) * pos.X + (P0.X - P2.X) * pos.Y) / area2;
            if (s < 0)
                return false;

            float t = (P0.X * P1.Y - P0.Y * P1.X + (P0.Y - P1.Y) * pos.X + (P1.X - P0.X) * pos.Y) / area2;
            if (t < 0 || s + t > 1)
                return false;

            return true;
        }

        public RectangleF AABBFloat
        {
            get
            {
                float xMin = Math.Min(P0.X, Math.Min(P1.X, P2.X));
                float yMin = Math.Min(P0.Y, Math.Min(P1.Y, P2.Y));
                float xMax = Math.Max(P0.X, Math.Max(P1.X, P2.X));
                float yMax = Math.Max(P0.Y, Math.Max(P1.Y, P2.Y));

                return new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
            }
        }

        public float Area => 0.5f * Math.Abs(Vector2Extensions.GetOrientation(GetVertices()));
    }
}
