// -----------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;

    /// <summary>
    ///     Helper methods related to enums
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Parses a string into an enum value
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">string version of enum</param>
        /// <returns>enum value</returns>
        public static TEnum ParseExact<TEnum>(string value)
            where TEnum : struct, Enum
        {
            if (!Enum.TryParse(value, false, out TEnum parsed))
            {
                throw new ArgumentException($"{value} is not valid for enum:{typeof(TEnum).Name}");
            }

            return parsed;
        }
    }
}