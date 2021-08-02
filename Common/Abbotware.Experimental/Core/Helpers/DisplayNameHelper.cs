// -----------------------------------------------------------------------
// <copyright file="DisplayNameHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///     Helper methods related to enums
    /// </summary>
    public static class DisplayNameHelper
    {
        /// <summary>
        /// Gets the display name value of the enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">value of enum</param>
        /// <returns>enum as string, or display name valule</returns>
        public static string GetDisplayName<TEnum>(TEnum? value)
            where TEnum : struct
        {
            if (value == null)
            {
                return string.Empty;
            }

            var valueString = value.ToString();

            var t = value.GetType();

            if (!t.IsEnum)
            {
                throw new InvalidOperationException($"{t.Name} is not an enum");
            }

            return t.GetMember(valueString)
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName() ?? valueString;
        }

        /// <summary>
        /// Gets the display name value of the enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">value of enum</param>
        /// <returns>enum as string, or display name value</returns>
        public static string GetDisplayName<TEnum>(TEnum value)
            where TEnum : struct
        {
            return GetDisplayName((TEnum?)value);
        }
    }
}