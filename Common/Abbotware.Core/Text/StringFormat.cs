// -----------------------------------------------------------------------
// <copyright file="StringFormat.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Text
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///     Helper for string formatting data
    /// </summary>
    public static class StringFormat
    {
        /// <summary>
        ///     size limits between each unit
        /// </summary>
        private static readonly float[] Limit =
        {
            1,
            1_000,
            1_000_000,
            1_000_000_000,
            1_000_000_000_000,
        };

        /// <summary>
        ///     unit suffixes
        /// </summary>
        private static readonly string[] Suffix =
        {
            "B",
            "KB",
            "MB",
            "GB",
            "TB",
        };

        /// <summary>
        ///     metric units
        /// </summary>
        private enum Unit
        {
            /// <summary>
            ///     Bytes
            /// </summary>
            Byte = 0,

            /// <summary>
            ///     KiloBytes
            /// </summary>
            Kilobyte = 1,

            /// <summary>
            ///     MegaBytes
            /// </summary>
            Megabyte = 2,

            /// <summary>
            ///     GigaBytes
            /// </summary>
            Gigabyte = 3,

            /// <summary>
            ///     TeraBytes
            /// </summary>
            Terabyte = 4,
        }

        /// <summary>
        ///     formats a bytes in B, MB, GB, TB
        /// </summary>
        /// <param name="bytes">size in bytes</param>
        /// <returns>formatted byte string</returns>
        public static string Bytes(long bytes)
        {
            var unit = DetermineUnit(bytes);

            var suffix = Suffix[(int)unit];

            var display = Truncate(bytes, unit, 2);

            return FormattableString.Invariant($"{display} {suffix}");
        }

        /// <summary>
        ///     Truncates the number for the given unit
        /// </summary>
        /// <param name="bytes">size in bytes</param>
        /// <param name="unit">unit type</param>
        /// <param name="numberDecimalDigits">number of decimals</param>
        /// <returns>truncate number as a string</returns>
        private static string Truncate(long bytes, Unit unit, int numberDecimalDigits)
        {
            var display = (bytes / Limit[(int)unit]) * 100;

            var rounded = (float)Math.Round(display);

            display = rounded / 100f;

            var nfi = new CultureInfo("en-US", false).NumberFormat;

            if (unit == Unit.Byte)
            {
                nfi.NumberDecimalDigits = 0;
            }
            else
            {
                nfi.NumberDecimalDigits = numberDecimalDigits;
            }

            var temp = display.ToString("n", nfi);

            var parts = temp.Split('.');

            if (parts.Length > 1)
            {
                if (parts[1].All(x => x == '0'))
                {
                    return parts[0];
                }

                return temp.TrimEnd('0');
            }

            return temp;
        }

        /// <summary>
        ///     given number of bytes, return the unit
        /// </summary>
        /// <param name="bytes">size in bytes</param>
        /// <returns>unit type</returns>
        private static Unit DetermineUnit(long bytes)
        {
            if (bytes < Limit[(int)Unit.Kilobyte])
            {
                return Unit.Byte;
            }

            if (Limit[(int)Unit.Kilobyte] <= bytes && bytes <= Limit[(int)Unit.Megabyte])
            {
                return Unit.Kilobyte;
            }

            if (Limit[(int)Unit.Megabyte] <= bytes && bytes <= Limit[(int)Unit.Gigabyte])
            {
                return Unit.Megabyte;
            }

            if (Limit[(int)Unit.Gigabyte] <= bytes && bytes <= Limit[(int)Unit.Terabyte])
            {
                return Unit.Gigabyte;
            }

            return Unit.Terabyte;
        }
    }
}