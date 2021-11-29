// -----------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Abbotware.Core.Diagnostics;

    /// <summary>
    ///     Helper methods related to enums
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets all the values of the enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <returns>all values of the enum</returns>
        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct, Enum
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

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

            var (s, m) = GetEnumMemberInfo(value);

            var attribute = ReflectionHelper.SingleOrDefaultAttribute<EnumMemberAttribute>(m);

            return attribute?.Value ?? s;
        }

        /// <summary>
        /// Gets a value indicating whether or not the enum is marked obsolete
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">value of enum</param>
        /// <returns>true if obsolete</returns>
        public static bool IsEnumObsolete<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            if (value == null)
            {
                return false;
            }

            var (_, m) = GetEnumMemberInfo(value);

            var a = ReflectionHelper.SingleOrDefaultAttribute<ObsoleteAttribute>(m);

            return a != null;
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

        private static (string Value, MemberInfo MemberInfo) GetEnumMemberInfo<TEnum>(TEnum? value)
            where TEnum : struct, Enum
        {
            var valueString = value.ToString()!;

            var t = value!.GetType();

            var memberInfo = t.GetMember(valueString).
                FirstOrDefault();

            if (memberInfo == null)
            {
                throw new InvalidOperationException($"{value} not part of enum");
            }

            return (valueString, memberInfo);
        }
    }
}