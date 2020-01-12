// -----------------------------------------------------------------------
// <copyright file="DateRange.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    ///     Helper class for creating a date range interval
    /// </summary>
    public static class DateRange
    {
        /// <summary>
        ///     Factory method to create
        /// </summary>
        /// <param name="start">begin date</param>
        /// <param name="end">end date</param>
        /// <returns>date range</returns>
        public static Interval<DateTimeOffset> Between(DateTimeOffset start, DateTimeOffset end)
        {
            return new Interval<DateTimeOffset>(start, end);
        }

        /// <summary>
        ///     Factory method to create date range for last 24 hours
        /// </summary>
        /// <returns>date range</returns>
        public static Interval<DateTimeOffset> Last24Hours()
        {
            var t = DateTimeOffset.Now;

            return new Interval<DateTimeOffset>(t.AddDays(-1), t);
        }
    }
}