// -----------------------------------------------------------------------
// <copyright file="IPrimaryKey{TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Entity
{
    using System;

    /// <summary>
    /// Interface for a strongly typed primary key
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    public interface IPrimaryKey<TKey> : IPrimaryKey
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {
        /// <summary>
        /// Gets the key
        /// </summary>
        TKey Value { get; }
    }
}
