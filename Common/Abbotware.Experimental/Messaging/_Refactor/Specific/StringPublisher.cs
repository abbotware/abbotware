//-----------------------------------------------------------------------
// <copyright file="StringPublisher.cs" company="Abbotware, LLC">
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
    ///     Publishes strings to an exchange
    /// </summary>
    public class StringPublisher : SingleMessageTypePublisher<string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StringPublisher" /> class.
        /// </summary>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="logger">injected logger</param>
        public StringPublisher(string exchange, IPublishManager channel, ILogger logger)
            : this(exchange, channel, new UTF8Encoding(), logger)
        {
            Contract.Requires<ArgumentNullException>(exchange != null, "exchange is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringPublisher" /> class.
        /// </summary>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="encoding">encoding to encode strings with</param>
        /// <param name="logger">injected logger</param>
        public StringPublisher(string exchange, IPublishManager channel, Encoding encoding, ILogger logger)
            : base(exchange, channel, new StringProtocol(encoding), logger)
        {
            Contract.Requires<ArgumentNullException>(exchange != null, "exchange is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(encoding != null, "encoding is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }
    }
}