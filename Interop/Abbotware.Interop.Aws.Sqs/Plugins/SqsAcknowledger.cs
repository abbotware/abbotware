// -----------------------------------------------------------------------
// <copyright file="SqsAcknowledger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon.SQS;
    using global::Amazon.SQS.Model;

    /// <summary>
    /// Channel manager used for ack,nack, and reject operations
    /// </summary>
    public class SqsAcknowledger : BaseSqsClient, IBasicAcknowledger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqsAcknowledger"/> class.
        /// </summary>
        /// <param name="client">Amazon client</param>
        /// <param name="configuration">injected configuration</param>
        /// <param name="logger">injected logger</param>
        protected SqsAcknowledger(AmazonSQSClient client, ISqsSettings configuration, ILogger logger)
       : base(client, configuration, logger)
        {
            Arguments.NotNull(client, nameof(client));
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public void Ack(IMessageEnvelope envelope)
        {
            this.Ack(envelope, false);
        }

        /// <inheritdoc />
        public void Ack(IMessageEnvelope envelope, bool multiple)
        {
            envelope = Arguments.EnsureNotNull(envelope, nameof(envelope));

            this.InitializeIfRequired();

            var q = envelope.PublishProperties.Exchange;

            if (envelope.DeliveryProperties.DeliveryTag.IsBlank())
            {
                throw new ArgumentException("missing delievery tag");
            }

            var r = envelope.DeliveryProperties.DeliveryTag!.ToString(CultureInfo.InvariantCulture);

            this.Delete(q, r);
        }

        /// <summary>
        /// Deletes messages from the queue
        /// </summary>
        /// <param name="queue">queue name</param>
        /// <param name="envelopes">list of envelopes to delete</param>
        public void Delete(string queue, IEnumerable<IMessageEnvelope> envelopes)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var count = 0;
                var entries = envelopes.Select(x => new DeleteMessageBatchRequestEntry((count++).ToString(CultureInfo.InvariantCulture), x.DeliveryProperties.DeliveryTag)).ToList();

                var dmbr = new DeleteMessageBatchRequest(queue, entries);

                var response = this.Client.DeleteMessageBatchAsync(dmbr).Result;

                VerifyDeleteResponse(queue, $"batch Size:{entries.Count}", response);
            }
        }

        /// <summary>
        /// Deletes messages from the queue
        /// </summary>
        /// <param name="queue">queue name</param>
        /// <param name="receiptHandle">receipt handle of all messages recieved</param>
        public void Delete(string queue, string receiptHandle)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var dmr = new DeleteMessageRequest(queue, receiptHandle);

                var response = this.Client.DeleteMessageAsync(dmr).Result;

                VerifyDeleteResponse(queue, receiptHandle, response);
            }
        }
    }
}