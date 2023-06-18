// -----------------------------------------------------------------------
// <copyright file="TimestreamPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Amazon.TimestreamWrite;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamPublisher<TMessage> : AwsConnection<AmazonTimestreamWriteClient, AmazonTimestreamWriteConfig, TimestreamOptions>, IMessageBatchPublisher<TMessage>
        where TMessage : notnull
    {
        private volatile int recordsInjested;

        private volatile int recordsPublished;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamPublisher(TimestreamOptions options, ITimestreamWriteProtocol<TMessage> protocol, ILogger<TimestreamPublisher<TMessage>> logger)
            : this(new AmazonTimestreamWriteClient(), options, protocol, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamPublisher(AmazonTimestreamWriteClient client, TimestreamOptions options, ITimestreamWriteProtocol<TMessage> protocol, ILogger<TimestreamPublisher<TMessage>> logger)
            : base(client, options, logger)
        {
            this.Protocol = protocol;
        }

        /// <summary>
        /// gets the count of records injested by AWS Timestream
        /// </summary>
        public long RecordsIngested => this.recordsInjested;

        /// <summary>
        /// gets the count of records published to AWS Timestream
        /// </summary>
        public long RecordsPublished => this.recordsPublished;

        /// <summary>
        /// gets the protocol encoder
        /// </summary>
        public ITimestreamWriteProtocol<TMessage> Protocol { get; }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(TMessage message, CancellationToken ct)
        {
            return this.OnPublishAsync(new TMessage[] { message }, ct);
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(IEnumerable<TMessage> message, CancellationToken ct)
        {
            return this.OnPublishAsync(message, ct);
        }

        /// <summary>
        /// writes records
        /// </summary>
        /// <param name="messages">records to write</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>publish status</returns>
        protected virtual ValueTask<PublishStatus> OnPublishAsync(IEnumerable<TMessage> messages, CancellationToken ct)
        {
            return this.OnWriteRecordsAsync(messages, ct);
        }

        /// <summary>
        /// writes records
        /// </summary>
        /// <param name="messages">records to write</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>publish status</returns>
        protected virtual async ValueTask<PublishStatus> OnWriteRecordsAsync(IEnumerable<TMessage> messages, CancellationToken ct)
        {
            var request = this.Protocol.Encode(messages, this.Configuration);

            if (request.Records.Count > TimesreamConstants.MaxRecordBatch)
            {
                throw new InvalidOperationException($"Can not send more than {TimesreamConstants.MaxRecordBatch} records to AWS Timestream");
            }

            var sw = Stopwatch.StartNew();

            var result = await this.Client.WriteRecordsAsync(request, ct)
                .ConfigureAwait(false);

            sw.Stop();

            var all = result.RecordsIngested.Total == request.Records.Count;

            Interlocked.Add(ref this.recordsPublished, request.Records.Count);
            Interlocked.Add(ref this.recordsInjested, result.RecordsIngested.Total);

            if (!all)
            {
                this.Logger.Warn($"WriteRecordsAsync={result.HttpStatusCode}  time:{sw.Elapsed} records:{request.Records.Count} != injested:{result.RecordsIngested.Total}");
            }
            else
            {
                this.Logger.Debug($"WriteRecordsAsync={result.HttpStatusCode}  time:{sw.Elapsed} injested:{result.RecordsIngested.Total}");
            }

            if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return PublishStatus.Returned;
            }

            if (!all)
            {
                return PublishStatus.Unknown;
            }

            return PublishStatus.Confirmed;
        }
    }
}
