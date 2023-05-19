// -----------------------------------------------------------------------
// <copyright file="HandleBasicDelivery.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.ExtensionPoints.Patterns
{
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Configuration;

    /// <summary>
    ///     Delegate / Callback type for handling the BasicDeliver for an AMQP consumer
    /// </summary>
    /// <param name="consumerTag">the consumer tag</param>
    /// <param name="envelope">payload and properties of the message</param>
    /// <param name="ackManager">rabbitmq connection channel</param>
    public delegate void HandleBasicDelivery(string consumerTag, IMessageEnvelope envelope, IAmqpAcknowledger ackManager);
}