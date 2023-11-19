// -----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Extensions methods for Enum
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the text set in the EnumMember attribute: [EnumMember(Value="text")]
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="enumeration">enum value</param>
        /// <returns>value or enum</returns>
        public static string GetEnumMemberValue<T>(this T enumeration)
            where T : notnull, Enum
        {
            var t = GetEnumTypeOrThrow<T>();

            var fi = t.GetField(enumeration.ToString());

            var a = fi?.GetCustomAttribute<EnumMemberAttribute>(false);

            if (a == null)
            {
                return enumeration.ToString();
            }

            return a.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets the text set in the description attribute: [Description("test")]
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="enumeration">enum value</param>
        /// <returns>description or enum</returns>
        public static string GetDescription<T>(this T enumeration)
            where T : notnull, Enum
        {
            var t = GetEnumTypeOrThrow<T>();

            var mi = t.GetMember(enumeration.ToString());

            if (mi.Length == 0)
            {
                return enumeration.ToString();
            }

            var a = mi.First()
                .GetCustomAttribute<DescriptionAttribute>(false);

            if (a == null)
            {
                return enumeration.ToString();
            }

            return a.Description;
        }

        private static Type GetEnumTypeOrThrow<T>()
            where T : notnull, Enum
        {
            var t = typeof(T);

            if (!t.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return t;
        }
    }
}