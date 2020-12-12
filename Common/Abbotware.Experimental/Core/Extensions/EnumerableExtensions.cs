// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions methods for Enum
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns an empty enumerable if the source is null
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="source">source enumerable</param>
        /// <returns>non-null</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}