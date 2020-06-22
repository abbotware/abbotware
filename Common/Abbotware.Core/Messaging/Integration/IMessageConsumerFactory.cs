// -----------------------------------------------------------------------
// <copyright file="IMessageConsumerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Integration
{
    using Abbotware.Core.Messaging.Integration.Base;

    /// <summary>
    /// interface for message consumer factory
    /// </summary>
    public interface IMessageConsumerFactory
    {
        /// <summary>
        /// Creates a message consumer object
        /// </summary>
        /// <typeparam name="TConsumer">consumer class type</typeparam>
        /// <param name="acknowledger">message ack manager</param>
        /// <returns>configured consumer object</returns>
        TConsumer CreateConsumer<TConsumer>(IBasicAcknowledger acknowledger)
        where TConsumer : BaseMqConsumer;

        /// <summary>
        /// Creates a message consumer object
        /// </summary>
        /// <typeparam name="TConsumer">consumer class type</typeparam>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="acknowledger">message ack manager</param>
        /// <param name="protocol">message protocol</param>
        /// <param name="consumer">message consumer</param>
        /// <returns>configured consumer object</returns>
        TConsumer CreateConsumer<TConsumer, TMessage>(IBasicAcknowledger acknowledger, IMessageProtocol<TMessage> protocol, IBasicConsumer consumer)
            where TConsumer : MqConsumer<TMessage>;
    }
}