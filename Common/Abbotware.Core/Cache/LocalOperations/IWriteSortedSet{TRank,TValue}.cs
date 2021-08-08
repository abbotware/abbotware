// -----------------------------------------------------------------------
// <copyright file="IWriteSortedSet{TRank,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.LocalOperations
{
    /// <summary>
    /// interface for writing to a cache backed sorted set (ranked values)
    /// </summary>
    /// <typeparam name="TRank">Rank Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public interface IWriteSortedSet<TRank, TValue> : IReadSortedSet<TRank, TValue>
        where TRank : notnull
    {
        /// <summary>
        /// Adds a ranked value to the local snapshot
        /// </summary>
        /// <param name="rank">item rank</param>
        /// <param name="value">item value</param>
        void Add(TRank rank, TValue value);
    }
}