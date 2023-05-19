// -----------------------------------------------------------------------
// <copyright file="Templates.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core;
    using Abbotware.Core.Messaging.Amqp.Configuration;

    /// <summary>
    ///     Creates common / useful configurations
    /// </summary>
    public static class Templates
    {
        /// <summary>
        ///     Creates a configuration for a temporary exchange
        /// </summary>
        /// <param name="name">name of the exchange</param>
        /// <param name="type">type of the exchange</param>
        /// <returns>exchange configuration</returns>
        public static ExchangeConfiguration TemporaryExchange(string name, ExchangeType type)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            var temp = new ExchangeConfiguration
            {
                ExchangeType = type,
                Name = name,
                IsAutoDelete = true,
                IsDurable = false,
            };

            return temp;
        }

        /// <summary>
        ///     Creates a configuration for a persistent exchange
        /// </summary>
        /// <param name="name">name of the exchange</param>
        /// <param name="type">type of the exchange</param>
        /// <returns>exchange configuration</returns>
        public static ExchangeConfiguration PersistentExchange(string name, ExchangeType type)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            var temp = new ExchangeConfiguration
            {
                ExchangeType = type,
                Name = name,
                IsAutoDelete = false,
                IsDurable = true,
            };

            return temp;
        }

        /// <summary>
        ///     Creates a queue for observing an exchange
        /// </summary>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration ObserverQueue()
        {
            return ObserverQueue(string.Empty);
        }

        /// <summary>
        ///     Creates a queue for observing an exchange
        /// </summary>
        /// <param name="name">queue name</param>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration ObserverQueue(string name)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            var config = new QueueConfiguration
            {
                IsAutoDelete = true,
                IsDurable = false,
                IsExclusive = true,
                Name = name,
            };

            return config;
        }

        /// <summary>
        ///     Creates a queue that can be shared among consumers
        /// </summary>
        /// <param name="name">queue name</param>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration ConsumerQueue(string name)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            var config = new QueueConfiguration
            {
                IsAutoDelete = false,
                IsDurable = true,
                IsExclusive = false,
                Name = name,
            };

            return config;
        }

        /// <summary>
        ///     Creates a queue that can be shared among consumers
        /// </summary>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration ConsumerQueue()
        {
            return ConsumerQueue(string.Empty);
        }

        /// <summary>
        ///     Creates a queue that can be used for unit tests
        /// </summary>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration UnitTestQueue()
        {
            return UnitTestQueue(string.Empty, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        ///     Creates a queue that can be used for unit tests
        /// </summary>
        /// <param name="name">queue name</param>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration UnitTestQueue(string name)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            return UnitTestQueue(name, TimeSpan.FromMinutes(10));
        }

        /// <summary>
        ///     Creates a queue that can be used for unit tests
        /// </summary>
        /// <param name="name">queue name</param>
        /// <param name="expiration">expiration for queue</param>
        /// <returns>queue configuration</returns>
        public static QueueConfiguration UnitTestQueue(string name, TimeSpan expiration)
        {
            Arguments.NotNullOrWhitespace(name, nameof(name));

            var config = new QueueConfiguration
            {
                IsAutoDelete = true,
                IsDurable = false,
                IsExclusive = false,
                Name = name,
                Expires = expiration,
            };

            return config;
        }

        /// <summary>
        ///     Channel Configruations
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "reviewed: better intelisense")]
        public static class Channel
        {
            /// <summary>
            ///     Creates a default channel config
            /// </summary>
            /// <param name="mode">channel mode</param>
            /// <returns>channel configuration</returns>
            public static ChannelConfiguration Default(ChannelMode mode)
            {
                return new ChannelConfiguration { Mode = mode };
            }

            /// <summary>
            ///     Creates a default channel config
            /// </summary>
            /// <returns>channel configuration</returns>
            public static ChannelConfiguration Default()
            {
                return Default(ChannelMode.None);
            }

            /// <summary>
            ///     Creates a channel config for round robin dispatching
            /// </summary>
            /// <returns>channel configuration</returns>
            public static ChannelConfiguration RoundRobinDispatchChannel()
            {
                return RoundRobinDispatchChannel(1);
            }

            /// <summary>
            ///     Creates a channel config for round robin dispatching
            /// </summary>
            /// <param name="count">message count to distribute evenly</param>
            /// <returns>channel configuration</returns>
            public static ChannelConfiguration RoundRobinDispatchChannel(ushort count)
            {
                var qos = new QualityOfService
                {
                    PreFetchCount = count,
                    PreFetchSize = 0,
                    PreFetchGlobal = false,
                };

                return new ChannelConfiguration
                {
                    Mode = ChannelMode.None,
                    QualityOfService = qos,
                };
            }
        }
    }
}