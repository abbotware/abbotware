// -----------------------------------------------------------------------
// <copyright file="StatisticCollection{TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generalized statistic information
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    public class StatisticCollection<TKey> : IReadOnlyStatisticCollection<TKey>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, uint> counts = new();

        /// <inheritdoc/>
        public uint Total
        {
            get
            {
                return (uint)this.counts.Values.Sum(x => x);
            }
        }

        /// <inheritdoc/>
        public uint Count(TKey key)
        {
            if (this.counts.ContainsKey(key))
            {
                return this.counts[key];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Increments a key by 1
        /// </summary>
        /// <param name="key">key value</param>
        public void Increment(TKey key)
        {
            if (this.counts.ContainsKey(key))
            {
                ++this.counts[key];
            }
            else
            {
                this.counts.Add(key, 1);
            }
        }

        /// <summary>
        /// Merges the source statistics into this statistics
        /// </summary>
        /// <param name="source">merge source data into this</param>
        public void MergeWith(StatisticCollection<TKey> source)
        {
            source = Arguments.EnsureNotNull(source, nameof(source));

            foreach (var kvp in source.counts)
            {
                if (this.counts.ContainsKey(kvp.Key))
                {
                    this.counts[kvp.Key] += kvp.Value;
                }
                else
                {
                    this.counts.Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, uint>> GetEnumerator()
        {
            return this.counts.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerable ienumerable = this.counts;

            return ienumerable.GetEnumerator();
        }
    }
}
