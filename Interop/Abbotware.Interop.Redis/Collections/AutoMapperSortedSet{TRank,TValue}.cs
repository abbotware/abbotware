// -----------------------------------------------------------------------
// <copyright file="AutoMapperSortedSet{TRank,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.Collections
{
    using System;
    using Abbotware.Interop.Redis.ExtensionPoints;
    using global::AutoMapper;
    using StackExchange.Redis;

    /// <summary>
    /// Redis SortedSet with AutoMapper converters
    /// </summary>
    /// <typeparam name="TRank">Rank Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public class AutoMapperSortedSet<TRank, TValue> : BaseSortedSet<TRank, TValue>
        where TRank : IComparable
    {
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperSortedSet{TRank, TValue}"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the set</param>
        /// <param name="database">redis database</param>
        /// <param name="mapper">type mapper</param>
        public AutoMapperSortedSet(string remotekey, int capacity, IDatabase database, IMapper mapper)
            : base(remotekey, capacity, database)
        {
            this.mapper = mapper;
        }

        /// <inheritdoc />
        protected override TValue OnConvertValue(RedisValue value)
        {
            return this.mapper.Map<TValue>(value);
        }

        /// <inheritdoc />
        protected override TRank OnConvertRank(double rank)
        {
            return this.mapper.Map<TRank>(rank);
        }

        /// <inheritdoc />
        protected override RedisValue OnConvertValue(TValue value)
        {
            return this.mapper.Map<RedisValue>(value);
        }

        /// <inheritdoc />
        protected override double OnConvertRank(TRank rank)
        {
            return this.mapper.Map<double>(rank);
        }
    }
}
