// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.Primitives;
using flucto.Graphics.Shapes;
using System;

namespace flucto.Graphics.Visualisation
{
    internal class FlashyBox : Box
    {
        private Drawable target;
        private readonly Func<Drawable, Quad> getScreenSpaceQuad;

        public FlashyBox(Func<Drawable, Quad> getScreenSpaceQuad)
        {
            this.getScreenSpaceQuad = getScreenSpaceQuad;
        }

        public Drawable Target
        {
            set => target = value;
        }

        public override Quad ScreenSpaceDrawQuad => target == null ? new Quad() : getScreenSpaceQuad(target);
    }
}
