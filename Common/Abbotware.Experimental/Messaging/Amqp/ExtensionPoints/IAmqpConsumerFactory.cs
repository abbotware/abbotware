// -----------------------------------------------------------------------
// <copyright file="IAmqpConsumerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using Abbotware.Core.Messaging.Integration.Base;

    /// <summary>
    /// interface for message consumer factory
    /// </summary>
    public interface IAmqpConsumerFactory
    {
        /// <summary>
        /// Creates a message consumer object
        /// </summary>
        /// <typeparam name="TConsumer">consumer class type</typeparam>
        /// <param name="acknowledgementManager">message ack manager</param>
        /// <returns>configured consumer object</returns>
        TConsumer CreateConsumer<TConsumer>(IAmqpAcknowledger acknowledgementManager)
        where TConsumer : BaseMqConsumer;
    }
}