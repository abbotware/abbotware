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
    public class RedisConnectionFactory : BaseLoggable, IRedisConnectionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisConnectionFactory"/> class.
        /// </summary>
        /// <param name="defaultConfiguration">injected default configuration</param>
        /// <param name="logger">injected logger</param>
        public RedisConnectionFactory(IConnectionOptions defaultConfiguration, ILogger logger)
            : base(logger)
        {
            this.DefaultOptions = defaultConfiguration;
        }

        /// <inheritdoc/>
        public IConnectionOptions DefaultOptions { get; }

        /// <inheritdoc/>
        public IRedisConnection Create()
        {
            var cfg = this.DefaultOptions.ToStackExchange();

            return this.OnCreate(cfg);
        }

        /// <inheritdoc/>
        public IRedisConnection Create(IConnectionOptions configuration)
        {
            configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));

            var cfg = configuration.ToStackExchange();

            return this.OnCreate(cfg);
        }

        /// <inheritdoc/>
        public void Destroy(IRedisConnection connection)
        {
            connection = Arguments.EnsureNotNull(connection, nameof(connection));

            connection.Dispose();
        }

        /// <summary>
        /// encasulates the create connection logic
        /// </summary>
        /// <param name="options">configuration options</param>
        /// <returns>redis connection</returns>
        private RedisConnection OnCreate(ConfigurationOptions options)
        {
            var cm = ConnectionMultiplexer.Connect(options);

            // https://stackoverflow.com/questions/30797716/deadlock-when-accessing-stackexchange-redis
            cm.PreserveAsyncOrder = false;

            var connection = new RedisConnection(cm);

            return connection;
        }
    }
}