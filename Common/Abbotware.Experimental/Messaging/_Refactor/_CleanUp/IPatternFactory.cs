// -----------------------------------------------------------------------
// <copyright file="IPatternFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
// -----------------------------------------------------------------------

namespace Abbotware.Messaging.Interop.RabbitMQ.Patterns
{
    
    using Abbotware.Interop.RabbitMQ.Config;
    using Abbotware.Interop.RabbitMQ.Patterns;
    using Abbotware.Messaging.Interop.RabbitMQ.Patterns.CodeContracts;

    /// <summary>
    /// factory interface for creating common pub/sub patterns
    /// </summary>
    [ContractClass(typeof(IPatternFactoryContracts))]
    public interface IPatternFactory
    {
        /// <summary>
        /// Creates a publisher for the default exchange
        /// </summary>
        /// <returns>configured publisher</returns>
        ExchangePublisher CreateExchangePublisher();
        
        /// <summary>
        /// Creates a publisher for a specified exchange
        /// </summary>
        /// <param name="exchange">name of exchange to publish messages to</param>
        /// <returns>configured publisher</returns>
        ExchangePublisher CreateExchangePublisher(string exchange);

        /// <summary>
        /// Creates a publisher for a specified exchange
        /// </summary>
        /// <param name="exchange">name of exchange to publish messages to</param>
        /// <param name="protocol">protocol for encoding messages</param>
        /// <returns>configured publisher</returns>
        ExchangePublisher CreateExchangePublisher(string exchange, IGenericMessageProtocol protocol);

        /// <summary>
        /// Creates a publisher for a specified exchange
        /// </summary>
        /// <param name="exchange">name of exchange to publish messages to</param>
        /// <param name="protocol">protocol for encoding messages</param>
        /// <param name="channelConfig">connection configuration</param>
        /// <returns>configured publisher</returns>
        ExchangePublisher CreateExchangePublisher(string exchange, IGenericMessageProtocol protocol, ChannelConfiguration channelConfig);
    }
}
