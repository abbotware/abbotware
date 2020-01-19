// -----------------------------------------------------------------------
// <copyright file="BaseCappedList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.ExtensionPoints
{
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
    /// Base class for creating a Redis 'Capped' List key
    /// </summary>
    /// <typeparam name="T">list element Type</typeparam>
    public abstract class BaseCappedList<T> : ICacheableList<T>, IWriteList<T>, ICacheOperations
    {
        private readonly int capacity;

        private readonly IDatabase database;

        private readonly List<T> snapshot = new List<T>();

        private readonly BagOfWork<T> work = new BagOfWork<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCappedList{T}"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the list</param>
        /// <param name="database">redis database</param>
        public BaseCappedList(string remotekey, int capacity, IDatabase database)
        {
            this.RemoteKey = remotekey;
            this.capacity = capacity;
            this.snapshot = new List<T>(capacity);
            this.database = database;
        }

        /// <inheritdoc />
        public IWriteList<T> Local => this;

        /// <inheritdoc />
        public ICacheOperations Remote => this;

        /// <inheritdoc />
        public string RemoteKey { get; }

        /// <inheritdoc />
        public virtual void Add(T element)
        {
            this.work.Add(element);
        }

        /// <inheritdoc />
        public IEnumerable<T> AsEnumerable()
        {
            lock (this.snapshot)
            {
                return this.snapshot.ToList();
            }
        }

        /// <inheritdoc />
        public async Task LoadAsync(CancellationToken ct)
        {
            var keys = await this.database.ListRangeAsync(this.RemoteKey)
                .ConfigureAwait(false);

            lock (this.snapshot)
            {
                this.snapshot.Clear();

                var add = keys.Select(x => this.OnConvertElement(x))
                    .ToList();

                this.snapshot.AddRange(add);
            }
        }

        /// <inheritdoc />
        public async Task SaveAsync(CancellationToken ct)
        {
            var changes = this.work.GetWork()
                .Select(x => this.OnConvertElement(x))
                .ToArray();

            await this.database.ListLeftPushAsync(this.RemoteKey, changes, CommandFlags.FireAndForget).ConfigureAwait(false);
            await this.database.ListTrimAsync(this.RemoteKey, 0, this.capacity - 1, CommandFlags.FireAndForget).ConfigureAwait(false);
        }

        /// <summary>
        /// Converts a redis value to T
        /// </summary>
        /// <param name="value">redis value</param>
        /// <returns>value</returns>
        protected abstract T OnConvertElement(RedisValue value);

        /// <summary>
        /// Converts a TValue to redis value
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>redis value</returns>
        protected abstract RedisValue OnConvertElement(T value);
    }
}
