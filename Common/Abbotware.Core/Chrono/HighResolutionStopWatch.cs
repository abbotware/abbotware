// -----------------------------------------------------------------------
// <copyright file="HighResolutionStopWatch.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Stopwatch that returns HighResolutionTimeSpans
    /// </summary>
    public class HighResolutionStopWatch
    {
        /// <summary>
        /// Gets the start ticks
        /// </summary>
        public long StartTicks { get; private set; } = StopwatchHelper.CurrentTimeStamp;

        /// <summary>
        /// Gets the elapsed TimeSpan
        /// </summary>
        public HighResolutionTimeSpan Elapsed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(this.StartTicks, StopwatchHelper.CurrentTimeStamp);
        }

        /// <summary>
        /// Create a new stopwatch starting now
        /// </summary>
        /// <returns>new hs timespan</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HighResolutionStopWatch StartNew()
        {
            return new HighResolutionStopWatch();
        }

        /// <summary>
        /// reset the stopwatch
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            this.StartTicks = StopwatchHelper.CurrentTimeStamp;
        }
    }
}
