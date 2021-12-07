// -----------------------------------------------------------------------
// <copyright file="BaseSortedSet{TRank,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.ExtensionPoints
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache;
    using Abbotware.Core.Cache.Internal;
    using Abbotware.Core.Cache.LocalOperations;
    using Abbotware.Core.Cache.RemoteOperations;
    using StackExchange.Redis;

    /// <summary>
    /// Base class for creating a Redis SortedSet key
    /// </summary>
    /// <typeparam name="TRank">Rank Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public abstract class BaseSortedSet<TRank, TValue> : ICacheableSortedSet<TRank, TValue>, IWriteSortedSet<TRank, TValue>, ICacheOperations
        where TRank : IComparable
    {
        private readonly int capacity;

        private readonly IDatabase database;

        private readonly SortedList<TRank, TValue> snapshot;

        private readonly BagOfWork<Tuple<TRank, TValue>> work = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSortedSet{TRank, TValue}"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the set</param>
        /// <param name="database">redis database</param>
        protected BaseSortedSet(string remotekey, int capacity, IDatabase database)
        {
            this.RemoteKey = remotekey;
            this.capacity = capacity;
            this.snapshot = new SortedList<TRank, TValue>(this.capacity, new DuplicateKeyComparer<TRank>());
            this.database = database;
        }

        /// <inheritdoc />
        public string RemoteKey { get; }

        /// <inheritdoc />
        public IWriteSortedSet<TRank, TValue> Local => this;

        /// <inheritdoc />
        public ICacheOperations Remote => this;

        /// <inheritdoc />
        public void Add(TRank rank, TValue value)
        {
            this.work.Add(new Tuple<TRank, TValue>(rank, value));
        }

        /// <inheritdoc />
        public SortedList<TRank, TValue> AsSortedList()
        {
            lock (this.snapshot)
            {
                return new SortedList<TRank, TValue>(
                    this.snapshot,
                    this.snapshot.Comparer);
            }
        }

        /// <inheritdoc />
        public async Task LoadAsync(CancellationToken ct)
        {
            var keys = await this.database.SortedSetRangeByRankWithScoresAsync(this.RemoteKey).ConfigureAwait(false);

            lock (this.snapshot)
            {
                this.snapshot.Clear();

                foreach (var k in keys)
                {
                    this.snapshot.Add(this.OnConvertRank(k.Score), this.OnConvertValue(k.Element));
                }
            }
        }

        /// <inheritdoc />
        public async Task SaveAsync(CancellationToken ct)
        {
            var list = new List<SortedSetEntry>();

            foreach (var c in this.work.GetWork())
            {
                var sse = new SortedSetEntry(this.OnConvertValue(c.Item2), this.OnConvertRank(c.Item1));

                list.Add(sse);
            }

            await this.database.SortedSetAddAsync(this.RemoteKey, list.ToArray(), CommandFlags.FireAndForget).ConfigureAwait(false);
            await this.database.SortedSetRemoveRangeByRankAsync(this.RemoteKey, 0, -this.capacity - 1, CommandFlags.FireAndForget).ConfigureAwait(false);
        }

        /// <summary>
        /// Converts a redis value to TValue
        /// </summary>
        /// <param name="value">redis value</param>
        /// <returns>value</returns>
        protected abstract TValue OnConvertValue(RedisValue value);

        /// <summary>
        /// Converts a redis rank to TRank
        /// </summary>
        /// <param name="rank">redis rank</param>
        /// <returns>rank</returns>
        protected abstract TRank OnConvertRank(double rank);

        /// <summary>
        /// Converts a TValue to redis value
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>redis value</returns>
        protected abstract RedisValue OnConvertValue(TValue value);

        /// <summary>
        /// Converts a TRank to redis rank
        /// </summary>
        /// <param name="rank">rank</param>
        /// <returns>redis rank</returns>
        protected abstract double OnConvertRank(TRank rank);
    }
}
