// -----------------------------------------------------------------------
// <copyright file="PatternFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
// -----------------------------------------------------------------------

namespace Abbotware.Messaging.Interop.RabbitMQ.Patterns
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    using Abbotware.Core.Services;
    using Abbotware.Interop.RabbitMQ;
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.Patterns;
    using Abbotware.Messaging.Interop.RabbitMQ.Protocol;
    
    /// <summary>
    /// Factory for creating common pub/sub patterns
    /// </summary>
    public class PatternFactory : IPatternFactory
    {
        /// <summary>
        /// rabbitmq connection
        /// </summary>
        private readonly RabbitMQConnection connection;
    
        /// <summary>
        /// injected logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the PatternFactory class
        /// </summary>
        /// <param name="connection">rabbitmq connection</param>
        /// <param name="logger">injected logger</param>
        public PatternFactory(RabbitMQConnection connection, ILogger logger)
        {
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
            Contract.Requires<ArgumentNullException>(connection != null, "connection is null");

            this.logger = logger;
            this.connection = connection;
        }

        /// <inheritdoc/>
        public ExchangePublisher CreateExchangePublisher()
        {
            return this.CreateExchangePublisher(Constants.DefaultExchange);
        }

        /// <inheritdoc/>
        public ExchangePublisher CreateExchangePublisher(string exchange)
        {
            return this.CreateExchangePublisher(Constants.DefaultExchange, new PocoDataContractSerialization());
        }

        /// <inheritdoc/>
        public ExchangePublisher CreateExchangePublisher(string exchange, IGenericMessageProtocol protocol)
        {
            var childLogger = this.logger.CreateChildLogger("test");

            return new ExchangePublisher(exchange, this.connection.CreateChannel<IPublishManager>(), protocol, childLogger);
        }

        /// <inheritdoc/>
        public ExchangePublisher CreateExchangePublisher(string exchange, IGenericMessageProtocol protocol, ChannelConfiguration channelConfig)
        {
            var childLogger = this.logger.CreateChildLogger("test");

            return new ExchangePublisher(exchange, this.connection.CreateChannel<IPublishManager>(channelConfig), protocol, childLogger);
        }
    }
}
