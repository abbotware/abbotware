// -----------------------------------------------------------------------
// <copyright file="RedisConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis
{
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Redis.ExtensionPoints;
    using StackExchange.Redis;

    /// <summary>
    /// Redis Connection Factory via StackExchange
    /// </summary>
    public class RedisConnectionFactory : BaseConnectionFactory<IRedisConnection, IConnectionOptions>, IRedisConnectionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisConnectionFactory"/> class.
        /// </summary>
        /// <param name="defaultOptions">injected default connection options</param>
        /// <param name="logger">injected logger</param>
        public RedisConnectionFactory(IConnectionOptions defaultOptions, ILogger logger)
            : base(defaultOptions, logger)
        {
        }

        /// <inheritdoc/>
        public override IRedisConnection Create()
        {
            var cfg = this.DefaultOptions.ToStackExchange();

            return OnCreate(cfg);
        }

        /// <inheritdoc/>
        public override IRedisConnection Create(IConnectionOptions configuration)
        {
            configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));

            var cfg = configuration.ToStackExchange();

            return OnCreate(cfg);
        }

        /// <inheritdoc/>
        public override void Destroy(IRedisConnection connection)
        {
            connection = Arguments.EnsureNotNull(connection, nameof(connection));

            connection.Dispose();
        }

        /// <summary>
        /// encasulates the create connection logic
        /// </summary>
        /// <param name="options">configuration options</param>
        /// <returns>redis connection</returns>
        private static RedisConnection OnCreate(ConfigurationOptions options)
        {
            var cm = ConnectionMultiplexer.Connect(options);

            // https://stackoverflow.com/questions/30797716/deadlock-when-accessing-stackexchange-redis
            cm.PreserveAsyncOrder = false;

            var connection = new RedisConnection(cm);

            return connection;
        }
    }
}