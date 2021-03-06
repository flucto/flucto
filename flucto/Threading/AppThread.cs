﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;
using flucto.Statistics;
using flucto.Timing;
using System.Collections.Generic;
using flucto.Bindables;

namespace flucto.Threading
{
    public class AppThread
    {
        internal const double DEFAULT_ACTIVE_HZ = 1000;
        internal const double DEFAULT_INACTIVE_HZ = 60;

        internal PerformanceMonitor Monitor { get; }
        public ThrottledFrameClock Clock { get; }
        public Thread Thread { get; }
        public Scheduler Scheduler { get; }

        /// <summary>
        /// Attach a handler to delegate responsibility for per-frame exceptions.
        /// While attached, all exceptions will be caught and forwarded. Thread execution will continue indefinitely.
        /// </summary>
        public EventHandler<UnhandledExceptionEventArgs> UnhandledException;

        protected Action OnNewFrame;

        /// <summary>
        /// Whether the game is active (in the foreground).
        /// </summary>
        public readonly IBindable<bool> IsActive = new Bindable<bool>(true);

        private double activeHz = DEFAULT_ACTIVE_HZ;

        public double ActiveHz
        {
            get => activeHz;
            set
            {
                activeHz = value;
                updateMaximumHz();
            }
        }

        private double inactiveHz = DEFAULT_INACTIVE_HZ;

        public double InactiveHz
        {
            get => inactiveHz;
            set
            {
                inactiveHz = value;
                updateMaximumHz();
            }
        }

        public static string PrefixedThreadNameFor(string name) => $"{nameof(AppThread)}.{name}";

        public bool Running => Thread.IsAlive;

        private readonly ManualResetEvent initializedEvent = new ManualResetEvent(false);

        public Action OnThreadStart;

        internal virtual IEnumerable<StatisticsCounterType> StatisticsCounters => Array.Empty<StatisticsCounterType>();

        public readonly string Name;

        internal AppThread(Action onNewFrame = null, string name = "unknown", bool monitorPerformance = true)
        {
            OnNewFrame = onNewFrame;

            Thread = new Thread(runWork)
            {
                Name = PrefixedThreadNameFor(name),
                IsBackground = true,
            };

            Name = name;
            Clock = new ThrottledFrameClock();
            if (monitorPerformance)
                Monitor = new PerformanceMonitor(this, StatisticsCounters);
            Scheduler = new Scheduler(null, Clock);

            IsActive.BindValueChanged(_ => updateMaximumHz(), true);
        }

        public void WaitUntilInitialized()
        {
            initializedEvent.WaitOne();
        }

        private void updateMaximumHz() => Scheduler.Add(() => Clock.MaximumUpdateHz = IsActive.Value ? activeHz : inactiveHz);

        private void runWork()
        {
            Scheduler.SetCurrentThread();

            OnThreadStart?.Invoke();

            initializedEvent.Set();

            while (!exitCompleted)
            {
                try
                {
                    ProcessFrame();
                }
                catch (Exception e)
                {
                    if (UnhandledException != null)
                        UnhandledException.Invoke(this, new UnhandledExceptionEventArgs(e, false));
                    else
                        throw;
                }
            }
        }

        protected void ProcessFrame()
        {
            if (exitCompleted)
                return;

            if (exitRequested)
            {
                PerformExit();
                exitCompleted = true;
                return;
            }

            Monitor?.NewFrame();

            using (Monitor?.BeginCollecting(PerformanceCollectionType.Scheduler))
                Scheduler.Update();

            using (Monitor?.BeginCollecting(PerformanceCollectionType.Work))
                OnNewFrame?.Invoke();

            using (Monitor?.BeginCollecting(PerformanceCollectionType.Sleep))
                Clock.ProcessFrame();
        }

        private volatile bool exitRequested;
        private volatile bool exitCompleted;

        public bool Exited => exitCompleted;

        public void Exit() => exitRequested = true;
        public virtual void Start() => Thread?.Start();

        protected virtual void PerformExit()
        {
            Monitor?.Dispose();
            initializedEvent?.Dispose();
        }
    }
}
