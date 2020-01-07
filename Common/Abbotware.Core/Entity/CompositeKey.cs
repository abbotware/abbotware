// -----------------------------------------------------------------------
// <copyright file="CompositeKey.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Entity
{
    using System;

    /// <summary>
    /// POCO class for a 2 part composite primary key
    /// </summary>
    /// <typeparam name="TKey1">key part 1 type</typeparam>
    /// <typeparam name="TKey2">key part 2 type</typeparam>
    public sealed class CompositeKey<TKey1, TKey2> : Tuple<TKey1, TKey2>, IPrimaryKey<TKey1, TKey2>
        where TKey1 : IEquatable<TKey1>, IComparable<TKey1>, IComparable
        where TKey2 : IEquatable<TKey2>, IComparable<TKey2>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeKey{TKey1, TKey2}"/> class.
        /// </summary>
        /// <param name="key1">key 1 value</param>
        /// <param name="key2">key 2 value</param>
        public CompositeKey(TKey1 key1, TKey2 key2)
            : base(key1, key2)
        {
        }

        /// <inheritdoc/>
        TKey1 IPrimaryKey<TKey1, TKey2>.Value1 => this.Item1;

        /// <inheritdoc/>
        TKey2 IPrimaryKey<TKey1, TKey2>.Value2 => this.Item2;

        /// <inheritdoc/>
        public object[] ToEntityFindKeyValues() => new object[] { this.Item1, this.Item2 };
    }
}