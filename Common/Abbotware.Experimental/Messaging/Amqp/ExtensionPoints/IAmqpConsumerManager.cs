// -----------------------------------------------------------------------
// <copyright file="IAmqpConsumerManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.ExtensionPoints.Services
{
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;

    /// <summary>
    ///     Interface that manages AMQP Consumers via RabbitMQ Channel
    /// </summary>
    /// <typeparam name="TConsumer">platform consumer object</typeparam>
    public interface IAmqpConsumerManager<TConsumer> : IAmqpAcknowledger
    {
        /// <summary>
        ///     Starts a new Consumer on the current Channel
        /// </summary>
        /// <param name="queueName">queue name to attach consumer to</param>
        /// <param name="requireAcknowledgements">true = turn off acknowledgement's, false = require acknowledgements</param>
        /// <param name="consumer">Consumer to attach</param>
        /// <returns>Consumer Tag</returns>
        string Start(string queueName, bool requireAcknowledgements, TConsumer consumer);

        /// <summary>
        ///     Cancels an existing Consumer
        /// </summary>
        /// <param name="consumerTag">Consumer Tag to cancel</param>
        void Cancel(string consumerTag);
    }
}