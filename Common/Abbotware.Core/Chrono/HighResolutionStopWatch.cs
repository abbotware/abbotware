// -----------------------------------------------------------------------
// <copyright file="HighResolutionStopWatch.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono;

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Abbotware.Core.Chrono.Units;

/// <summary>
/// Stopwatch that returns HighResolutionTimeSpans
/// </summary>
public class HighResolutionStopWatch
{
    private HighResolutionStopWatch(long start)
    {
        this.Start = start;
    }

    /// <summary>
    /// Gets 'Now' as CPU Time Stamp Counter (TSC)
    /// </summary>
    public static long Now
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Stopwatch.GetTimestamp();
    }

    /// <summary>
    ///   Gets the initial date time
    /// </summary>
    public static DateTimeOffset InitialDateTime { get; } = DateTimeOffset.Now;

    /// <summary>
    ///  Gets the initial ticks TimeStamp
    /// </summary>
    public static long InitialTimeStamp { get; } = Now;

    /// <summary>
    /// Gets the number of ticks/second
    /// </summary>
    public static long TicksPerSecond
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

= Stopwatch.Frequency;

    /// <summary>
    /// Gets the number of seconds/tick
    /// </summary>
    public static double SecondsPerTick
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

= 1d / TicksPerSecond;

    /// <summary>
    /// Gets the number of Milliseconds/tick
    /// </summary>
    public static double MillisecondsPerTick
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

= PerSecond.Milliseconds / TicksPerSecond;

    /// <summary>
    /// Gets the number of microseconds/tick
    /// </summary>
    public static double MicrosecondsPerTick
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

= PerSecond.Microseconds / TicksPerSecond;

    /// <summary>
    /// Gets the number of Nanoseconds/tick
    /// </summary>
    public static double NanosecondsPerTick
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

= PerSecond.Nanoseconds / TicksPerSecond;

    /// <summary>
    /// Gets the start timestamp
    /// </summary>
    public long Start
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    /// <summary>
    /// Gets the Elapsed time
    /// </summary>
    /// <returns>timespan</returns>
    public HighResolutionTimeSpan Elapsed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(this.Start, Now);
    }

    /// <summary>
    /// Gets the Elapsed as a TimeSpan
    /// </summary>
    /// <returns>timespan</returns>
    public TimeSpan ElapsedTimeSpan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsTimeSpan(this.Start, Now);
    }

    /// <summary>
    /// Converts timestamp to Seconds
    /// </summary>
    /// <param name="timestamp">timestamp</param>
    /// <returns>seconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToSeconds(long timestamp)
         => timestamp * SecondsPerTick;

    /// <summary>
    /// Converts timestamp to Milliseconds
    /// </summary>
    /// <param name="timestamp">timestamp</param>
    /// <returns>Milliseconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToMilliseconds(long timestamp)
         => timestamp * MillisecondsPerTick;

    /// <summary>
    /// Converts timestamp to Microseconds
    /// </summary>
    /// <param name="timestamp">timestamp</param>
    /// <returns>Microseconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToMicroseconds(long timestamp)
        => timestamp * MicrosecondsPerTick;

    /// <summary>
    /// Converts timestamp to Nanoseconds
    /// </summary>
    /// <param name="timestamp">timestamp</param>
    /// <returns>Nanoseconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToNanoseconds(long timestamp)
        => timestamp * NanosecondsPerTick;

    /// <summary>
    /// Converts timestamp to a TimeSpan
    /// </summary>
    /// <param name="timestamp">timestamp</param>
    /// <returns>TimeSpan</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan ToTimeSpan(long timestamp)
        => TimeSpan.FromMilliseconds(ToMilliseconds(timestamp));

    /// <summary>
    /// Converts the ticks to a date time
    /// </summary>
    /// <param name="timestamp">ticks to convert</param>
    /// <returns>datetime</returns>
    public static DateTimeOffset ToDateTimeOffset(long timestamp)
        => InitialDateTime + ToTimeSpan(timestamp);

    /// <summary>
    ///  Converts a TimeSpan to ticks
    /// </summary>
    /// <param name="timeSpan">timeSpan</param>
    /// <returns>cpu ticks</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToTicks(TimeSpan timeSpan)
        => timeSpan.TotalSeconds * TicksPerSecond;

    /// <summary>
    /// Computes the difference as Seconds
    /// </summary>
    /// <param name="start">start timestampe</param>
    /// <param name="end">end timestamp</param>
    /// <returns>Seconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double AsSeconds(long start, long end)
        => ToSeconds(start - end);

    /// <summary>
    /// Computes the difference as Milliseconds
    /// </summary>
    /// <param name="start">start timestampe</param>
    /// <param name="end">end timestamp</param>
    /// <returns>Milliseconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double AsMilliseconds(long start, long end)
        => ToMilliseconds(start - end);

    /// <summary>
    /// Computes the difference as Microseconds
    /// </summary>
    /// <param name="start">start timestampe</param>
    /// <param name="end">end timestamp</param>
    /// <returns>Microseconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double AsMicroseconds(long start, long end)
        => ToMicroseconds(start - end);

    /// <summary>
    /// Computes the difference as Nanoseconds
    /// </summary>
    /// <param name="start">start timestampe</param>
    /// <param name="end">end timestamp</param>
    /// <returns>Nanoseconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double AsNanoseconds(long start, long end)
        => ToNanoseconds(start - end);

    /// <summary>
    /// Computes the difference as seconds
    /// </summary>
    /// <param name="start">start timestampe</param>
    /// <param name="end">end timestamp</param>
    /// <returns>seconds</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan AsTimeSpan(long start, long end)
        => TimeSpan.FromMilliseconds(AsMilliseconds(start, end));

    /// <summary>
    /// Create a new stopwatch starting now
    /// </summary>
    /// <returns>new hs timespan</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static HighResolutionStopWatch StartNew()
        => new(Now);

    /// <summary>
    /// Perform a SpinWait until the desired timestamp is reached
    /// </summary>
    /// <param name="timestamp">end timestamp</param>
    /// <param name="iterations">used to spinwait</param>
    /// <param name="ct">cancellation token</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SpinUntil(long timestamp, sbyte iterations, CancellationToken ct)
    {
        while (Now < timestamp)
        {
            ct.ThrowIfCancellationRequested();
            Thread.SpinWait(iterations);
        }
    }

    /// <summary>
    /// Adds a TimeSpan to a timestamp
    /// </summary>
    /// <param name="timestamp">intiial timestamp</param>
    /// <param name="duration">timespan to add</param>
    /// <returns>computed end timestamp</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Add(long timestamp, TimeSpan duration)
        => (long)(timestamp + ToTicks(duration));

    /// <summary>
    /// Computes a timestamp from now
    /// </summary>
    /// <param name="duration">timespan to add</param>
    /// <returns>computed end timestamp</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long FromNow(TimeSpan duration)
         => Add(Now, duration);
}
