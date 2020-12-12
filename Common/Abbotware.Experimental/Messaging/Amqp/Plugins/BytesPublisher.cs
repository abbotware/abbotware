// -----------------------------------------------------------------------
// <copyright file="BytesPublisher.cs" company="Abbotware, LLC">
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
    ///     publisher raw binary data with no transformation to an exchange
    /// </summary>
    public class BytesPublisher : AdvancedPublisher<byte[]>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BytesPublisher" /> class.
        /// </summary>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="logger">injected logger</param>
        public BytesPublisher(IAmqpPublisher channel, ILogger logger)
            : base(channel, new Bytes(), logger)
        {
            Arguments.NotNull(channel, nameof(channel));
            Arguments.NotNull(logger, nameof(logger));
        }
    }
}