// -----------------------------------------------------------------------
// <copyright file="Retriever{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Plugins
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     base class used for many of the retrievers
    /// </summary>
    /// <typeparam name="TMessage">type of message</typeparam>
    public class Retriever<TMessage> : BaseComponent, IMessageRetriever<TMessage>, IBasicAcknowledger
    {
        /// <summary>
        ///     protocol to encode the message with
        /// </summary>
        private readonly IMessageProtocol<TMessage> protocol;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Retriever{TMessage}"/> class.
        /// </summary>
        /// <param name="source">name of the queue to retrieve from</param>
        /// <param name="channel">handle to the connection with retreival features</param>
        /// <param name="protocol">protocol to encode message with</param>
        /// <param name="logger">injected logger</param>
        public Retriever(string source, IBasicRetriever channel, IMessageProtocol<TMessage> protocol, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(source, nameof(source));
            Arguments.NotNull(channel, nameof(channel));
            Arguments.NotNull(protocol, nameof(protocol));
            Arguments.NotNull(logger, nameof(logger));

            this.Source = source;
            this.Channel = channel;
            this.protocol = protocol;
        }

        /// <summary>
        ///     Gets the name of the queue used for retrieving
        /// </summary>
        public string Source
        {
            get;
        }

        /// <summary>
        ///     Gets connection channel to use for retrieving messages
        /// </summary>
        protected IBasicRetriever Channel
        {
            get;
        }

        /// <inheritdoc />
        public void Ack(IMessageEnvelope envelope, bool multiple)
        {
            this.Channel.Ack(envelope, multiple);
        }

        /// <inheritdoc />
        public void Ack(IMessageEnvelope envelope)
        {
            this.Ack(envelope, false);
        }

        /// <inheritdoc />
        public async Task<IReceived<TMessage>> RetrieveAsync()
        {
            var envelope = await this.Channel.Get(this.Source, false).ConfigureAwait(false);

            var content = this.protocol.Decode(envelope.Single());

            var result = new Received<TMessage>(content, envelope.Single());

            return result;
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.Channel?.Dispose();
            base.OnDisposeManagedResources();
        }
    }
}