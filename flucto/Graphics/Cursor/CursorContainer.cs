﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Allocation;
using flucto.Graphics.Containers;
using flucto.Graphics.Effects;
using flucto.Graphics.Shapes;
using flucto.Input;
using flucto.Input.Events;
using flucto.MathUtils;
using osuTK;
using osuTK.Graphics;

namespace flucto.Graphics.Cursor
{
    public class CursorContainer : VisibilityContainer, IRequireHighFrequencyMousePosition
    {
        public Drawable ActiveCursor { get; protected set; }

        public CursorContainer()
        {
            Depth = float.MinValue;
            RelativeSizeAxes = Axes.Both;

            State.Value = Visibility.Visible;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(ActiveCursor = CreateCursor());
        }

        protected virtual Drawable CreateCursor() => new Cursor();

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => true;

        public override bool PropagatePositionalInputSubTree => IsPresent; // make sure we are still updating position during possible fade out.

        private Vector2? lastPosition;

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            // required due to IRequireHighFrequencyMousePosition firing with the last known position even when the source is not in a
            // valid state (ie. receiving updates from user or otherwise). in this case, we generally want the cursor to remain at its
            // last *relative* position.
            if (lastPosition.HasValue && Precision.AlmostEquals(e.ScreenSpaceMousePosition, lastPosition.Value))
                return false;

            lastPosition = e.ScreenSpaceMousePosition;

            ActiveCursor.RelativePositionAxes = Axes.None;
            ActiveCursor.Position = e.MousePosition;
            ActiveCursor.RelativePositionAxes = Axes.Both;
            return base.OnMouseMove(e);
        }

        protected override void PopIn()
        {
            Alpha = 1;
        }

        protected override void PopOut()
        {
            Alpha = 0;
        }

        private class Cursor : CircularContainer
        {
            public Cursor()
            {
                AutoSizeAxes = Axes.Both;
                Origin = Anchor.Centre;

                BorderThickness = 2;
                BorderColour = new Color4(247, 99, 164, 255);

                Masking = true;
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Glow,
                    Colour = new Color4(247, 99, 164, 6),
                    Radius = 50
                };

                Child = new Box
                {
                    Size = new Vector2(8, 8),
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                };
            }
        }
    }
}
