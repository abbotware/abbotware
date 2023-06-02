// -----------------------------------------------------------------------
// <copyright file="BaseTimestreamPublisher{TMessage}.cs" company="Abbotware, LLC">
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
    using Amazon.Runtime;
    using Amazon.Runtime.EventStreams.Internal;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public abstract class BaseTimestreamPublisher<TMessage> : BaseComponent<ITimestreamOptions>, IMessageBatchPublisher<TMessage>
    {
        /// <summary>
        /// map of C# type to timestream measure value type
        /// </summary>
        public static readonly IReadOnlyDictionary<Type, MeasureValueType> MeasureTypes = new Dictionary<Type, MeasureValueType>
        {
            {
                typeof(int), MeasureValueType.BIGINT
            },
            {
                typeof(short), MeasureValueType.BIGINT
            },
            {
                typeof(long), MeasureValueType.BIGINT
            },
            {
                typeof(byte), MeasureValueType.BIGINT
            },
            {
                typeof(uint), MeasureValueType.BIGINT
            },
            {
                typeof(ushort), MeasureValueType.BIGINT
            },
            {
                typeof(ulong), MeasureValueType.BIGINT
            },
            {
                typeof(sbyte), MeasureValueType.BIGINT
            },
            {
                typeof(float), MeasureValueType.DOUBLE
            },
            {
                typeof(double), MeasureValueType.DOUBLE
            },
            {
                typeof(decimal), MeasureValueType.DOUBLE
            },
            {
                typeof(DateTime), MeasureValueType.TIMESTAMP
            },
            {
                typeof(DateTimeOffset), MeasureValueType.TIMESTAMP
            },
            {
                typeof(bool), MeasureValueType.BOOLEAN
            },
            {
                typeof(string), MeasureValueType.VARCHAR
            },
        };

        /// <summary>
        /// map of C# type to timestream dimension type
        /// </summary>
        public static readonly IReadOnlyDictionary<Type, DimensionValueType> DimensionTypes = new Dictionary<Type, DimensionValueType>
        {
            {
                typeof(string), DimensionValueType.VARCHAR
            },
        };

        private readonly AmazonTimestreamWriteClient writeClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="logger">injected logger</param>
        protected BaseTimestreamPublisher(ITimestreamOptions options, ILogger logger)
            : base(options, logger)
        {
            var writeClientConfig = new AmazonTimestreamWriteConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(options.Region),
                Timeout = TimeSpan.FromSeconds(100),
                MaxErrorRetry = 10,
            };

            this.writeClient = new AmazonTimestreamWriteClient(writeClientConfig);

            this.writeClient.ExceptionEvent += this.OnExceptionEvent;
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(TMessage message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(new TMessage[] { message }, ct);
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(TMessage[] message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(message.AsEnumerable(), ct);
        }

        /// <inheritdoc/>
        public ValueTask<PublishStatus> PublishAsync(IEnumerable<TMessage> message, CancellationToken ct)
        {
            return this.WriteRecordsAsync(message, ct);
        }

        /// <summary>
        /// writes records
        /// </summary>
        /// <param name="messages">records to write</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>publish status</returns>
        public async ValueTask<PublishStatus> WriteRecordsAsync(IEnumerable<TMessage> messages, CancellationToken ct)
        {
            var time = DateTimeOffset.UtcNow;
            var records = new List<Record>();

            foreach (var message in messages)
            {
                var d = this.OnCreateDimensions(message);
                var m = this.OnCreateMeasures(message);

                var record = new Record
                {
                    Dimensions = d,
                    Version = 1,
                };

                this.OnSetTime(message, record, time);

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
                    record.MeasureValueType = MeasureValueType.MULTI;
                }

                records.Add(record);
            }

            var writeRecordsRequest = new WriteRecordsRequest
            {
                DatabaseName = this.Configuration.Database,
                TableName = this.Configuration.Table,
                Records = records,
                CommonAttributes = this.OnCreateCommonAttributes(),
            };

            var result = await this.writeClient.WriteRecordsAsync(writeRecordsRequest, ct)
            .ConfigureAwait(false);

            if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                return PublishStatus.Unknown;
            }

            if (result.RecordsIngested.Total != records.Count)
            {
                return PublishStatus.Unknown;
            }

            return PublishStatus.Confirmed;
        }

        /// <summary>
        /// Serializesthe time into the record
        /// </summary>
        /// <param name="record">record to update</param>
        /// <param name="time">time value</param>
        /// <param name="type">time unit</param>
        /// <exception cref="NotSupportedException">currently unsupported time unit</exception>
        protected static void WriteRecordTime(Record record, DateTimeOffset time, TimeUnitType type)
        {
            switch (type)
            {
                case TimeUnitType.Milliseconds:
                    record.Time = GetTimeMeasureValue(time);
                    record.TimeUnit = TimeUnit.MILLISECONDS;
                    break;
                default:
                    throw new NotSupportedException($"TimeUnitType:{type} currently unsupported");
            }
        }

        /// <summary>
        /// Gets the time value (milliseconds)
        /// </summary>
        /// <param name="time">time</param>
        /// <returns>time measure value</returns>
        protected static string GetTimeMeasureValue(DateTimeOffset time)
        {
            var t = time.ToUnixTimeMilliseconds();
            return t.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.writeClient.Dispose();

            this.writeClient.ExceptionEvent -= this.OnExceptionEvent;

            base.OnDisposeManagedResources();
        }

        /// <summary>
        ///  Hook to implement custom logic that gets the message dimensions values
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>dimension properties</returns>
        protected abstract List<Dimension> OnCreateDimensions(TMessage message);

        /// <summary>
        ///  Hook to implement custom logic that gets the message mesaure values
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>dimension properties</returns>
        protected abstract List<MeasureValue> OnCreateMeasures(TMessage message);

        /// <summary>
        ///  Hook to implement custom logic that sets the message time value
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="record">timestream record object</param>
        /// <param name="fallbackTime">fallback time if there is none present on the message</param>
        protected abstract void OnSetTime(TMessage message, Record record, DateTimeOffset fallbackTime);

        /// <summary>
        ///  Hook to implement custom logic that creates te common attributes
        /// </summary>
        /// <returns>Common Attributes record</returns>
        protected abstract Record? OnCreateCommonAttributes();

        private void OnExceptionEvent(object sender, ExceptionEventArgs e)
        {
            switch (e)
            {
                case WebServiceExceptionEventArgs wsee:
                    this.Logger.Error(wsee.Exception, "OnExceptionEvent");
                    break;
                default:
                    this.Logger.Error(e?.ToString() ?? "unknown exception");
                    break;
            }
        }
    }
}
