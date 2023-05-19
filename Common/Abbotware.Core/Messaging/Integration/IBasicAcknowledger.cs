// -----------------------------------------------------------------------
// <copyright file="IBasicAcknowledger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Interface that manages the acknowledgement of messages via RabbitMQ Channel
    /// </summary>
    public interface IBasicAcknowledger : IDisposable
    {
        /// <summary>
        ///     Positively Acknowledges a message has been processed
        /// </summary>
        /// <param name="envelope">message properties</param>
        void Ack(IMessageEnvelope envelope);

        /// <summary>
        ///     Positively Acknowledges a message has been processed
        /// </summary>
        /// <param name="envelope">message properties</param>
        /// <param name="multiple">true to ack all messages up to and including the deliveryTag</param>
        void Ack(IMessageEnvelope envelope, bool multiple);
    }
}