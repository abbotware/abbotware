// -----------------------------------------------------------------------
// <copyright file="TimeSeriesCollection{TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     Time series data set
    /// </summary>
    /// <typeparam name="TValue">value of time point</typeparam>
    public class TimeSeriesCollection<TValue> : ITimeSeriesCollection<TValue>
    {
        /// <summary>
        ///     internal data set cache
        /// </summary>
        private readonly SortedList<DateTimeOffset, ITimeSeriesValue<TValue>> data = new();

        /// <inheritdoc />
        public IEnumerator<ITimeSeriesValue<TValue>> GetEnumerator()
        {
            return this.data.Values.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Adds a point to the time series collection
        /// </summary>
        /// <param name="time">time</param>
        /// <param name="valueY">value</param>
        public void Add(DateTimeOffset time, TValue valueY)
        {
            var d = new TimeSeriesValue<TValue>
            { X = time, Y = valueY };

            this.data.Add(d.X, d);
        }

        /// <summary>
        /// checks if the key is in the collection
        /// </summary>
        /// <param name="time">time</param>
        /// <returns>true if contained in collection</returns>
        public bool ContainsKey(DateTimeOffset time)
        {
            return this.data.ContainsKey(time);
        }
    }
}