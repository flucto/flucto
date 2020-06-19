﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Allocation;
using flucto.Graphics;
using flucto.Graphics.Containers;
using flucto.Timing;

namespace flucto.Screens
{
    public class Screen : CompositeDrawable, IScreen
    {
        public bool ValidForResume { get; set; } = true;

        public bool ValidForPush { get; set; } = true;

        public sealed override bool RemoveWhenNotAlive => false;

        [Resolved]
        protected Game Game { get; private set; }

        public Screen()
        {
            RelativeSizeAxes = Axes.Both;
        }

        internal override void UpdateClock(IFrameBasedClock clock)
        {
            base.UpdateClock(clock);
            if (Parent != null && !(Parent is ScreenStack))
                throw new InvalidOperationException($"Screens must always be added to a {nameof(ScreenStack)} (attempted to add {GetType()} to {Parent.GetType()})");
        }

        public virtual void OnEntering(IScreen last)
        {
        }

        public virtual bool OnExiting(IScreen next) => false;

        public virtual void OnResuming(IScreen last)
        {
        }

        public virtual void OnSuspending(IScreen next)
        {
        }
    }
}