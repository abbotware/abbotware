// -----------------------------------------------------------------------
// <copyright file="Publisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Base
{
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Common base class for single destination Publisher
    /// </summary>
    /// <typeparam name="TMessage">type of message</typeparam>
    public class Publisher<TMessage> : BaseComponent, IMessagePublisher<TMessage>
    {
        /// <summary>
        ///     protocol to encode the message with
        /// </summary>
        private readonly IMessageProtocol<TMessage> protocol;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Publisher{TMessage}" /> class.
        /// </summary>
        /// <param name="destination">Name of publishing destination</param>
        /// <param name="channel">handle to the connection with publishing features</param>
        /// <param name="protocol">protocol to encode message with</param>
        /// <param name="logger">injected logger</param>
        public Publisher(string destination, IBasicPublisher channel, IMessageProtocol<TMessage> protocol, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(destination, nameof(destination));
            Arguments.NotNull(channel, nameof(channel));
            Arguments.NotNull(protocol, nameof(protocol));
            Arguments.NotNull(logger, nameof(logger));

            this.protocol = protocol;
            this.Destination = destination;
            this.Channel = channel;
        }

        /// <summary>
        ///    Gets the name of the destination used for publishing
        /// </summary>
        public string Destination
        {
            get;
        }

        /// <summary>
        ///     Gets the client publisher
        /// </summary>
        protected IBasicPublisher Channel
        {
            get;
        }

        /// <inheritdoc />
        public ValueTask<PublishStatus> PublishAsync(TMessage message, CancellationToken ct)
        {
            return this.Channel.Publish(this.protocol.Encode(message, this.Destination));
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.Channel?.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}