// -----------------------------------------------------------------------
// <copyright file="IWriteList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.LocalOperations
{
    /// <summary>
    /// interface for writing to a cache backed list
    /// </summary>
    /// <typeparam name="T">list element type</typeparam>
    public interface IWriteList<T> : IReadList<T>
    {
        /// <summary>
        /// Adds an item to the local snapshot
        /// </summary>
        /// <param name="element">element</param>
        void Add(T element);
    }
}