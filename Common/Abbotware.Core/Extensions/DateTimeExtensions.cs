// -----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
    ///     DateTime extensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// string format for RFC 822 date time
        /// </summary>
        private static readonly string Rfc822Format = InitRfc822FormatString();

        /// <summary>
        ///     Value used for UNIX time conversions
        /// </summary>
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     Converts a DateTime to UNIX time
        /// </summary>
        /// <param name="extendedObject">DateTime object</param>
        /// <returns>converted UNIX time</returns>
        public static long ToUnixTimeSeconds(this DateTime extendedObject)
        {
            var span = extendedObject - DateTimeExtensions.UnixEpoch;
            return (long)span.TotalSeconds;
        }

        /// <summary>
        ///     Converts UNIX time to a DateTime
        /// </summary>
        /// <param name="extendedObject">UNIX time value</param>
        /// <returns>converted DateTime</returns>
        public static DateTime FromUnixTimeSeconds(this long extendedObject)
        {
            return DateTimeExtensions.UnixEpoch.AddSeconds(extendedObject)
                .ToUniversalTime();
        }

        /// <summary>
        ///     Determins if the given DateTime is between the two supplied DateTime values
        /// </summary>
        /// <param name="extended">the time being checked</param>
        /// <param name="time1">time boundary 1</param>
        /// <param name="time2">time boundary 2</param>
        /// <returns>true if self is between time1 and time2</returns>
        public static bool Between(this DateTime extended, DateTime time1, DateTime time2)
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
        public static DateTime AddWorkdays(this DateTime date, int workingDays)
        {
            return AddWorkdays(date, workingDays, Enumerable.Empty<DateTime>());
        }

        /// <summary>
        /// Adds working days to a given date (no holiday schedule)
        /// </summary>
        /// <param name="date">the original date</param>
        /// <param name="workingDays">number of working days</param>
        /// <param name="holidays">list of holiday dates to ignore when adding work days</param>
        /// <returns>new date</returns>
        public static DateTime AddWorkdays(this DateTime date, int workingDays, IEnumerable<DateTime> holidays)
        {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;

            while (workingDays != 0)
            {
                newDate = newDate.AddDays(direction);

                if (newDate.DayOfWeek == DayOfWeek.Saturday || newDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                if (holidays.Contains(newDate))
                {
                    continue;
                }

                workingDays -= direction;
            }

            return newDate;
        }

        /// <summary>
        /// converts the date to RFC 822 format
        /// </summary>
        /// <param name="extendedObject">DateTime object</param>
        /// <returns>formatted string</returns>
        public static string ToRfc822Format(this DateTime extendedObject)
        {
            return extendedObject.ToLocalTime().ToString(Rfc822Format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Initializes static members of the <see cref="DateTimeExtensions"/> class.
        /// </summary>
        /// <returns>format string</returns>
        private static string InitRfc822FormatString()
        {
            TimeSpan offset = DateTimeOffset.Now.Offset;

            var sign = (offset.Ticks < 0) ? '-' : '+';
            var hours = Math.Abs(offset.Hours).ToString("D2", CultureInfo.InvariantCulture);
            var minutes = Math.Abs(offset.Minutes).ToString("D2", CultureInfo.InvariantCulture);

            return FormattableString.Invariant($"ddd, dd MMM yyyy HH:mm:ss {sign}{hours}{minutes}");
        }
    }
}