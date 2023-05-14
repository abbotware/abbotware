// -----------------------------------------------------------------------
// <copyright file="VLookup{T1,T2,T3,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    ///     VLookup-like class to find a value based on mulitple keys
    /// </summary>
    /// <typeparam name="T1">level 1 key type</typeparam>
    /// <typeparam name="T2">level 2 key type</typeparam>
    /// <typeparam name="T3">level 3 key type</typeparam>
    /// <typeparam name="TValue">value type</typeparam>
    public class VLookup<T1, T2, T3, TValue> : IEditableLookup<T1, T2, T3, TValue>
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        /// <summary>
        ///     internal dictionaries of keys / value
        /// </summary>
        private readonly ConcurrentDictionary<T1, ConcurrentDictionary<T2, ConcurrentDictionary<T3, TValue>>> values = new();

        /// <summary>
        ///     internal counter
        /// </summary>
        private volatile int counter;

        /// <inheritdoc />
        public long Count => this.counter;

        /// <inheritdoc />
        public TValue this[T1 key1, T2 key2, T3 key3]
        {
            get { return this.Lookup(key1, key2, key3); }
        }

        /// <inheritdoc />
        public void Add(T1 key1, T2 key2, T3 key3, TValue value)
        {
            var level2 = this.values.GetOrAdd(key1, _ => new ConcurrentDictionary<T2, ConcurrentDictionary<T3, TValue>>());

            var level3 = level2.GetOrAdd(key2, _ => new ConcurrentDictionary<T3, TValue>());

            level3.AddOrUpdate(key3, k => value, (_, _) => throw new InvalidOperationException($"Duplicate Key: {key1} {key2} {key3} : {value}"));

            Interlocked.Increment(ref this.counter);
        }

        /// <inheritdoc />
        public IEnumerable<T1> Level1()
        {
            return this.values.Keys.ToList();
        }

        /// <inheritdoc />
        public IEnumerable<T2> Level2(T1 key1)
        {
            if (!this.values.TryGetValue(key1, out var level2))
            {
                throw new KeyNotFoundException($"Level 1 Key: {key1} not found");
            }

            return level2.Keys.ToList();
        }

        /// <inheritdoc />
        public IEnumerable<T3> Level3(T1 key1, T2 key2)
        {
            if (!this.values.TryGetValue(key1, out var level2))
            {
                throw new KeyNotFoundException($"Level 1 Key: {key1} not found");
            }

            if (!level2.TryGetValue(key2, out var level3))
            {
                throw new KeyNotFoundException($"Level 2 Key: {key2} not found (key1:{key1})");
            }

            return level3.Keys.ToList();
        }

        /// <inheritdoc />
        public TValue Lookup(T1 key1, T2 key2, T3 key3)
        {
            return this.values[key1][key2][key3];
        }
    }
}