// -----------------------------------------------------------------------
// <copyright file="SimplifiedPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Plugins
{
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp.Configuration;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Publishes a single message type to an exchange
    /// </summary>
    /// <typeparam name="TMessage">Type of Message to publish</typeparam>
    public class SimplifiedPublisher<TMessage> : BaseComponent, IMessagePublisher<TMessage>
    {
        private readonly IAmqpProtocolDefaults defaults;

        private readonly IAdvancedMessagePublisher<TMessage> publisher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplifiedPublisher{TMessage}" /> class.
        /// </summary>
        /// <param name="publisher">advanced publisher</param>
        /// <param name="defaults">config for advanced publisher</param>
        /// <param name="logger">injected logger</param>
        public SimplifiedPublisher(IAdvancedMessagePublisher<TMessage> publisher, IAmqpProtocolDefaults defaults, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(publisher, nameof(publisher));
            Arguments.NotNull(defaults, nameof(defaults));
            Arguments.NotNull(logger, nameof(logger));

            this.publisher = publisher;
            this.defaults = defaults;
        }

        /// <inheritdoc />
        public async Task<PublishStatus> PublishAsync(TMessage message)
        {
            return await this.publisher.PublishAsync(message, this.defaults.Exchange, this.defaults.Topic, this.defaults.Mandatory).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.publisher?.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}