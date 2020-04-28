// -----------------------------------------------------------------------
// <copyright file="HighResolutionTimeSpan.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// TimeSpan that uses the high resolution clock
    /// </summary>
    public struct HighResolutionTimeSpan : IEquatable<HighResolutionTimeSpan>
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="HighResolutionTimeSpan" /> struct.
        /// </summary>
        /// <param name="duration">The amount of time that can pass before the timer is considered expired</param>
        public HighResolutionTimeSpan(TimeSpan duration)
        {
            var total_ticks = duration.Ticks * Stopwatch.Frequency / TimeSpan.TicksPerSecond;

            this.Start = StopwatchHelper.CurrentTimeStamp;
            this.End = this.Start + total_ticks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HighResolutionTimeSpan"/> struct.
        /// </summary>
        /// <param name="start">start ticks</param>
        /// <param name="end">end ticks</param>
        public HighResolutionTimeSpan(long start, long end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the start ticks
        /// </summary>
        public long Start { get; }

        /// <summary>
        /// Gets the end ticks
        /// </summary>
        public long End { get; }

        /// <summary>
        ///     Gets a value indicating whether the timer's expiration point has been reached, otherwise false
        /// </summary>
        public bool IsExpired
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return StopwatchHelper.CurrentTimeStamp >= this.End; }
        }

        /// <summary>
        /// equals operator
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(HighResolutionTimeSpan left, HighResolutionTimeSpan right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// not equals operator
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(HighResolutionTimeSpan left, HighResolutionTimeSpan right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!this.StructPossiblyEquals<HighResolutionTimeSpan>(obj, out var other))
            {
                return false;
            }

#if NETSTANDARD2_0
            if (other.HasValue)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
#else
            return this.Equals(other);
#endif
        }

        /// <inheritdoc />
        public bool Equals(HighResolutionTimeSpan other)
        {
            if (!this.Start.Equals(other.Start))
            {
                return false;
            }

            if (!EqualityComparer<long?>.Default.Equals(this.End, other.End))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
#if NETSTANDARD2_0
            return (this.Start, this.End).GetHashCode();
#else
            return HashCode.Combine(this.Start, this.End);
#endif
        }
    }
}
