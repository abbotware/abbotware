// -----------------------------------------------------------------------
// <copyright file="IntervalLookup{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core.Math;

    /// <summary>
    /// Interval lookup collection
    /// </summary>
    /// <typeparam name="T">type of interval item</typeparam>
    public class IntervalLookup<T> : IReadOnlyIntervalLookup<T>
    {
        private readonly List<IntervalBucket<T>> ranges = new List<IntervalBucket<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="IntervalLookup{T}"/> class.
        /// </summary>
        public IntervalLookup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntervalLookup{T}"/> class.
        /// </summary>
        /// <param name="ranges">ranges to initialize lookup with</param>
        public IntervalLookup(IEnumerable<IntervalBucket<T>> ranges)
        {
            ranges = Arguments.EnsureNotNull(ranges, nameof(ranges));

            foreach (var rangeToAdd in ranges)
            {
                this.Add(rangeToAdd);
            }
        }

        /// <summary>
        /// Adds an interval to the lookup
        /// </summary>
        /// <param name="lower">lower bound</param>
        /// <param name="upper">upper bound</param>
        /// <param name="item">item</param>
        public void Add(int lower, int upper, T item)
        {
            var i = new IntervalBucket<T>(lower, upper, item);
            this.Add(i);
        }

        /// <summary>
        /// Adds an interval to the lookup
        /// </summary>
        /// <param name="interval">interval range</param>
        /// <param name="item">item</param>
        public void Add(Interval interval, T item)
        {
            interval = Arguments.EnsureNotNull(interval, nameof(interval));

            var i = new IntervalBucket<T>(interval.LowerBound, interval.UpperBound, item);
            this.Add(i);
        }

        /// <summary>
        /// Adds to the interval lookup
        /// </summary>
        /// <param name="toAdd">interval to add</param>
        public void Add(IntervalBucket<T> toAdd)
        {
            toAdd = Arguments.EnsureNotNull(toAdd, nameof(toAdd));

            // This validates to make sure the ranges don't overlap
            foreach (var existingRange in this.ranges)
            {
                if (existingRange.Within(toAdd.LowerBound))
                {
                    throw new InvalidOperationException("Ranges overlap");
                }

                if (existingRange.Within(toAdd.UpperBound))
                {
                    throw new InvalidOperationException("Ranges overlap");
                }
            }

            this.ranges.Add(toAdd);
        }

        /// <inheritdoc/>
        public T Lookup(int value)
        {
            // Brute force works fine for a very small amount of ranges!!
            var found = this.ranges.SingleOrDefault(x => x.Within(value));

            if (found == null)
            {
                throw new IndexOutOfRangeException($"no range found for value:{value}");
            }

            return found.Item;
        }

        /// <inheritdoc/>
        public bool ContainedWithin(int value)
        {
            var found = this.ranges.SingleOrDefault(x => x.Within(value));

            return found != null;
        }
    }
}