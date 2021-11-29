// -----------------------------------------------------------------------
// <copyright file="TimeSpanHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     Helper for TimeSpan formatting
    /// </summary>
    public static class TimeSpanHelper
    {
        /// <summary>
        ///     size limits between each unit
        /// </summary>
        private static readonly TimeSpan[] Limit =
        {
            TimeSpan.FromMinutes(1),
            TimeSpan.FromMinutes(60),
            TimeSpan.FromDays(1),
        };

        /// <summary>
        ///     metric units
        /// </summary>
        private enum Unit
        {
            /// <summary>
            ///     Seconds
            /// </summary>
            Seconds = 0,

            /// <summary>
            ///     Minutes
            /// </summary>
            Minutes = 1,

            /// <summary>
            ///     Hours
            /// </summary>
            Hours = 2,

            /// <summary>
            ///     Days
            /// </summary>
            Days = 3,
        }

        /// <summary>
        ///     formats a TimeSpan in seconds, minutes, etc
        /// </summary>
        /// <param name="ts">time span</param>
        /// <returns>formatted byte string</returns>
        public static string ToString(TimeSpan? ts)
        {
            if (ts == null)
            {
                return "n/a";
            }

            var timeSpan = ts.Value;

            var unit = DetermineUnit(timeSpan);

            var formatString = @"d\.hh\:mm\:ss";
            var suffix = string.Empty;

            switch (unit)
            {
                case Unit.Seconds:
                    formatString = "%s";
                    suffix = "s";
                    break;
                case Unit.Minutes:
                    formatString = "%m";
                    suffix = " Min";

                    break;
                case Unit.Hours:
                    {
                        formatString = "%h";
                        suffix = " Hours";

                        var remainder = timeSpan.Minutes / 60d;

                        if (remainder > .1)
                        {
                            return FormattableString.Invariant($"{timeSpan.TotalHours:F1}{suffix}");
                        }

                        break;
                    }

                case Unit.Days:
                    {
                        formatString = "%d";
                        suffix = " Days";

                        var remainder = timeSpan.Hours / 24d;

                        if (remainder > .1)
                        {
                            return FormattableString.Invariant($"{timeSpan.TotalDays:F1}{suffix}");
                        }

                        break;
                    }
            }

            return FormattableString.Invariant($"{timeSpan.ToString(formatString, CultureInfo.InvariantCulture)}{suffix}");
        }

        /// <summary>
        ///     given the number, return the unit
        /// </summary>
        /// <param name="timeSpan">time span</param>
        /// <returns>unit type</returns>
        private static Unit DetermineUnit(TimeSpan timeSpan)
        {
            if (timeSpan < Limit[(int)Unit.Seconds])
            {
                return Unit.Seconds;
            }

            if (Limit[(int)Unit.Seconds] <= timeSpan && timeSpan <= Limit[(int)Unit.Minutes])
            {
                return Unit.Minutes;
            }

            if (Limit[(int)Unit.Minutes] <= timeSpan && timeSpan <= Limit[(int)Unit.Hours])
            {
                return Unit.Hours;
            }

            return Unit.Days;
        }
    }
}