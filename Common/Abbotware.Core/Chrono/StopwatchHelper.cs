// -----------------------------------------------------------------------
// <copyright file="StopwatchHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     Helper class for dealing with the stopwatch
    /// </summary>
    public static class StopwatchHelper
    {
        /// <summary>
        ///   Gets the initial date time
        /// </summary>
        public static DateTime InitialDateTime { get; } = DateTime.Now;

        /// <summary>
        ///  Gets the initial ticks TimeStamp
        /// </summary>
        public static long InitialTimeStamp { get; } = CurrentTimeStamp;

        /// <summary>
        ///     Gets the current high frequency system clock timestamp
        /// </summary>
        /// <returns>The high frequency system clock's timestamp</returns>
        public static long CurrentTimeStamp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// Converts the ticks to a date time
        /// </summary>
        /// <param name="ticks">ticks to convert</param>
        /// <returns>datetime</returns>
        public static DateTime TicksToDateTime(long ticks)
        {
            return InitialDateTime + StopwatchHelper.DiffToTimeSpan(ticks, InitialTimeStamp);
        }

        /// <summary>
        ///     Converts ticks to milliseconds
        /// </summary>
        /// <param name="ticks">ticks to convert</param>
        /// <returns>milliseconds</returns>
        public static double TicksToSeconds(long ticks)
        {
            return (double)ticks / Stopwatch.Frequency * 1000.0;
        }

        /// <summary>
        ///     Converts ticks to milliseconds
        /// </summary>
        /// <param name="ticks">ticks to convert</param>
        /// <returns>milliseconds</returns>
        public static double TicksToMilliseconds(long ticks)
        {
            return (double)ticks / Stopwatch.Frequency * 1000.0;
        }

        /// <summary>
        ///     Converts ticks to nanoseconds
        /// </summary>
        /// <param name="ticks">ticks to convert</param>
        /// <returns>milliseconds</returns>
        public static double TicksToNanoseconds(long ticks)
        {
            return (double)ticks / Stopwatch.Frequency * 1000000000.0;
        }

        /// <summary>
        ///     Computes the difference of ticks and returns milliseconds
        /// </summary>
        /// <param name="startTicks">start tick value</param>
        /// <param name="endTicks">end tick value</param>
        /// <returns>milliseconds</returns>
        public static double DiffToMilliseconds(long startTicks, long endTicks)
        {
            return TicksToMilliseconds(endTicks - startTicks);
        }

        /// <summary>
        ///     Computes the difference of ticks and returns nanoseconds
        /// </summary>
        /// <param name="startTicks">start tick value</param>
        /// <param name="endTicks">end tick value</param>
        /// <returns>nanoseconds</returns>
        public static double DiffToNanoseconds(long startTicks, long endTicks)
        {
            return TicksToNanoseconds(endTicks - startTicks);
        }

        /// <summary>
        ///     Computes the difference of ticks
        /// </summary>
        /// <param name="startTicks">start tick value</param>
        /// <param name="endTicks">end tick value</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan DiffToTimeSpan(long startTicks, long endTicks)
        {
            return TimeSpan.FromSeconds(TicksToSeconds(startTicks - endTicks));
        }
    }
}