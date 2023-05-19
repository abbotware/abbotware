// -----------------------------------------------------------------------
// <copyright file="VLookup{T1,T2,T3,T4,T5,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    ///     VLookup-like class to find a value based on mulitple keys
    /// </summary>
    /// <typeparam name="T1">level 1 key type</typeparam>
    /// <typeparam name="T2">level 2 key type</typeparam>
    /// <typeparam name="T3">level 3 key type</typeparam>
    /// <typeparam name="T4">level 4 key type</typeparam>
    /// <typeparam name="T5">level 5 key type</typeparam>
    /// <typeparam name="TValue">value type</typeparam>
    public class VLookup<T1, T2, T3, T4, T5, TValue> : IEditableLookup<T1, T2, T3, T4, T5, TValue>
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        /// <summary>
        ///     internal dictionaries of keys / value
        /// </summary>
        private readonly ConcurrentDictionary<T1, ConcurrentDictionary<T2, ConcurrentDictionary<T3, ConcurrentDictionary<T4, ConcurrentDictionary<T5, TValue>>>>> values = new();

        /// <summary>
        ///     internal counter
        /// </summary>
        private volatile int counter;

        /// <inheritdoc />
        public long Count => this.counter;

        /// <inheritdoc />
        public TValue this[T1 key1, T2 key2, T3 key3, T4 key4, T5 key5] => this.Lookup(key1, key2, key3, key4, key5);

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
        public IEnumerable<T4> Level4(T1 key1, T2 key2, T3 key3)
        {
            if (!this.values.TryGetValue(key1, out var level2))
            {
                throw new KeyNotFoundException($"Level 1 Key: {key1} not found");
            }

            if (!level2.TryGetValue(key2, out var level3))
            {
                throw new KeyNotFoundException($"Level 2 Key: {key2} not found (key1:{key1})");
            }

            if (!level3.TryGetValue(key3, out var level4))
            {
                throw new KeyNotFoundException($"Level 3 Key: {key3} not found (key1:{key1} key2:{key2})");
            }

            return level4.Keys.ToList();
        }

        /// <inheritdoc />
        public IEnumerable<T5> Level5(T1 key1, T2 key2, T3 key3, T4 key4)
        {
            if (!this.values.TryGetValue(key1, out var level2))
            {
                throw new KeyNotFoundException($"Level 1 Key: {key1} not found");
            }

            if (!level2.TryGetValue(key2, out var level3))
            {
                throw new KeyNotFoundException($"Level 2 Key: {key2} not found (key1:{key1})");
            }

            if (!level3.TryGetValue(key3, out var level4))
            {
                throw new KeyNotFoundException($"Level 3 Key: {key3} not found (key1:{key1} key2:{key2})");
            }

            if (!level4.TryGetValue(key4, out var level5))
            {
                throw new KeyNotFoundException($"Level 4 Key: {key4} not found (key1:{key1} key2:{key2} key3:{key3})");
            }

            return level5.Keys.ToList();
        }

        /// <inheritdoc />
        public TValue Lookup(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5)
        {
            return this.values[key1][key2][key3][key4][key5];
        }

        /// <inheritdoc />
        public void Add(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5, TValue value)
        {
            var level2 = this.values.GetOrAdd(key1, _ => new ConcurrentDictionary<T2, ConcurrentDictionary<T3, ConcurrentDictionary<T4, ConcurrentDictionary<T5, TValue>>>>());

            var level3 = level2.GetOrAdd(key2, _ => new ConcurrentDictionary<T3, ConcurrentDictionary<T4, ConcurrentDictionary<T5, TValue>>>());

            var level4 = level3.GetOrAdd(key3, _ => new ConcurrentDictionary<T4, ConcurrentDictionary<T5, TValue>>());

            var level5 = level4.GetOrAdd(key4, _ => new ConcurrentDictionary<T5, TValue>());

            level5.AddOrUpdate(key5, k => value, (_, _) => throw new InvalidOperationException($"Duplicate Key: {key1} {key2} {key3} {key4} {key5} : {value}"));

            Interlocked.Increment(ref this.counter);
        }

        /// <inheritdoc />
        public bool ContainsKey(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5)
        {
            if (!this.values.ContainsKey(key1))
            {
                return false;
            }

            if (!this.values[key1].ContainsKey(key2))
            {
                return false;
            }

            if (!this.values[key1][key2].ContainsKey(key3))
            {
                return false;
            }

            if (!this.values[key1][key2][key3].ContainsKey(key4))
            {
                return false;
            }

            if (!this.values[key1][key2][key3][key4].ContainsKey(key5))
            {
                return false;
            }

            return true;
        }
    }
}