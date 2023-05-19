// -----------------------------------------------------------------------
// <copyright file="IReadOnlyUniquePairs{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// ReadOnly Interface for a unique pairs collection
    /// </summary>
    /// <typeparam name="T">item type</typeparam>
    public interface IReadOnlyUniquePairs<T>
    {
        /// <summary>
        /// checks if the item is in the map or not
        /// </summary>
        /// <param name="item">item to look up pair</param>
        /// <returns>true if it is contained</returns>
        bool Contains(T item);

        /// <summary>
        /// Gets the other element for this pair
        /// </summary>
        /// <param name="item">item to look up pair</param>
        /// <returns>the other item</returns>
        T Other(T item);
    }
}