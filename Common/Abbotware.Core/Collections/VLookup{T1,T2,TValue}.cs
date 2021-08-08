// -----------------------------------------------------------------------
// <copyright file="VLookup{T1,T2,TValue}.cs" company="Abbotware, LLC">
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
    /// <typeparam name="TValue">value type</typeparam>
    public class VLookup<T1, T2, TValue> : IEditableLookup<T1, T2, TValue>
        where T1 : notnull
        where T2 : notnull
    {
        /// <summary>
        ///     internal dictionaries of keys / value
        /// </summary>
        private readonly Dictionary<T1, Dictionary<T2, TValue>> values = new();

        /// <summary>
        ///     internal counter
        /// </summary>
        private volatile int counter;

        /// <inheritdoc />
        public long Count => this.counter;

        /// <inheritdoc />
        public TValue this[T1 key1, T2 key2]
        {
            get { return this.Lookup(key1, key2); }
        }

        /// <inheritdoc />
        public void Add(T1 key1, T2 key2, TValue value)
        {
            if (!this.values.ContainsKey(key1))
            {
                this.values.Add(key1, new Dictionary<T2, TValue>());
            }

            if (this.values[key1].ContainsKey(key2))
            {
                throw new InvalidOperationException($"Duplicate Key: {key1} {key2} : {value}");
            }

            this.values[key1].Add(key2, value);

            Interlocked.Increment(ref this.counter);
        }

        /// <inheritdoc />
        public TValue Lookup(T1 key1, T2 key2)
        {
            return this.values[key1][key2];
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
    }
}