// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Allocation;
using flucto.Statistics;

namespace flucto.Platform
{
    /// <summary>
    /// Track native memory allocations via <see cref="GlobalStatistics"/>.
    /// Also adds memory pressure automatically.
    /// </summary>
    public class NativeMemoryTracker
    {
        /// <summary>
        /// Add new tracked native memory.
        /// </summary>
        /// <param name="source">The object responsible for this allocation.</param>
        /// <param name="amount">The number of bytes allocated.</param>
        /// <returns>A lease which should be disposed when memory is released.</returns>
        public static NativeMemoryLease AddMemory(object source, long amount)
        {
            getStatistic(source).Value += amount;
            GC.AddMemoryPressure(amount);

            return new NativeMemoryLease(() => removeMemory(source, amount));
        }

        /// <summary>
        /// Remove previously tracked native memory.
        /// </summary>
        /// <param name="source">The object responsible for this allocation.</param>
        /// <param name="amount">The number of bytes allocated.</param>
        private static void removeMemory(object source, long amount)
        {
            getStatistic(source).Value -= amount;
            GC.RemoveMemoryPressure(amount);
        }

        private static GlobalStatistic<long> getStatistic(object source) => GlobalStatistics.Get<long>("Native", source.GetType().Name);

        /// <summary>
        /// A leased on a native memory allocation. Should be disposed when the associated memory is freed.
        /// </summary>
        public class NativeMemoryLease : InvokeOnDisposal
        {
            internal NativeMemoryLease(Action action)
                : base(action)
            {
            }

            private bool isDisposed;

            public override void Dispose()
            {
                if (isDisposed)
                    throw new ObjectDisposedException(ToString(), $"{nameof(NativeMemoryLease)} should not be disposed more than once");

                base.Dispose();
                isDisposed = true;
            }
        }
    }
}