﻿// -----------------------------------------------------------------------
// <copyright file="IEditableLookUp{T1,T2,T3,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    /// <summary>
    ///     Interface for an editable 3-key/level look up structure
    /// </summary>
    /// <typeparam name="T1">key for level 1</typeparam>
    /// <typeparam name="T2">key for level 2</typeparam>
    /// <typeparam name="T3">key for level 3</typeparam>
    /// <typeparam name="TValue">value</typeparam>
    public interface IEditableLookup<T1, T2, T3, TValue> : ILookup<T1, T2, T3, TValue>
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        /// <summary>
        /// Adds a value to the lookup
        /// </summary>
        /// <param name="key1">level 1 key</param>
        /// <param name="key2">level 2 key</param>
        /// <param name="key3">level 3 key</param>
        /// <param name="value">value to add</param>
        void Add(T1 key1, T2 key2, T3 key3, TValue value);
    }
}