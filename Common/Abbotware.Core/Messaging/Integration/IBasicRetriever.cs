// -----------------------------------------------------------------------
// <copyright file="IBasicRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Interface that manages receiving messages via RabbitMQ Channel
    /// </summary>
    public interface IBasicRetriever : IBasicAcknowledger, IDisposable
    {
        /// <summary>
        ///     Retrieves the raw bytes of a message (no type inference)
        /// </summary>
        /// <param name="queueName">Queue name to get from</param>
        /// <param name="noAck">
        ///     if set to true, the broker will not require an Ack from the client.  It will automatically assume
        ///     the message was successfully process.  If set to false, the client must Ack,Nack or Reject the message.
        /// </param>
        /// <returns>null if no messages are in queue, otherwise returns a message along with its meta data</returns>
        Task<IEnumerable<IMessageEnvelope>> Get(string queueName, bool noAck);
    }
}