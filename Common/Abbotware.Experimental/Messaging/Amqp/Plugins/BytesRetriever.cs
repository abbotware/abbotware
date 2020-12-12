// -----------------------------------------------------------------------
// <copyright file="BytesRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Amqp.Plugins;

    /// <summary>
    ///     Retrieves a binary data from a queue
    /// </summary>
    public class BytesRetriever : AdvancedRetriever<byte[]>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BytesRetriever" /> class.
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="noAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="logger">injected logger</param>
        public BytesRetriever(string queue, bool noAck, IAmqpRetriever channel, ILogger logger)
            : base(queue, noAck, channel, new Bytes(), logger)
        {
            Arguments.NotNull(queue, nameof(queue));
            Arguments.NotNull(channel, nameof(channel));
            Arguments.NotNull(logger, nameof(logger));
        }
    }
}