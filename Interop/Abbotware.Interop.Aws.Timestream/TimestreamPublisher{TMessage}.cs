// -----------------------------------------------------------------------
// <copyright file="TimestreamPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Amazon;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamPublisher<TMessage> : BaseComponent<ITimestreamOptions>, IMessagePublisher<TMessage>
    {
        private readonly AmazonTimestreamWriteClient writeClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="logger">injected logger</param>
        public TimestreamPublisher(ITimestreamOptions options, ILogger logger)
            : base(options, logger)
        {
            var writeClientConfig = new AmazonTimestreamWriteConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(options.Region),
                Timeout = TimeSpan.FromSeconds(100),
                MaxErrorRetry = 10,
            };

            this.writeClient = new AmazonTimestreamWriteClient(writeClientConfig);
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(TMessage message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(new TMessage[] { message }, ct);
        }

        /// <summary>
        /// writes records
        /// </summary>
        /// <param name="messages">records to write</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>publish status</returns>
        public async ValueTask<PublishStatus> WriteRecordsAsync(TMessage[] messages, CancellationToken ct)
        {
            var time = DateTimeOffset.UtcNow;
            var records = new List<Record>();

            foreach (var message in messages)
            {
                var d = this.OnMessageDimensions(message);
                var m = this.OnMessageMeasures(message);

                var record = new Record
                {
                    Dimensions = d,
                    Version = 1,
                };

                this.OnMessageTime(message, record, time);

                if (m.Count == 1)
                {
                    var s = m.Single();
                    record.MeasureName = s.Name;
                    record.MeasureValue = s.Value;
                    record.MeasureValueType = s.Type;
                }
                else
                {
                    record.MeasureValues = m;
                }
            }

            var writeRecordsRequest = new WriteRecordsRequest
            {
                DatabaseName = this.Configuration.Database,
                TableName = this.Configuration.Table,
                Records = records,
            };

            var result = await this.writeClient.WriteRecordsAsync(writeRecordsRequest, ct)
                .ConfigureAwait(false);

            if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return PublishStatus.Unknown;
            }

            if (result.RecordsIngested.Total != 1)
            {
                return PublishStatus.Unknown;
            }

            return PublishStatus.Confirmed;
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.writeClient.Dispose();

            base.OnDisposeManagedResources();
        }

        /// <summary>
        ///  Hook to implement custom logic that gets the message dimensions values
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>dimension properties</returns>
        protected virtual List<Dimension> OnMessageDimensions(TMessage message)
        {
            return new();
        }

        /// <summary>
        ///  Hook to implement custom logic that gets the message mesaure values
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>dimension properties</returns>
        protected virtual List<MeasureValue> OnMessageMeasures(TMessage message)
        {
            return new();
        }

        /// <summary>
        ///  Hook to implement custom logic that sets the message time value
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="record">timestream record object</param>
        /// <param name="fallbackTime">fallback time if there is none present on the message</param>
        protected virtual void OnMessageTime(TMessage message, Record record, DateTimeOffset fallbackTime)
        {
            var t = fallbackTime.ToUnixTimeMilliseconds();
            record.Time = t.ToString(CultureInfo.InvariantCulture);
            record.TimeUnit = TimeUnit.MILLISECONDS;
        }
    }
}
