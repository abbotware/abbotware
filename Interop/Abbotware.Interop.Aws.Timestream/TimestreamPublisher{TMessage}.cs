// -----------------------------------------------------------------------
// <copyright file="TimestreamPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamPublisher<TMessage> : AwsConnection<AmazonTimestreamWriteClient, AmazonTimestreamWriteConfig, TimestreamOptions>, IMessageBatchPublisher<TMessage>
    {
        private readonly ITimestreamProtocol<TMessage> protocol;

#if NET6_0_OR_GREATER
        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamPublisher(TimestreamOptions options, ITimestreamProtocol<TMessage> protocol, Microsoft.Extensions.Logging.ILogger<TimestreamPublisher<TMessage>> logger)
            : this(new AmazonTimestreamWriteClient(), options, protocol, new LoggingAdapter(logger))
        {
        }
#else
        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamPublisher(TimestreamOptions options, ITimestreamProtocol<TMessage> protocol, ILogger logger)
            : this(new AmazonTimestreamWriteClient(), options, protocol, logger)
        {
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamPublisher(AmazonTimestreamWriteClient client, TimestreamOptions options, ITimestreamProtocol<TMessage> protocol, ILogger logger)
            : base(client, options, logger)
        {
            this.protocol = protocol;
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(TMessage message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(new TMessage[] { message }, ct);
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(TMessage[] message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(message, ct);
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(IEnumerable<TMessage> message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(message.ToArray(), ct);
        }

        /// <summary>
        /// writes records
        /// </summary>
        /// <param name="messages">records to write</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>publish status</returns>
        public async ValueTask<PublishStatus> WriteRecordsAsync(TMessage[] messages, CancellationToken ct)
        {
            var request = this.protocol.Encode(messages, this.Configuration);

            var result = await this.Client.WriteRecordsAsync(request, ct)
                .ConfigureAwait(false);

            if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return PublishStatus.Unknown;
            }

            if (result.RecordsIngested.Total != messages.Length)
            {
                return PublishStatus.Unknown;
            }

            return PublishStatus.Confirmed;
        }
    }
}
