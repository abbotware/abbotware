// -----------------------------------------------------------------------
// <copyright file="UniquePairs{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     maps 2 items to each other as unique pairs
    /// </summary>
    /// <typeparam name="T">item type</typeparam>
    public class UniquePairs<T> : IUniquePairs<T>
    {
        private readonly Dictionary<T, T> pairs = new Dictionary<T, T>();

        /// <inheritdoc/>
        public T Other(T item)
        {
            lock (this.pairs)
            {
                if (this.pairs.TryGetValue(item, out var other))
                {
                    return other;
                }
            }

            throw new KeyNotFoundException();
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            lock (this.pairs)
            {
                return this.pairs.ContainsKey(item);
            }
        }

        /// <inheritdoc/>
        public void Add(T first, T second)
        {
            lock (this.pairs)
            {
                if (this.pairs.ContainsKey(first))
                {
                    throw new InvalidOperationException($"{first} already in collection");
                }

                this.pairs[first] = second;

                if (this.pairs.ContainsKey(second))
                {
                    throw new InvalidOperationException($"{second} already in collection");
                }

                this.pairs[second] = first;
            }
        }

        /// <inheritdoc/>
        public bool TryAdd(T first, T second)
        {
            lock (this.pairs)
            {
                if (this.pairs.ContainsKey(first))
                {
                    return false;
                }

                this.pairs[first] = second;

                if (this.pairs.ContainsKey(second))
                {
                    return false;
                }

                this.pairs[second] = first;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            lock (this.pairs)
            {
                if (!this.pairs.TryGetValue(item, out var other))
                {
                    return false;
                }

                var first = this.pairs.Remove(item);

                var second = this.pairs.Remove(other);

                return first && second;
            }
        }

        /// <inheritdoc/>
        public bool TryRemove(T item, out T other)
        {
            lock (this.pairs)
            {
                if (!this.pairs.TryGetValue(item, out other))
                {
                    return false;
                }

                var first = this.pairs.Remove(item);

                var second = this.pairs.Remove(other);

                return first && second;
            }
        }
    }
}