// -----------------------------------------------------------------------
// <copyright file="VLookup{T1,T2,T3,T4,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
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
    /// <typeparam name="TValue">value type</typeparam>
    public class VLookup<T1, T2, T3, T4, TValue> : IEditableLookup<T1, T2, T3, T4, TValue>
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        /// <summary>
        ///     internal dictionaries of keys / value
        /// </summary>
        private readonly Dictionary<T1, Dictionary<T2, Dictionary<T3, Dictionary<T4, TValue>>>> values = new();

        /// <summary>
        ///     internal counter
        /// </summary>
        private volatile int counter;

        /// <inheritdoc />
        public long Count => this.counter;

        /// <inheritdoc />
        public TValue this[T1 key1, T2 key2, T3 key3, T4 key4] => this.Lookup(key1, key2, key3, key4);

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
        public TValue Lookup(T1 key1, T2 key2, T3 key3, T4 key4)
        {
            return this.values[key1][key2][key3][key4];
        }

        /// <inheritdoc />
        public void Add(T1 key1, T2 key2, T3 key3, T4 key4, TValue value)
        {
            if (!this.values.ContainsKey(key1))
            {
                this.values.Add(key1, new Dictionary<T2, Dictionary<T3, Dictionary<T4, TValue>>>());
            }

            if (!this.values[key1].ContainsKey(key2))
            {
                this.values[key1].Add(key2, new Dictionary<T3, Dictionary<T4, TValue>>());
            }

            if (!this.values[key1][key2].ContainsKey(key3))
            {
                this.values[key1][key2].Add(key3, new Dictionary<T4, TValue>());
            }

            if (!this.values[key1][key2][key3].ContainsKey(key4))
            {
                var message = $"Duplicate Key: {key1} {key2} {key3} {key4} : {value}";
                throw new InvalidOperationException(message);
            }

            this.values[key1][key2][key3].Add(key4, value);

            Interlocked.Increment(ref this.counter);
        }

        /// <inheritdoc />
        public bool ContainsKey(T1 key1, T2 key2, T3 key3, T4 key4)
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

            return true;
        }
    }
}