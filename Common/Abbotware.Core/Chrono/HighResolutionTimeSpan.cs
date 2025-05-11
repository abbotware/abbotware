// -----------------------------------------------------------------------
// <copyright file="HighResolutionTimeSpan.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono;

using System;
using System.Runtime.CompilerServices;

/// <summary>
/// TimeSpan that uses the high resolution clock
/// </summary>
/// <param name="Start">start timestamp</param>
/// <param name="End">end timestamp</param>
public record struct HighResolutionTimeSpan(long Start, long End)
{
    /// <summary>
    /// Gets a value indicating whether the timer's expiration point has been reached, otherwise false
    /// </summary>
    public bool IsExpired
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HighResolutionStopWatch.Now >= this.End;
    }

    /// <summary>
    /// Gets the total Seconds
    /// </summary>
    public double TotalSeconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HighResolutionStopWatch.AsSeconds(this.Start, this.End);
    }

    /// <summary>
    /// Gets the total Milliseconds
    /// </summary>
    public double TotalMilliseconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HighResolutionStopWatch.AsMilliseconds(this.Start, this.End);
    }

    /// <summary>
    /// Gets the total Microseconds
    /// </summary>
    public double TotalMicroseconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HighResolutionStopWatch.AsMicroseconds(this.Start, this.End);
    }

    /// <summary>
    /// Gets the total Nanoseconds
    /// </summary>
    public double TotalNanoseconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HighResolutionStopWatch.AsNanoseconds(this.Start, this.End);
    }

    /// <summary>
    /// Gets the TimeSpan
    /// </summary>
    public TimeSpan Elapsed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HighResolutionStopWatch.AsTimeSpan(this.Start, this.End);
    }

    /// <summary>
    /// Creates future expiring timepan - useful for low-latency timers
    /// </summary>
    /// <param name="duration">The amount of time that can pass before the timer is considered expired</param>
    /// <returns>high-res time span</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static HighResolutionTimeSpan Future(TimeSpan duration)
    {
        var start = HighResolutionStopWatch.Now;
        var end = HighResolutionStopWatch.Add(start, duration);
        return new(start, end);
    }
}
