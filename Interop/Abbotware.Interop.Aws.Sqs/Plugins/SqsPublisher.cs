// -----------------------------------------------------------------------
// <copyright file="SqsPublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Threading.Counters;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon.SQS;
    using global::Amazon.SQS.Model;
    using ProtoBuf;

    /// <summary>
    ///     Channel manager used for publishing operations
    /// </summary>
    public class SqsPublisher : BaseSqsClient, IBasicPublisher
    {
        /// <summary>
        ///     Counter for messages published
        /// </summary>
        private readonly AtomicCounter published = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqsPublisher" /> class.
        /// </summary>
        /// <param name="client">SQS client</param>
        /// <param name="configuration">injected configuration</param>
        /// <param name="logger">injected logger</param>
        public SqsPublisher(AmazonSQSClient client, ISqsSettings configuration, ILogger logger)
            : base(client, configuration, logger)
        {
            Arguments.NotNull(client, nameof(client));
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Gets the number of messaged published
        /// </summary>
        public long PublishedMessages => this.published.Value;

        /// <inheritdoc />
        public ValueTask<PublishStatus> Publish(byte[] body, IPublishProperties properties)
        {
            properties = Arguments.EnsureNotNull(properties, nameof(properties));

            var envelope = new MessageEnvelope();

            envelope.PublishProperties.Exchange = properties.Exchange;
            envelope.PublishProperties.RoutingKey = properties.RoutingKey;
            envelope.PublishProperties.Mandatory = properties.Mandatory;
            envelope.PublishProperties.Persistent = properties.Persistent;
            envelope.Body = body;

            return this.Publish(envelope);
        }

        /// <inheritdoc />
        public async ValueTask<PublishStatus> Publish(IMessageEnvelope envelope)
        {
            envelope = Arguments.EnsureNotNull(envelope, nameof(envelope));

            this.InitializeIfRequired();

            var smr = new SendMessageRequest
            {
                QueueUrl = envelope.PublishProperties.Exchange,
                MessageBody = Convert.ToBase64String(envelope.Body.ToArray()),
            };

            this.Logger.Debug("Published:{0}", this.published.Value);

            var response = await this.Client.SendMessageAsync(smr).ConfigureAwait(false);

            this.Logger.Debug("SendMessageAsync: response={0}", response);

            this.published.Increment();

            if (IsSuccess(response))
            {
                return PublishStatus.Confirmed;
            }

            return PublishStatus.Unknown;
        }

        /// <summary>
        /// SQS Test Publish
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>asyc task handle</returns>
        [Obsolete("not for external use")]
        public async Task<PublishStatus> StringPublishAsync(string input)
        {
            return await this.StringPublishAsync(input, this.Configuration.Queue).ConfigureAwait(false);
        }

        /// <summary>
        /// SQS Test Publish
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="queue">queue name</param>
        /// <returns>asyc task handle</returns>
        [Obsolete("not for external use")]
        public async Task<PublishStatus> StringPublishAsync(string input, string queue)
        {
            var m = new MessageEnvelope();
            m.PublishProperties.Exchange = queue;
            m.Body = Encoding.UTF8.GetBytes(input);

            return await this.Publish(m).ConfigureAwait(false);
        }

        /// <summary>
        /// SQS Test Publish
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="input">message input</param>
        /// <returns>asyc task handle</returns>
        [Obsolete("not for external use")]
        public async Task<PublishStatus> ProtoPublishAsync<TMessage>(TMessage input)
        {
            return await this.ProtoPublishAsync(input, this.Configuration.Queue).ConfigureAwait(false);
        }

        /// <summary>
        /// SQS Test Publish
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="input">message input</param>
        /// <param name="queue">queue name</param>
        /// <returns>asyc task handle</returns>
        [Obsolete("not for external use")]
        public async Task<PublishStatus> ProtoPublishAsync<TMessage>(TMessage input, string queue)
        {
            var m = new MessageEnvelope();
            m.PublishProperties.Exchange = queue;

            using var stream = new MemoryStream();

            Serializer.Serialize(stream, input);

            m.Body = stream.ToArray();

            return await this.Publish(m).ConfigureAwait(false);
        }
    }
}