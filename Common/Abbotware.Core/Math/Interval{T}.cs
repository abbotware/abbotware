// -----------------------------------------------------------------------
// <copyright file="Interval{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    using System.Collections.Generic;

    /// <summary>
    /// class that represents a numeric interval.
    /// </summary>
    /// <typeparam name="T">interval type</typeparam>
    /// <param name="Lower">lower bound</param>
    /// <param name="Upper">upper bound</param>
    /// <param name="IncludeLower">Gets a value indicating whether to include the upper bound</param>
    /// <param name="IncludeUpper">Gets a value indicating whether to include the lower bound</param>
    public record class Interval<T>(T Lower, T Upper, bool IncludeLower, bool IncludeUpper)
    {
        private static readonly IComparer<T> Comparer = Comparer<T>.Default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval{T}"/> class.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="upper">upper bound</param>
        public Interval(T lower, T upper)
            : this(Min(lower, upper), Max(lower, upper), true, true)
        {
        }

        /// <summary>
        /// Gets a value indicating whether to include both upper and lower bounds
        /// </summary>
        public bool IsInclusive => this.IncludeLower && this.IncludeUpper;

        /// <summary>
        /// Gets a value indicating whether to exclude both upper and lower bounds
        /// </summary>
        public bool IsExlusive => !this.IncludeLower && !this.IncludeUpper;

        /// <summary>
        /// Checks if the supplied value is within the interval range
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true / false if the value is wthin the interval</returns>
        public bool Within(T value)
        {
            var valToLower = Comparer.Compare(value, this.Lower);
            var valToUpper = Comparer.Compare(value, this.Upper);

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

        private static T Min(T left, T right)
        {
            var valToLower = Comparer.Compare(left, right);

            if (valToLower < 0)
            {
                return left;
            }
            else
            {
                return right;
            }
        }

        private static T Max(T left, T right)
        {
            var valToLower = Comparer.Compare(left, right);

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