// -----------------------------------------------------------------------
// <copyright file="IAmqpAcknowledger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    ///     Interface that manages the acknowledgement of messages via RabbitMQ Channel
    /// </summary>
    public interface IAmqpAcknowledger : IBasicAcknowledger
    {
        /// <summary>
        ///     Positively Acknowledges a message has been processed
        /// </summary>
        /// <param name="deliveryTag">message delivery tag</param>
        /// <param name="multiple">true to ack all messages up to and including the deliveryTag</param>
        void Ack(string deliveryTag, bool multiple);

        /// <summary>
        ///     Negatively Acknowledges a message (same as Reject, but can be used for multiple)
        /// </summary>
        /// <param name="deliveryTag">message delivery tag</param>
        /// <param name="multiple">true to nack all messages up to and including the deliveryTag</param>
        /// <param name="requeue">puts message back on the queue</param>
        void Nack(string deliveryTag, bool multiple, bool requeue);

        /// <summary>
        ///     Rejects a received message
        /// </summary>
        /// <param name="deliveryTag">message delivery tag</param>
        /// <param name="requeue">puts message back on the queue</param>
        void Reject(string deliveryTag, bool requeue);
    }
}