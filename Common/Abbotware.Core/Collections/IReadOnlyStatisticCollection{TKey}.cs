// -----------------------------------------------------------------------
// <copyright file="IReadOnlyStatisticCollection{TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generalized statistic information (ReadOnly)
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    public interface IReadOnlyStatisticCollection<TKey> : IEnumerable<KeyValuePair<TKey, uint>>
        where TKey : notnull
    {
        /// <summary>
        /// Gets the aggregate total of all values
        /// </summary>
        uint Total { get; }

        /// <summary>
        /// Gets the count for a specific key
        /// </summary>
        /// <param name="key">key value</param>
        /// <returns>count</returns>
        uint Count(TKey key);
    }
}