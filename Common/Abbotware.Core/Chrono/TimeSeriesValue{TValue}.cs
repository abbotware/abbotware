// -----------------------------------------------------------------------
// <copyright file="TimeSeriesValue{TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// Time series value
    /// </summary>
    /// <typeparam name="TValue">type of the value</typeparam>
    public struct TimeSeriesValue<TValue> : ITimeSeriesValue<TValue>, IEquatable<TimeSeriesValue<TValue>>
    {
        /// <summary>
        /// Gets or sets the date/time
        /// </summary>
        public DateTimeOffset X { get; set; }

        /// <inheritdoc/>
        public TValue Y { get; set; }

        /// <summary>
        ///     Tests equals
        /// </summary>
        /// <param name="left">left hand side operand</param>
        /// <param name="right">right hand side operand</param>
        /// <returns>true/false</returns>
        public static bool operator ==(TimeSeriesValue<TValue> left, TimeSeriesValue<TValue> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Tests Not equals
        /// </summary>
        /// <param name="left">left hand side operand</param>
        /// <param name="right">right hand side operand</param>
        /// <returns>true/false</returns>
        public static bool operator !=(TimeSeriesValue<TValue> left, TimeSeriesValue<TValue> right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!this.StructPossiblyEquals<TimeSeriesValue<TValue>>(obj, out var other))
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <inheritdoc />
        public bool Equals(TimeSeriesValue<TValue> other)
        {
            if (!other.X.Equals(this.X))
            {
                return false;
            }

            if (!other.Y.Equals(this.Y))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.X, this.Y).GetHashCode();
        }
    }
}