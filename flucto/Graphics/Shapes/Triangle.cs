﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Graphics.OpenGL.Vertices;
using flucto.Graphics.Textures;
using osuTK;
using flucto.Graphics.Primitives;
using flucto.Graphics.Sprites;

namespace flucto.Graphics.Shapes
{
    /// <summary>
    /// Represents a sprite that is drawn in a triangle shape, instead of a rectangle shape.
    /// </summary>
    public class Triangle : Sprite
    {
        /// <summary>
        /// Creates a new triangle with a white pixel as texture.
        /// </summary>
        public Triangle()
        {
            Texture = Texture.WhitePixel;
        }

        public override RectangleF BoundingBox => toTriangle(ToParentSpace(LayoutRectangle)).AABBFloat;

        private static Primitives.Triangle toTriangle(Quad q) => new Primitives.Triangle(
            (q.TopLeft + q.TopRight) / 2,
            q.BottomLeft,
            q.BottomRight);

        public override bool Contains(Vector2 screenSpacePos) => toTriangle(ScreenSpaceDrawQuad).Contains(screenSpacePos);

        protected override DrawNode CreateDrawNode() => new TriangleDrawNode(this);

        private class TriangleDrawNode : SpriteDrawNode
        {
            public TriangleDrawNode(Triangle source)
                : base(source)
            {
            }

            protected override void Blit(Action<TexturedVertex2D> vertexAction)
            {
                DrawTriangle(Texture, toTriangle(ScreenSpaceDrawQuad), DrawColourInfo.Colour, null, null,
                    new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height));
            }
        }
    }
}
