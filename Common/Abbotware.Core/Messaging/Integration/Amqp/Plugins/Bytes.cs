// -----------------------------------------------------------------------
// <copyright file="Bytes.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Plugins.Serialization;

    /// <summary>
    ///     Binary protocol that performs no data alterations
    /// </summary>
    public class Bytes : BaseAmqpProtocol<byte[]>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bytes"/> class.
        /// </summary>
        public Bytes()
            : base(new NoEncoder())
        {
        }

        /// <inheritdoc />
        public override IMessageEnvelope Encode(byte[] message, IPublishProperties properties)
        {
            var envelope = new MessageEnvelope(properties)
            {
                Body = message,
            };

            this.OnMessageEnvelope(message, envelope);

            return envelope;
        }

        /// <inheritdoc />
        public override byte[] Decode(IMessageEnvelope envelope)
        {
            Arguments.NotNull(envelope, nameof(envelope));

            return envelope.Body.ToArray();
        }
    }
}