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
    public class IntervalBucket<T> : Interval
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntervalBucket{T}"/> class.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="upper">upper bound</param>
        /// <param name="item">item</param>
        public IntervalBucket(int lower, int upper, T item)
            : this(lower, true, upper, true, item)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntervalBucket{T}"/> class.
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="includeLower">include lower bound</param>
        /// <param name="upper">upper bound</param>
        /// <param name="includeUpper">include upper bound</param>
        /// <param name="item">item</param>
        public IntervalBucket(int lower, bool includeLower, int upper, bool includeUpper, T item)
            : base(lower, includeLower, upper, includeUpper)
        {
            this.Item = item;
        }

        /// <summary>
        /// Gets the item associated with the interval
        /// </summary>
        public T Item { get; }
    }
}