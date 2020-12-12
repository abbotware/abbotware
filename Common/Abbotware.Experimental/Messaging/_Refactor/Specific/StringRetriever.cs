//-----------------------------------------------------------------------
// <copyright file="StringRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins.Patterns.Specific
{
    using System;
    using System.Text;
    using Abbotware.Core;
    using Abbotware.Core.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.Plugins.MessageProtocol.Specific;

    /// <summary>
    ///     Retrieves strings from a queue
    /// </summary>
    public class StringRetriever : SingleMessageTypeRetriever<string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StringRetriever" /> class.
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="noAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="logger">injected logger</param>
        public StringRetriever(string queue, bool noAck, IMessageRetrievalManager channel, ILogger logger)
            : this(queue, noAck, channel, new UTF8Encoding(), logger)
        {
            Contract.Requires<ArgumentNullException>(queue != null, "queue is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringRetriever" /> class.
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="noAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="encoding">string encoding type to use</param>
        /// <param name="logger">injected logger</param>
        public StringRetriever(string queue, bool noAck, IMessageRetrievalManager channel, Encoding encoding, ILogger logger)
            : base(queue, noAck, channel, new StringProtocol(encoding), logger)
        {
            Contract.Requires<ArgumentNullException>(queue != null, "queue is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(encoding != null, "encoding is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }
    }
}