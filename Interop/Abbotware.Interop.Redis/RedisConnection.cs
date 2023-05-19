// -----------------------------------------------------------------------
// <copyright file="RedisConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis
{
    using Abbotware.Core.Cache;
    using Abbotware.Core.Cache.ExtensionPoints;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Redis.ExtensionPoints;
    using StackExchange.Redis;

    /// <summary>
    /// Redis Connection Configuration
    /// </summary>
    public sealed class RedisConnection : IConnection, IRedisConnection
    {
        /// <summary>
        /// injected connection multiplexer
        /// </summary>
        private readonly ConnectionMultiplexer connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisConnection"/> class.
        /// </summary>
        /// <param name="connectionMultiplexer">injected connection multiplexer</param>
        public RedisConnection(ConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
        }

        /// <inheritdoc />
        public bool IsOpen => this.connectionMultiplexer.IsConnected;

        /// <inheritdoc />
        public PropertySet CreatePropertySet(string type, string id)
        {
            var db = this.GetDatabase();

            return new PropertySet(type, id, db);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.connectionMultiplexer.Dispose();
        }

        /// <summary>
        /// Gets the redis db
        /// </summary>
        /// <returns>redis database</returns>
        public IRemoteCache<IDatabase> GetDatabase()
        {
            return this.GetDatabase(-1);
        }

        /// <summary>
        /// Gets the redis db
        /// </summary>
        /// <param name="db">db number</param>
        /// <returns>redis database</returns>
        public IRemoteCache<IDatabase> GetDatabase(int db)
        {
            var d = this.connectionMultiplexer.GetDatabase(db);

            var cache = new RedisCache(d);

            return cache;
        }
    }
}
