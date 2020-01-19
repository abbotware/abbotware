// -----------------------------------------------------------------------
// <copyright file="ICacheableSortedSet{TRank,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using Abbotware.Core.Cache.LocalOperations;

    /// <summary>
    /// interface for reading a cache backed sorted set (ranked values)
    /// </summary>
    /// <typeparam name="TRank">Rank Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public interface ICacheableSortedSet<TRank, TValue> : ICacheable<IWriteSortedSet<TRank, TValue>>
    {
    }
}