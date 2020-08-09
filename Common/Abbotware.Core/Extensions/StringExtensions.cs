// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;

    /// <summary>
    ///     String Extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts a string to an enum
        /// </summary>
        /// <typeparam name="T">type of enum</typeparam>
        /// <param name="value">string value</param>
        /// <returns>enum value</returns>
        public static T ToEnum<T>(this string value)
        {
            Arguments.NotNull(value, nameof(value));

            return ToEnum<T>(value, true);
        }

        /// <summary>
        ///     Converts a string to an enum
        /// </summary>
        /// <typeparam name="T">type of enum</typeparam>
        /// <param name="value">string value</param>
        /// <param name="ignoreCase">indicates whethere or not to ignore case</param>
        /// <returns>enum value</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
        {
            Arguments.NotNull(value, nameof(value));

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// Contains with string comparision
        /// </summary>
        /// <param name="text">string to extend</param>
        /// <param name="value">value to serach</param>
        /// <param name="stringComparison">comparison type</param>
        /// <returns>true/false if contains</returns>
        public static bool ContainsCaseInsensitive(this string text, string value, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            text = Arguments.EnsureNotNull(text, nameof(text));

            return text.IndexOf(value, stringComparison) >= 0;
        }

        /// <summary>
        /// shortcut for string.IsNullOrWhiteSpace
        /// </summary>
        /// <param name="text">string to extend</param>
        /// <returns>true/false if contains</returns>
        public static bool IsBlank(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// shortcut for !string.IsNullOrWhiteSpace
        /// </summary>
        /// <param name="text">string to extend</param>
        /// <returns>true/false if contains</returns>
        public static bool IsNotBlank(this string text)
        {
            return !IsBlank(text);
        }
    }
}