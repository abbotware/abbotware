// -----------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using Abbotware.Core.Diagnostics;

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

        /// <summary>
        /// Gets the EnumMemberAttribute value of the enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">value of enum</param>
        /// <returns>enum as string, or EnumMemberAttribute value</returns>
        public static string GetEnumMemberValue<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                return string.Empty;
            }

            var valueString = value.ToString();

            var t = value.GetType();

            var member = t.GetMember(valueString).
                FirstOrDefault();

            if (member == null)
            {
                throw new InvalidOperationException($"{value} not part of enum");
            }

            var attribute = ReflectionHelper.SingleOrDefaultAttribute<EnumMemberAttribute>(member);

            return attribute.Value ?? valueString;
        }

        /// <summary>
        /// Gets the display name value of the enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">value of enum</param>
        /// <returns>enum as string, or display name value</returns>
        public static string GetEnumMemberValue<TEnum>(TEnum value)
            where TEnum : struct, Enum
        {
            return GetEnumMemberValue((TEnum?)value);
        }
    }
}