// -----------------------------------------------------------------------
// <copyright file="StatisticCollection{TKey1,TKey2}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;

    /// <summary>
    /// Statistics for 2-key (composite)
    /// </summary>
    /// <typeparam name="TKey1">key type 1</typeparam>
    /// <typeparam name="TKey2">key type 2</typeparam>
    public class StatisticCollection<TKey1, TKey2> : StatisticCollection<Tuple<TKey1, TKey2>>
    {
        /// <summary>
        /// Incrememnts the composite key
        /// </summary>
        /// <param name="key1">key1 value</param>
        /// <param name="key2">key2 value</param>
        public void Increment(TKey1 key1, TKey2 key2)
        {
            this.Increment(new Tuple<TKey1, TKey2>(key1, key2));
        }
    }
}
