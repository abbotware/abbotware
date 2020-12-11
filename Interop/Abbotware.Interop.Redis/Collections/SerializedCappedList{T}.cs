// -----------------------------------------------------------------------
// <copyright file="SerializedCappedList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache;
    using Abbotware.Core.Cache.LocalOperations;
    using Abbotware.Core.Cache.RemoteOperations;
    using Abbotware.Core.Serialization;
    using StackExchange.Redis;

    /// <summary>
    /// Redis List with AutoMapper converters (Decorator for StringCappedList)
    /// </summary>
    /// <typeparam name="T">List Element Type</typeparam>
    public class SerializedCappedList<T> : ICacheableList<T>, IWriteList<T>, ICacheOperations
    {
        private readonly IStringSerializaton serializer;

        private readonly StringCappedList storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializedCappedList{T}"/> class.
        /// </summary>
        /// <param name="remotekey">remote key name</param>
        /// <param name="capacity">capacity of the list</param>
        /// <param name="database">redis database</param>
        /// <param name="serializer">string serializer</param>
        public SerializedCappedList(string remotekey, int capacity, IDatabase database, IStringSerializaton serializer)
        {
            this.storage = new StringCappedList(remotekey, capacity, database);

            this.serializer = serializer;
        }

        /// <inheritdoc />
        public string RemoteKey => this.storage.RemoteKey;

        /// <inheritdoc />
        public IWriteList<T> Local => this;

        /// <inheritdoc />
        public ICacheOperations Remote => this;

        /// <inheritdoc/>
        public void Add(T element)
        {
            var s = this.serializer.Encode(element);

            this.storage.Add(s);
        }

        /// <inheritdoc/>
        public IEnumerable<T> AsEnumerable()
        {
            var a = this.storage.AsEnumerable()
               .Select(x => this.serializer.Decode<T>(x));

            return a;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<T> ToList()
        {
            return this.AsEnumerable().ToList();
        }

        /// <inheritdoc/>
        public T[] ToArray()
        {
            return this.AsEnumerable().ToArray();
        }

        /// <inheritdoc/>
        public Task SaveAsync(CancellationToken ct)
        {
            return this.storage.SaveAsync(ct);
        }

        /// <inheritdoc/>
        public async Task LoadAsync(CancellationToken ct)
        {
            await this.storage.LoadAsync(ct)
                 .ConfigureAwait(false);
        }
    }
}