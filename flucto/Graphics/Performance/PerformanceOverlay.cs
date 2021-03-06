﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Graphics.Containers;
using flucto.Threading;
using System.Collections.Generic;

namespace flucto.Graphics.Performance
{
    internal class PerformanceOverlay : FillFlowContainer<FrameStatisticsDisplay>, IStateful<FrameStatisticsMode>
    {
        private readonly IEnumerable<AppThread> threads;
        private FrameStatisticsMode state;

        public event Action<FrameStatisticsMode> StateChanged;

        private bool initialised;

        public FrameStatisticsMode State
        {
            get => state;
            set
            {
                if (state == value) return;

                state = value;

                switch (state)
                {
                    case FrameStatisticsMode.None:
                        this.FadeOut(100);
                        break;

                    case FrameStatisticsMode.Minimal:
                    case FrameStatisticsMode.Full:
                        if (!initialised)
                        {
                            initialised = true;
                            foreach (AppThread t in threads)
                                Add(new FrameStatisticsDisplay(t) { State = state });
                        }

                        this.FadeIn(100);
                        break;
                }

                foreach (FrameStatisticsDisplay d in Children)
                    d.State = state;

                StateChanged?.Invoke(State);
            }
        }

        public PerformanceOverlay(IEnumerable<AppThread> threads)
        {
            this.threads = threads;
            Direction = FillDirection.Vertical;
        }
    }

    public enum FrameStatisticsMode
    {
        None,
        Minimal,
        Full
    }
}
