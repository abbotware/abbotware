// -----------------------------------------------------------------------
// <copyright file="AdvancedPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Plugins
{
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Publishes a single message type to an exchange
    /// </summary>
    /// <typeparam name="TMessage">Type of Message to publish</typeparam>
    public class AdvancedPublisher<TMessage> : BaseComponent, IAdvancedMessagePublisher<TMessage>
    {
        /// <summary>
        ///     protocol to encode the message with
        /// </summary>
        private readonly IAmqpMessageProtocol<TMessage> protocol;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedPublisher{TMessage}" /> class.
        /// </summary>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="protocol">protocol to encode message with</param>
        /// <param name="logger">injected logger</param>
        public AdvancedPublisher(IAmqpPublisher channel, IAmqpMessageProtocol<TMessage> protocol, ILogger logger)
            : base(logger)
        {
            this.Channel = Arguments.EnsureNotNull(channel, nameof(channel));
            this.protocol = Arguments.EnsureNotNull(protocol, nameof(protocol));
        }

        /// <summary>
        ///     Gets the client publisher
        /// </summary>
        protected IAmqpPublisher Channel
        {
            get;
        }

        /// <inheritdoc />
        public async Task<PublishStatus> PublishAsync(TMessage message)
        {
            return await this.Channel.Publish(this.protocol.Encode(message)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<PublishStatus> PublishAsync(TMessage message, string exchange)
        {
            return await this.Channel.Publish(this.protocol.Encode(message, exchange)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<PublishStatus> PublishAsync(TMessage message, string exchange, string topic)
        {
            return await this.Channel.Publish(this.protocol.Encode(message, exchange, topic)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<PublishStatus> PublishAsync(TMessage message, string exchange, string topic, bool mandatory)
        {
            return await this.Channel.Publish(this.protocol.Encode(message, exchange, topic, mandatory)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.Channel?.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}