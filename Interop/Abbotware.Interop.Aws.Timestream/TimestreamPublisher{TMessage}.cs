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
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Chrono;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamPublisher<TMessage> : AwsConnection<AmazonTimestreamWriteClient, AmazonTimestreamWriteConfig, TimestreamOptions>, IMessageBatchPublisher<TMessage>
        where TMessage : notnull
    {
        private volatile int globalRecordsPublished;

        private volatile int globalRecordsNotIngested;

        private volatile int currentRecordsPublished;

        private volatile int currentRecordsNotIngested;

        private MinimumTimeSpan loggingTimeSpan;

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
            this.loggingTimeSpan = new(options.MinimumTimeBetweenLogging);
        }

        /// <summary>
        /// gets the count of that were not injested by AWS Timestream
        /// </summary>
        public long RecordsNotIngested => this.globalRecordsNotIngested;

        /// <summary>
        /// gets the count of records published to AWS Timestream
        /// </summary>
        public long RecordsPublished => this.globalRecordsPublished;

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

        /// <inheritdoc/>
        protected override bool OnShouldLog(Exception exception)
        {
            // this is logged by the write caller - will result in a double logged exception
            if (exception is AmazonTimestreamWriteException)
            {
                return false;
            }

            return base.OnShouldLog(exception);
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

            try
            {
                var sw = Stopwatch.StartNew();

                var result = await this.Client.WriteRecordsAsync(request, ct)
                    .ConfigureAwait(false);

                sw.Stop();

                var notPublished = result.RecordsIngested.Total - request.Records.Count;

                // global stats
                Interlocked.Add(ref this.globalRecordsNotIngested, notPublished);
                Interlocked.Add(ref this.globalRecordsPublished, request.Records.Count);

                // current stats
                Interlocked.Add(ref this.currentRecordsNotIngested, notPublished);
                Interlocked.Add(ref this.currentRecordsPublished, request.Records.Count);

                if (notPublished > 0)
                {
                    this.Logger.Warn($"WriteRecordsAsync={result.HttpStatusCode}  time:{sw.Elapsed} records:{request.Records.Count} != injested:{result.RecordsIngested.Total}");
                }
                else
                {
                    if (this.loggingTimeSpan.IsExpired)
                    {
                        var global = $"Global [Published:{this.globalRecordsPublished} Not Published:{this.globalRecordsNotIngested}]";
                        var current = $"Current [TimeSpan:{this.loggingTimeSpan.MinimumWaitTime}  Published:{this.currentRecordsPublished} Avg Rate:{(double)this.currentRecordsPublished / this.loggingTimeSpan.MinimumWaitTime.TotalSeconds} Pub/Sec.  Problems:{this.currentRecordsNotIngested}] for this time span";
                        this.Logger.Debug($"WriteRecordsAsync {global}  {current}");

                        this.currentRecordsNotIngested = 0;
                        this.currentRecordsPublished = 0;
                    }
                }

                if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    return PublishStatus.Returned;
                }

                if (notPublished > 0)
                {
                    return PublishStatus.Unknown;
                }

                return PublishStatus.Confirmed;
            }
            catch (RejectedRecordsException ex)
            {
                var sb = new StringBuilder();
                sb.Append("RejectedRecords");

                foreach (var r in ex.RejectedRecords)
                {
                    sb.Append("[{r.RecordIndex}][{r.ExistingVersion}] = {r.Reason} ");
                }

                this.Logger.Error(sb.ToString());

                return PublishStatus.Unknown;
            }
        }
    }
}
