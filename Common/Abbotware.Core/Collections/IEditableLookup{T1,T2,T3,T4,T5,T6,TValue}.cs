// -----------------------------------------------------------------------
// <copyright file="IEditableLookup{T1,T2,T3,T4,T5,T6,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    /// <summary>
    ///     Interface for an editable 6-key/level look up structure
    /// </summary>
    /// <typeparam name="T1">key for level 1</typeparam>
    /// <typeparam name="T2">key for level 2</typeparam>
    /// <typeparam name="T3">key for level 3</typeparam>
    /// <typeparam name="T4">key for level 4</typeparam>
    /// <typeparam name="T5">key for level 5</typeparam>
    /// <typeparam name="T6">key for level 6</typeparam>
    /// <typeparam name="TValue">value</typeparam>
    public interface IEditableLookup<T1, T2, T3, T4, T5, T6, TValue> : ILookup<T1, T2, T3, T4, T5, T6, TValue>
    {
        /// <summary>
        /// Adds a value to the lookup
        /// </summary>
        /// <param name="key1">level 1 key</param>
        /// <param name="key2">level 2 key</param>
        /// <param name="key3">level 3 key</param>
        /// <param name="key4">level 4 key</param>
        /// <param name="key5">level 5 key</param>
        /// <param name="key6">level 6 key</param>
        /// <param name="value">value to add</param>
        void Add(T1 key1, T2 key2, T3 key3, T4 key4, T5 key5, T6 key6, TValue value);
    }
}