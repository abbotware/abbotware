// -----------------------------------------------------------------------
// <copyright file="RedisCache.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache.ExtensionPoints;
    using StackExchange.Redis;

    /// <summary>
    /// manages a redis cache database
    /// </summary>
    public class RedisCache : IRemoteCache<IDatabase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCache"/> class.
        /// </summary>
        /// <param name="database">injected redis database</param>
        internal RedisCache(IDatabase database)
        {
            this.Native = database;
        }

        /// <inheritdoc/>
        public IDatabase Native { get; }

        /// <inheritdoc/>
        public async Task<string> GetFieldAsync(string hash, string field, CancellationToken ct)
        {
            return await this.Native.HashGetAsync(hash, field).
                ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyDictionary<string, string>> GetFieldsAsync(string hash, CancellationToken ct)
        {
            var keys = await this.Native.HashGetAllAsync(hash)
                .ConfigureAwait(false);

            return keys.ToStringDictionary();
        }

        /// <inheritdoc/>
        public async Task SetFieldsAsync(string hash, IEnumerable<KeyValuePair<string, string>> fields, CancellationToken ct)
        {
            var values = fields
                .Select(x => new HashEntry(x.Key, x.Value))
                .ToArray();

            await this.Native.HashSetAsync(hash, values)
                .ConfigureAwait(false);
        }
    }
}