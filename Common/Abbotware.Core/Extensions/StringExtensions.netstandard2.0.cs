// -----------------------------------------------------------------------
// <copyright file="StringExtensions.netstandard2.0.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;

    /// <summary>
    ///     String Extension methods
    /// </summary>
    public static partial class StringExtensions
    {
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
        public static bool IsBlank(this string? text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// shortcut for !string.IsNullOrWhiteSpace
        /// </summary>
        /// <param name="text">string to extend</param>
        /// <returns>true/false if contains</returns>
        public static bool IsNotBlank(this string? text)
        {
            return !IsBlank(text);
        }
    }
}