// -----------------------------------------------------------------------
// <copyright file="DateTimeOffsetExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///     DateTimeOffset extensions
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        ///     Converts a DateTimeOffset to UNIX time
        /// </summary>
        /// <param name="extendedObject">DateTime object</param>
        /// <returns>converted UNIX time</returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset extendedObject)
        {
            return extendedObject.UtcDateTime.ToUnixTimeSeconds();
        }

        /// <summary>
        ///     Converts UNIX time to a DateTimeOffset
        /// </summary>
        /// <param name="extendedObject">UNIX time value</param>
        /// <returns>converted DateTime</returns>
        public static DateTimeOffset DateTimeOffsetFromUnixTimeSeconds(this long extendedObject)
        {
            return extendedObject.FromUnixTimeSeconds();
        }

        /// <summary>
        ///     Determins if the given DateTimeOffset is between the two supplied DateTimeOffset values
        /// </summary>
        /// <param name="extended">the time being checked</param>
        /// <param name="time1">time boundary 1</param>
        /// <param name="time2">time boundary 2</param>
        /// <returns>true if self is between time1 and time2</returns>
        public static bool Between(this DateTimeOffset extended, DateTimeOffset time1, DateTimeOffset time2)
        {
            if (time1 <= time2)
            {
                if (extended >= time1 && extended <= time2)
                {
                    return true;
                }
            }
            else
            {
                if (extended >= time1 || extended <= time2)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds working days to a given date (no holiday schedule)
        /// </summary>
        /// <param name="date">the original date</param>
        /// <param name="workingDays">number of working days</param>
        /// <returns>new date</returns>
        public static DateTimeOffset AddWorkdays(this DateTimeOffset date, int workingDays)
        {
            return AddWorkdays(date, workingDays, Enumerable.Empty<DateTimeOffset>());
        }

        /// <summary>
        /// Adds working days to a given date (no holiday schedule)
        /// </summary>
        /// <param name="date">the original date</param>
        /// <param name="workingDays">number of working days</param>
        /// <param name="holidays">list of holiday dates to ignore when adding work days</param>
        /// <returns>new date</returns>
        public static DateTimeOffset AddWorkdays(this DateTimeOffset date, int workingDays, IEnumerable<DateTimeOffset> holidays)
        {
            var holidaysInDateTime = holidays
                .Select(x => x.DateTime)
                .ToList();

            return new DateTimeOffset(date.DateTime.AddWorkdays(workingDays, holidaysInDateTime), date.Offset);
        }

        /// <summary>
        /// Converts DateTimeOffset to an ISO 8601 string
        /// 2018-04-20T19:08:36+00:00
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/ISO_8601</remarks>
        /// <param name="date">the original date</param>
        /// <returns>yyyy-MM-ddTHH:mm:ss.zzz"</returns>
        public static string ToIso8601(this DateTimeOffset date)
        {
            return date.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts DateTimeOffset to an ISO 8601 string with nanoseconds
        /// 2018-04-20T19:08:36.123456789+00:00
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/ISO_8601</remarks>
        /// <param name="date">the original date</param>
        /// <returns>yyyy-MM-ddTHH:mm:ss.fffffffffzzz"</returns>
        public static string ToIso8601WithPrecision(this DateTimeOffset date)
        {
            return date.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
        }
    }
}