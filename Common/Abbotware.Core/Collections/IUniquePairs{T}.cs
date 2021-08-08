// -----------------------------------------------------------------------
// <copyright file="IUniquePairs{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    /// <summary>
    /// Interface for a unique pairs collection
    /// </summary>
    /// <typeparam name="T">item type</typeparam>
    public interface IUniquePairs<T> : IReadOnlyUniquePairs<T>
    {
        /// <summary>
        ///     Adds a pair to the colletion
        /// </summary>
        /// <param name="first">first item</param>
        /// <param name="second">second item</param>
        void Add(T first, T second);

        /// <summary>
        ///     Adds a pair to the colletion
        /// </summary>
        /// <param name="first">first item</param>
        /// <param name="second">second item</param>
        /// <returns>true if Add succeeded</returns>
        bool TryAdd(T first, T second);

        /// <summary>
        /// Removes an item and its pair
        /// </summary>
        /// <param name="item">item to to remove (and its pair)</param>
        /// <returns>true if the pair was removed</returns>
        bool Remove(T item);

        /// <summary>
        /// Removes an item and its pair
        /// </summary>
        /// <param name="item">item to to remove (and its pair)</param>
        /// <param name="other">gets the other if it exists</param>
        /// <returns>true if the pair was removed</returns>
        bool Remove(T item, out T? other);
    }
}