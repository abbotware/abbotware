// -----------------------------------------------------------------------
// <copyright file="ILookUp{T1,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for a readonly 2-key/level look up structure
    /// </summary>
    /// <typeparam name="T1">key for level 1</typeparam>
    /// <typeparam name="TValue">value</typeparam>
    public interface ILookup<T1, out TValue>
        where T1 : notnull
    {
        /// <summary>
        ///     Gets the count of all values stored in lookup
        /// </summary>
        long Count { get; }

        /// <summary>
        ///     lookup function to return the value at the specified key
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <returns>value stored in lookup</returns>
        TValue this[T1 key1] { get; }

        /// <summary>
        ///     lookup function to return the value at the specified key
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <returns>value stored in lookup</returns>
        TValue Lookup(T1 key1);

        /// <summary>
        ///     Gets all keys stored in level 1
        /// </summary>
        /// <returns>list of keys</returns>
        IEnumerable<T1> Level1();
    }
}