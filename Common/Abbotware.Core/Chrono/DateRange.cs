// -----------------------------------------------------------------------
// <copyright file="DateRange.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;

    /// <summary>
    ///     Helper class that contains a start and end date range
    /// </summary>
    public class DateRange
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DateRange" /> class.
        /// </summary>
        /// <param name="lowerBound">begin date</param>
        /// <param name="upperBound">end date</param>
        public DateRange(DateTimeOffset lowerBound, DateTimeOffset upperBound)
        {
            if (lowerBound < upperBound)
            {
                this.LowerBound = lowerBound;
                this.UpperBound = upperBound;
            }
            else
            {
                this.UpperBound = lowerBound;
                this.LowerBound = upperBound;
            }
        }

        /// <summary>
        ///     Gets the begining date
        /// </summary>
        public DateTimeOffset LowerBound { get; }

        /// <summary>
        ///     Gets the ending date
        /// </summary>
        public DateTimeOffset UpperBound { get; }

        /// <summary>
        /// Gets a value indicating whether or not to include the upper bound (Less Than or Equal)
        /// </summary>
        public bool IncludeUpperBound { get; } = true;

        /// <summary>
        /// Gets a value indicating whether or not to include the lower bound (Greater Than or Equal)
        /// </summary>
        public bool IncludeLowerBound { get; } = true;

        /// <summary>
        ///     Factor method to create
        /// </summary>
        /// <param name="start">begin date</param>
        /// <param name="end">end date</param>
        /// <returns>date range</returns>
        public static DateRange Between(DateTimeOffset start, DateTimeOffset end)
        {
            return new DateRange(start, end);
        }

        /// <summary>
        ///     Factor method to create date range for last 24 hours
        /// </summary>
        /// <returns>date range</returns>
        public static DateRange Last24Hours()
        {
            var t = DateTimeOffset.Now;

            return new DateRange(t.AddDays(-1), t);
        }
    }
}