// -----------------------------------------------------------------------
// <copyright file="SqsRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon.SQS;
    using global::Amazon.SQS.Model;
    using ProtoBuf;

    /// <summary>
    ///     Channel manager used for publishing operations
    /// </summary>
    public class SqsRetriever : SqsAcknowledger, IBasicRetriever
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SqsRetriever" /> class.
        /// </summary>
        /// <param name="client">SQS client</param>
        /// <param name="configuration">injected configuration</param>
        /// <param name="logger">injected logger</param>
        public SqsRetriever(AmazonSQSClient client, ISqsSettings configuration, ILogger logger)
            : base(client, configuration, logger)
        {
            Arguments.NotNull(client, nameof(client));
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IMessageEnvelope>> Get(string queueName, bool noAck)
        {
            var rmr = new ReceiveMessageRequest
            {
                QueueUrl = queueName,
                MaxNumberOfMessages = 10,
            };

            var r = await this.Client.ReceiveMessageAsync(rmr).ConfigureAwait(false);

            var l = new List<IMessageEnvelope>(r.Messages.Count);

            foreach (var m in r.Messages)
            {
                var e = new MessageEnvelope
                {
                    Body = Convert.FromBase64String(m.Body),
                };

                e.DeliveryProperties.DeliveryTag = m.ReceiptHandle;
                e.PublishProperties.Exchange = this.Configuration.Queue;
                l.Add(e);
            }

            return l;
        }

        /// <summary>
        ///  Test retrieve
        /// </summary>
        /// <param name="queue">queue name</param>
        /// <returns>async message task</returns>
        [Obsolete("not for external use")]
        public async Task<IEnumerable<string>> StringRetrieveAsync(string queue)
        {
            var envelopes = await this.Get(queue, false)
                .ConfigureAwait(false);

            var m = envelopes
                .Select(e => Encoding.UTF8.GetString(e.Body.ToArray()))
                .ToList();

            return m;
        }

        /// <summary>
        ///  Test retrieve
        /// </summary>
        /// <returns>async message task</returns>
        [Obsolete("not for external use")]
        public async Task<IEnumerable<string>> StringRetrieveAsync()
        {
            return await this.StringRetrieveAsync(this.Configuration.Queue).ConfigureAwait(false);
        }

        /// <summary>
        ///  Test retrieve
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="queue">queue name</param>
        /// <returns>async message task</returns>
        [Obsolete("not for external use")]
        public async Task<IEnumerable<TMessage>> ProtoRetrieveAsync<TMessage>(string queue)
        {
            var envelopes = await this.Get(queue, false).ConfigureAwait(false);

            var m = envelopes
           .Select(e =>
           {
               using var stream = new MemoryStream(e.Body.ToArray());

               return Serializer.Deserialize<TMessage>(stream);
           })
           .ToList();

            return m;
        }

        /// <summary>
        ///  Test retrieve
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <returns>async message task</returns>
        [Obsolete("not for external use")]
        public async Task<IEnumerable<TMessage>> ProtoRetrieveAsync<TMessage>()
        {
            return await this.ProtoRetrieveAsync<TMessage>(this.Configuration.Queue).ConfigureAwait(false);
        }
    }
}