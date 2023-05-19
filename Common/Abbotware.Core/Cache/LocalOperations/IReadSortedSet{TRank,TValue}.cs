// -----------------------------------------------------------------------
// <copyright file="IReadSortedSet{TRank,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.LocalOperations
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for reading a cache backed sorted set (ranked values)
    /// </summary>
    /// <typeparam name="TRank">Rank Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public interface IReadSortedSet<TRank, TValue>
        where TRank : notnull
    {
        /// <summary>
        /// Gets the data as a SortedList
        /// </summary>
        /// <returns>sorted list</returns>
        SortedList<TRank, TValue> AsSortedList();
    }
}