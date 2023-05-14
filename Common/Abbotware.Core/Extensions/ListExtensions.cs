// -----------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    ///     Extension methods for IList
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// List equivalent of Array.BinarySearch
        /// </summary>
        /// <typeparam name="T">Type in list</typeparam>
        /// <param name="extended">object to dump</param>
        /// <param name="value">value to search</param>
        /// <param name="comparer">optional comparer</param>
        /// <returns>index when found or -index for closest index</returns>
        /// <remarks>https://stackoverflow.com/questions/967047/how-to-perform-a-binary-search-on-ilistt/2948872#2948872</remarks>
        public static int BinarySearchIndexOf<T>(this IList<T> extended, T value, IComparer<T>? comparer = null)
        {
            extended = Arguments.EnsureNotNull(extended, nameof(extended));

            comparer = comparer ?? Comparer<T>.Default;

            var lower = 0;
            var upper = extended.Count - 1;

            while (lower <= upper)
            {
                var middle = lower + ((upper - lower) / 2);
                var comparisonResult = comparer.Compare(value, extended[middle]);

                if (comparisonResult == 0)
                {
                    return middle;
                }
                else if (comparisonResult < 0)
                {
                    upper = middle - 1;
                }
                else
                {
                    lower = middle + 1;
                }
            }

            return ~lower;
        }
    }
}