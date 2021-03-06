﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Statistics;
using System;
using System.Collections.Generic;

namespace flucto.Threading
{
    public class UpdateThread : AppThread
    {
        public UpdateThread(Action onNewFrame)
            : base(onNewFrame, "Update")
        {
        }

        internal override IEnumerable<StatisticsCounterType> StatisticsCounters => new[]
        {
            StatisticsCounterType.Invalidations,
            StatisticsCounterType.Refreshes,
            StatisticsCounterType.DrawNodeCtor,
            StatisticsCounterType.DrawNodeAppl,
            StatisticsCounterType.ScheduleInvk,
            StatisticsCounterType.InputQueue,
            StatisticsCounterType.PositionalIQ,
            StatisticsCounterType.CCL
        };
    }
}
