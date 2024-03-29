﻿// -----------------------------------------------------------------------
// <copyright file="MqConsumer{TRequest,TResponse}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Plugins
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Base;

    /// <summary>
    ///     base class used to consume requests and send responses
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public abstract class MqConsumer<TRequest, TResponse> : BaseMqConsumer, IMessageConsumer<TRequest, TResponse>
    {
        /// <summary>
        ///     protocol to decode the messages
        /// </summary>
        private readonly IMessageProtocol<TRequest> protocol;

        /// <summary>
        ///     message publisher
        /// </summary>
        private readonly IMessagePublisher<TResponse> publisher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MqConsumer{TRequest, TResponse}" /> class.
        /// </summary>
        /// <param name="publisher">message publisher</param>
        /// <param name="acknowledger">message acknowledger</param>
        /// <param name="protocol">protocol to decode the messages</param>
        /// <param name="consumerImpl">injected consumer implementation</param>
        /// <param name="logger">injected logger</param>
        protected MqConsumer(IMessagePublisher<TResponse> publisher, IAmqpAcknowledger acknowledger, IMessageProtocol<TRequest> protocol, IBasicConsumer consumerImpl, ILogger logger)
            : base(acknowledger, consumerImpl, logger)
        {
            this.publisher = Arguments.EnsureNotNull(publisher, nameof(publisher));
            this.AcknowledgementManager = Arguments.EnsureNotNull(acknowledger, nameof(acknowledger));
            this.protocol = Arguments.EnsureNotNull(protocol, nameof(protocol));
            Arguments.NotNull(consumerImpl, nameof(protocol));
        }

        /// <summary>
        ///     Gets manager used for acknowledging the messages of this consumer
        /// </summary>
        protected IAmqpAcknowledger AcknowledgementManager { get; }

        /// <summary>
        ///     Gets the message publisher for this consumer
        /// </summary>
        protected IMessagePublisher<TResponse> Publisher
        {
            get { return this.publisher; }
        }

        /// <summary>
        ///     hook to for custom request/response handling logic
        /// </summary>
        /// <param name="message">received request message</param>
        /// <returns>response message</returns>
        public abstract TResponse OnHandle(TRequest message);

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "reviewed, bottom of call stack")]
        protected sealed override void OnDelivery(object? sender, DeliveryEventArgs args)
        {
            args = Arguments.EnsureNotNull(args, nameof(args));
            var envelope = Arguments.EnsureNotNull(args.Envelope, nameof(args.Envelope));
            var tag = envelope.DeliveryProperties.DeliveryTag!;

            try
            {
                var message = this.protocol.Decode(envelope);

                var response = this.OnHandle(message);

                var p = this.OnPublish(response);

                p.Wait(1000);

                this.AcknowledgementManager.Ack(tag, false);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex, $"Message:{tag} redelivered:{envelope.DeliveryProperties.Redelivered}");

                if (envelope.DeliveryProperties!.Redelivered)
                {
                    this.AcknowledgementManager.Nack(tag, false, false);
                }
            }
        }

        /// <summary>
        ///     Publishes a message using the supplied publisher
        /// </summary>
        /// <param name="response">response message</param>
        /// <returns>asynchronous publish task</returns>
        protected virtual Task<PublishStatus> OnPublish(TResponse response)
        {
            Arguments.NotNull(response, nameof(response));

            return this.Publisher.PublishAsync(response, default).AsTask();
        }
    }
}