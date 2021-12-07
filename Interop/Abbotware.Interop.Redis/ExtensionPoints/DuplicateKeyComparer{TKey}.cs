// -----------------------------------------------------------------------
// <copyright file="DuplicateKeyComparer{TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.ExtensionPoints
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core;

    /// <summary>
    ///     Use this Comparer with SortedLists or SortedDictionaries, so that it can allow duplicate keys
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    public class DuplicateKeyComparer<TKey> : IComparer<TKey>
        where TKey : IComparable
    {
        /// <inheritdoc />
        public int Compare(TKey? x, TKey? y)
        {
            var z = Arguments.EnsureNotNull(x, nameof(x));

            var result = z.CompareTo(y);

            if (result == 0)
            {
                return 1; // Handle equality as being greater
            }

            return result;
        }
    }
}