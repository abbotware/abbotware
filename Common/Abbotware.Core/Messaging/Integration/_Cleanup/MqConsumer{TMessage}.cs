// -----------------------------------------------------------------------
// <copyright file="MqConsumer{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Base
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     base class used to consume messages
    /// </summary>
    /// <typeparam name="TMessage">Type of Message</typeparam>
    public abstract class MqConsumer<TMessage> : BaseMqConsumer, IMessageConsumer<TMessage>
    {
        /// <summary>
        ///     protocol to decode the messages
        /// </summary>
        private readonly IMessageProtocol<TMessage> protocol;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MqConsumer{TMessage}" /> class.
        /// </summary>
        /// <param name="acknowledger">message acknowledger</param>
        /// <param name="protocol">protocol to decode the messages</param>
        /// <param name="consumer">injected consumer implementation</param>
        /// <param name="logger">injected logger</param>
        protected MqConsumer(IBasicAcknowledger acknowledger, IMessageProtocol<TMessage> protocol, IBasicConsumer consumer, ILogger logger)
        : base(acknowledger, consumer, logger)
        {
            Arguments.NotNull(acknowledger, nameof(acknowledger));
            Arguments.NotNull(protocol, nameof(protocol));
            Arguments.NotNull(consumer, nameof(consumer));
            Arguments.NotNull(logger, nameof(logger));

            this.protocol = protocol;
        }

        /// <summary>
        ///     hook to for custom message handling logic
        /// </summary>
        /// <param name="message">received request message</param>
        public abstract void OnHandle(TMessage message);

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "bu")]
        protected sealed override void OnDelivery(object? sender, DeliveryEventArgs args)
        {
            args = Arguments.EnsureNotNull(args, nameof(args));

            var envelope = args.Envelope;

            try
            {
                var message = this.protocol.Decode(envelope);

                this.OnHandle(message);

                this.Acknowledger.Ack(envelope);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex, $"Message:{envelope.DeliveryProperties?.DeliveryTag} redelivered:{envelope?.DeliveryProperties?.Redelivered}");

                if (envelope!.DeliveryProperties!.Redelivered)
                {
                    this.OnRedelileveredAndException(envelope);
                }
            }
        }

        /// <summary>
        ///     hook to for handled redelieved messages that have an exception
        /// </summary>
        /// <param name="envelope">message envelope</param>
        protected virtual void OnRedelileveredAndException(IMessageEnvelope envelope)
        {
            // in AMQP we would NACK the message
            // this.Acknowledger.Nack(envelope.DeliveryProperties.DeliveryTag, false, false);
        }
    }
}