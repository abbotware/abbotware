// -----------------------------------------------------------------------
// <copyright file="StringHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System.Linq;

    /// <summary>
    ///     String Helper methods
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        ///     joins a string without empty entries
        /// </summary>
        /// <param name="values">values to join</param>
        /// <returns>enum value</returns>
        public static string JoinWithoutEmpty(params string?[] values)
        {
            if (values == null)
            {
                return string.Empty;
            }

            return string.Join(",", values.Where(s => !string.IsNullOrEmpty(s)));
        }
    }
}