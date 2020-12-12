// -----------------------------------------------------------------------
// <copyright file="CallbackConsumer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ
{
    using System;
    using Abbotware.Core.Services;
    using global::RabbitMQ.Client;

    /// <summary>
    /// Consumer that calls a user supplied callback for each message received from rabbitAMQP
    /// </summary>
    public class CallbackConsumer : BaseConsumer
    {
        /// <summary>
        /// the callback invoked when a message arrives
        /// </summary>
        private readonly HandleBasicDelivery callback;

        /// <summary>
        /// Initializes a new instance of the CallbackConsumer class
        /// </summary>
        /// <param name="callback">the callback invoked when a message arrives</param>
        /// <param name="acknowledgementManager">injected acknowledgement manager that can ack messages</param>
        /// <param name="logger">injected logger</param>
        public CallbackConsumer(HandleBasicDelivery callback, IAcknowledgementManager acknowledgementManager, ILogger logger)
            : base(acknowledgementManager, logger)
        {
            Contract.Requires<ArgumentNullException>(callback != null, "callback  is null");
            Contract.Requires<ArgumentNullException>(acknowledgementManager != null, "acknowledgementManager  is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger  is null");

            this.callback = callback;
        }

        /// <inheritdoc />
       protected override void OnHandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            this.callback(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, this.AcknowledgementManager, body);
        }
    }
}
