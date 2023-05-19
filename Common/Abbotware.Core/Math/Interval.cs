// -----------------------------------------------------------------------
// <copyright file="Interval.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    using System;

    /// <summary>
    /// class that represents a numeric interval.
    /// </summary>
    public class Interval
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Interval"/> class.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="upper">upper bound</param>
        public Interval(int lower, int upper)
            : this(Math.Min(lower, upper), true, Math.Max(lower, upper), true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interval"/> class.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="includeLower">include lower bound</param>
        /// <param name="upper">upper bound</param>
        /// <param name="includeUpper">include upper bound</param>
        public Interval(int lower, bool includeLower, int upper, bool includeUpper)
        {
            if (lower > upper)
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
        public int LowerBound { get; }

        /// <summary>
        /// Gets the upper bound.
        /// </summary>
        public int UpperBound { get; } = int.MaxValue;

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
        public bool IncludeLower { get; } = true;

        /// <summary>
        /// Gets a value indicating whether to include the upper bound
        /// </summary>
        public bool IncludeUpper { get; } = true;

        /// <summary>
        /// Checks if the supplied value is within the interval range
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true / false if the value is wthin the interval</returns>
        public bool Within(int value)
        {
            if (this.IsInclusive)
            {
                return (value >= this.LowerBound) && (value <= this.UpperBound);
            }
            else if (this.IncludeLower && !this.IncludeUpper)
            {
                return (value >= this.LowerBound) && (value < this.UpperBound);
            }
            else if (!this.IncludeLower && this.IncludeUpper)
            {
                return (value > this.LowerBound) && (value <= this.UpperBound);
            }
            else
            {
                return (value > this.LowerBound) && (value < this.UpperBound);
            }
        }
    }
}