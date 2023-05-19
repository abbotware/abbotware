// -----------------------------------------------------------------------
// <copyright file="IEditableLookUp{T1,T2,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    /// <summary>
    ///     Interface for a editable 2-key/level look up structure
    /// </summary>
    /// <typeparam name="T1">key for level 1</typeparam>
    /// <typeparam name="T2">key for level 2</typeparam>
    /// <typeparam name="TValue">value</typeparam>
    public interface IEditableLookup<T1, T2, TValue> : ILookup<T1, T2, TValue>
        where T1 : notnull
        where T2 : notnull
    {
        /// <summary>
        ///     Adds a value to the lookup
        /// </summary>
        /// <param name="key1">level 1 key</param>
        /// <param name="key2">level 2 key</param>
        /// <param name="value">value to add</param>
        void Add(T1 key1, T2 key2, TValue value);
    }
}