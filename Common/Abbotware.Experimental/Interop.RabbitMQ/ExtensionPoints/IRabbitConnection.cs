// -----------------------------------------------------------------------
// <copyright file="IRabbitConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.ExtensionPoints
{
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints.Services;

    /// <summary>
    ///     Interface for creating different types of RabbitMQ channel managers
    /// </summary>
    public interface IRabbitConnection : IConnection
    {
        /// <summary>
        ///     creates a Queue Manager
        /// </summary>
        /// <returns>Queue Manager</returns>
        IAmqpQueueManager CreateQueueManager();

        /// <summary>
        ///     creates an Exchange Manager
        /// </summary>
        /// <returns>Exchange Manager</returns>
        IAmqpExchangeManager CreateExchangeManager();

        /// <summary>
        ///     creates a Publish Manager
        /// </summary>
        /// <returns>Publish Manager</returns>
        IAmqpPublisher CreatePublishManager();

        /// <summary>
        ///     creates a Publish Manager
        /// </summary>
        /// <param name="channelConfiguration">channel configuration</param>
        /// <returns>Publish Manager</returns>
        IAmqpPublisher CreatePublishManager(ChannelConfiguration channelConfiguration);

        /// <summary>
        ///     creates a Message Retrieval Manager
        /// </summary>
        /// <returns>Message Retrieval Manager</returns>
        IAmqpRetriever CreateRetriever();

        /// <summary>
        ///     creates a Consumer Manager
        /// </summary>
        /// <returns>Consumer Manager</returns>
        IAmqpConsumerManager<global::RabbitMQ.Client.IBasicConsumer> CreateConsumerManager();

        /// <summary>
        ///     creates a Consumer Manager
        /// </summary>
        /// <param name="channelConfiguration">channel configuration</param>
        /// <returns>Consumer Manager</returns>
        IAmqpConsumerManager<global::RabbitMQ.Client.IBasicConsumer> CreateConsumerManager(ChannelConfiguration channelConfiguration);

        /// <summary>
        ///     creates an Acknowledgement Manager
        /// </summary>
        /// <returns>Acknowledgement Manager</returns>
        IAmqpAcknowledger CreateAcknowledger();

        /// <summary>
        ///     creates a Resource Manager
        /// </summary>
        /// <returns>Resource Manager</returns>
        IAmqpResourceManager CreateResourceManager();
    }
}