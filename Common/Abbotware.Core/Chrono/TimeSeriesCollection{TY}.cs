﻿// -----------------------------------------------------------------------
// <copyright file="TimeSeriesCollection{TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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
    /// <typeparam name="TY">value of time point</typeparam>
    public class TimeSeriesCollection<TY> : ITimeSeriesCollection<TY>
        where TY : struct
    {
        /// <summary>
        ///     internal data set cache
        /// </summary>
        private readonly SortedList<DateTimeOffset, ITimeSeriesValue<TY>> data = new();

        /// <inheritdoc />
        public IEnumerator<ITimeSeriesValue<TY>> GetEnumerator()
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
        public void Add(DateTimeOffset time, TY valueY)
        {
            var d = new TimeSeriesValue<TY>(time, valueY);

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