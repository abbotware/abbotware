// -----------------------------------------------------------------------
// <copyright file="ActionConsumer{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Base
{
    using System;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    /// Consumer framework class that uses an action as a callback. (Useful for unit tests)
    /// </summary>
    /// <typeparam name="TMessage">Message type</typeparam>
    public class ActionConsumer<TMessage> : MqConsumer<TMessage>
    {
        /// <summary>
        /// injected callback action for message handler
        /// </summary>
        private readonly Action<TMessage> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionConsumer{TMessage}"/> class.
        /// </summary>
        /// <param name="action">callback for message handling</param>
        /// <param name="acknowledger">injected plugin for message acking</param>
        /// <param name="protocol">message protcol</param>
        /// <param name="consumer">injected plugin for consuming</param>
        /// <param name="logger">injected logger</param>
        public ActionConsumer(Action<TMessage> action, IBasicAcknowledger acknowledger, IMessageProtocol<TMessage> protocol, IBasicConsumer consumer, ILogger logger)
            : base(acknowledger, protocol, consumer, logger)
        {
            Arguments.NotNull(action, nameof(action));
            Arguments.NotNull(acknowledger, nameof(acknowledger));
            Arguments.NotNull(protocol, nameof(protocol));
            Arguments.NotNull(consumer, nameof(consumer));
            Arguments.NotNull(logger, nameof(logger));

            this.action = action;
        }

        /// <inheritdoc/>
        public override void OnHandle(TMessage message)
        {
            this.action(message);
        }
    }
}
