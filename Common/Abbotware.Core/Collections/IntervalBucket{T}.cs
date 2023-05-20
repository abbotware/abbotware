// -----------------------------------------------------------------------
// <copyright file="IntervalBucket{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Collections
{
    using Abbotware.Core.Math;

    /// <summary>
    /// class that represents a numeric interval with an item
    /// </summary>
    /// <typeparam name="T">type of interval item</typeparam>
    /// <param name="Lower">lower bound</param>
    /// <param name="Upper">upper bound</param>
    /// <param name="Item">item to store</param>
    /// <param name="IncludeLower">Gets a value indicating whether to include the upper bound</param>
    /// <param name="IncludeUpper">Gets a value indicating whether to include the lower bound</param>
    public record class IntervalBucket<T>(int Lower, int Upper, T Item, bool IncludeLower, bool IncludeUpper)
        : Interval<int>(Lower, Upper, IncludeLower, IncludeUpper)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntervalBucket{T}"/> class.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="upper">upper bound</param>
        /// <param name="item">item to store</param>
        public IntervalBucket(int lower, int upper, T item)
            : this(lower, upper, item, true, true)
        {
        }
    }
}