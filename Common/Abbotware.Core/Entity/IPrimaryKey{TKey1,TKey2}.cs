// -----------------------------------------------------------------------
// <copyright file="IPrimaryKey{TKey1,TKey2}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Entity
{
    using System;

    /// <summary>
    /// Interface for a strongly typed 2 part composite primary key
    /// </summary>
    /// <typeparam name="TKey1">first part of the key</typeparam>
    /// <typeparam name="TKey2">second part of the key</typeparam>
    public interface IPrimaryKey<TKey1, TKey2> : IPrimaryKey
        where TKey1 : IEquatable<TKey1>, IComparable<TKey1>, IComparable
        where TKey2 : IEquatable<TKey2>, IComparable<TKey2>, IComparable
    {
        /// <summary>
        /// Gets the first part of the composite primary key
        /// </summary>
        TKey1 Value1 { get; }

        /// <summary>
        /// Gets the second part of the composite primary key
        /// </summary>
        TKey2 Value2 { get; }
    }
}