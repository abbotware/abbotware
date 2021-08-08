// -----------------------------------------------------------------------
// <copyright file="Interval{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// class that represents a numeric interval.
    /// </summary>
    /// <typeparam name="T">interval type</typeparam>
    public readonly struct Interval<T> : IEquatable<Interval<T>>
        where T : IComparable, IComparable<T>, IEquatable<T>
    {
        private readonly IComparer<T> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> struct.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="upper">upper bound</param>
        public Interval(T lower, T upper)
            : this(Lower(lower, upper), true, Upper(lower, upper), true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> struct.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="includeLower">include lower bound</param>
        /// <param name="upper">upper bound</param>
        /// <param name="includeUpper">include upper bound</param>
        public Interval(T lower, bool includeLower, T upper, bool includeUpper)
        {
            this.comparer = Comparer<T>.Default;

            if (this.comparer.Compare(lower, upper) > 0)
            {
                throw new ArgumentException($"lower:{lower} > upper:{upper}");
            }

            this.LowerBound = lower;
            this.IncludeLower = includeLower;
            this.UpperBound = upper;
            this.IncludeUpper = includeUpper;
        }

        /// <summary>
        /// Gets the lower bound
        /// /// </summary>
        public T LowerBound { get; }

        /// <summary>
        /// Gets the upper bound.
        /// </summary>
        public T UpperBound { get; }

        /// <summary>
        /// Gets a value indicating whether to include both upper and lower bounds
        /// </summary>
        public bool IsInclusive => this.IncludeLower && this.IncludeUpper;

        /// <summary>
        /// Gets a value indicating whether to exclude both upper and lower bounds
        /// </summary>
        public bool IsExlusive => !this.IncludeLower && !this.IncludeUpper;

        /// <summary>
        /// Gets a value indicating whether to include the lower bound
        /// </summary>
        public bool IncludeLower { get; }

        /// <summary>
        /// Gets a value indicating whether to include the upper bound
        /// </summary>
        public bool IncludeUpper { get; }

        /// <summary>
        /// equals operator
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>true if equal</returns>
        public static bool operator ==(Interval<T> left, Interval<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// not equals operator
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(Interval<T> left, Interval<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Checks if the supplied value is within the interval range
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true / false if the value is wthin the interval</returns>
        public bool Within(T value)
        {
            var valToLower = this.comparer.Compare(value, this.LowerBound);
            var valToUpper = this.comparer.Compare(value, this.UpperBound);

            if (this.IsInclusive)
            {
                return (valToLower >= 0) && (valToUpper <= 0);
            }
            else if (this.IncludeLower && !this.IncludeUpper)
            {
                return (valToLower >= 0) && (valToUpper < 0);
            }
            else if (!this.IncludeLower && this.IncludeUpper)
            {
                return (valToLower > 0) && (valToUpper <= 0);
            }
            else
            {
                return (valToLower > 0) && (valToUpper < 0);
            }
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
#if NETSTANDARD2_0
            return (this.LowerBound, this.UpperBound, this.IncludeUpper, this.IncludeLower).GetHashCode();
#else
            return HashCode.Combine(this.LowerBound, this.UpperBound, this.IncludeUpper, this.IncludeLower);
#endif
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (!this.StructPossiblyEquals<Interval<T>>(obj, out var other))
            {
                return false;
            }

#if NETSTANDARD2_0
            if (other == null)
            {
                return false;
            }
#endif
            return this.Equals(other);
        }

        /// <inheritdoc/>
        public bool Equals(Interval<T> other)
        {
            if (!EqualityComparer<T>.Default.Equals(this.LowerBound, other.LowerBound))
            {
                return false;
            }

            if (!EqualityComparer<T>.Default.Equals(this.UpperBound, other.UpperBound))
            {
                return false;
            }

            if (!this.IncludeLower == other.IncludeLower)
            {
                return false;
            }

            if (!this.IncludeUpper == other.IncludeUpper)
            {
                return false;
            }

            return true;
        }

        private static T Lower(T left, T right)
        {
            var comparer = Comparer<T>.Default;

            var valToLower = comparer.Compare(left, right);

            if (valToLower < 0)
            {
                return left;
            }
            else
            {
                return right;
            }
        }

        private static T Upper(T left, T right)
        {
            var comparer = Comparer<T>.Default;

            var valToLower = comparer.Compare(left, right);

            if (valToLower > 0)
            {
                return left;
            }
            else
            {
                return right;
            }
        }
    }
}