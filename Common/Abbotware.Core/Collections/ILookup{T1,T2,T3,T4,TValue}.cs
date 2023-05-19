// -----------------------------------------------------------------------
// <copyright file="ILookup{T1,T2,T3,T4,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for a readonly 4-key/level look up structure
    /// </summary>
    /// <typeparam name="T1">key for level 1</typeparam>
    /// <typeparam name="T2">key for level 2</typeparam>
    /// <typeparam name="T3">key for level 3</typeparam>
    /// <typeparam name="T4">key for level 4</typeparam>
    /// <typeparam name="TValue">value</typeparam>
    public interface ILookup<T1, T2, T3, T4, out TValue>
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        /// <summary>
        ///     Gets the count of all values stored in lookup
        /// </summary>
        long Count { get; }

        /// <summary>
        /// lookup function to return the value at the specified key
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <param name="key2">key for level 2</param>
        /// <param name="key3">key for level 3</param>
        /// <param name="key4">key for level 4</param>
        /// <returns>value stored in lookup</returns>
        TValue this[T1 key1, T2 key2, T3 key3, T4 key4] { get; }

        /// <summary>
        ///     Gets all keys stored in level 1
        /// </summary>
        /// <returns>list of keys</returns>
        IEnumerable<T1> Level1();

        /// <summary>
        ///     Gets all keys stored within a particular key at level 2
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <returns>list of keys</returns>
        IEnumerable<T2> Level2(T1 key1);

        /// <summary>
        ///     Gets all keys stored within a particular key at level 3
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <param name="key2">key for level 2</param>
        /// <returns>list of keys</returns>
        IEnumerable<T3> Level3(T1 key1, T2 key2);

        /// <summary>
        ///     Gets all keys stored within a particular key at level 4
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <param name="key2">key for level 2</param>
        /// <param name="key3">key for level 3</param>
        /// <returns>list of keys</returns>
        IEnumerable<T4> Level4(T1 key1, T2 key2, T3 key3);

        /// <summary>
        /// lookup function to return the value at the specified key
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <param name="key2">key for level 2</param>
        /// <param name="key3">key for level 3</param>
        /// <param name="key4">key for level 4</param>
        /// <returns>value stored in lookup</returns>
        TValue Lookup(T1 key1, T2 key2, T3 key3, T4 key4);

        /// <summary>
        /// returns true if the key is present in the look up
        /// </summary>
        /// <param name="key1">key for level 1</param>
        /// <param name="key2">key for level 2</param>
        /// <param name="key3">key for level 3</param>
        /// <param name="key4">key for level 4</param>
        /// <returns>true/false if present</returns>
        bool ContainsKey(T1 key1, T2 key2, T3 key3, T4 key4);
    }
}