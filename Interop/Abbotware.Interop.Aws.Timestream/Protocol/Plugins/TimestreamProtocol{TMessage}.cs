// -----------------------------------------------------------------------
// <copyright file="TimestreamProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// message encoding protocol
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamProtocol<TMessage> : BaseComponent, ITimestreamProtocol<TMessage>
    {
        private readonly Type type = typeof(TMessage);

        private readonly HashSet<string> fields = new();

        private readonly IReadOnlyDictionary<string, DimensionValueOptions<TMessage>> dimensions;

        private readonly IReadOnlyDictionary<string, MeasureValueOptions<TMessage>> measures;

        private readonly TimeValueOptions<TMessage> time;

        private readonly string measureName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamProtocol{TMessage}"/> class.
        /// </summary>
        /// <param name="dimensions">dimension lookups</param>
        /// <param name="measures">measure lookups</param>
        /// <param name="logger">injected logger</param>
        public TimestreamProtocol(IReadOnlyDictionary<string, DimensionValueOptions<TMessage>> dimensions, IReadOnlyDictionary<string, MeasureValueOptions<TMessage>> measures, ILogger logger)
            : this(dimensions, measures, new(TimeUnitType.Milliseconds, x => DateTimeOffset.UtcNow), string.Empty, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamProtocol{TMessage}"/> class.
        /// </summary>
        /// <param name="dimensions">dimension lookups</param>
        /// <param name="measures">measure lookups</param>
        /// <param name="time">time function</param>
        /// <param name="logger">injected logger</param>
        public TimestreamProtocol(IReadOnlyDictionary<string, DimensionValueOptions<TMessage>> dimensions, IReadOnlyDictionary<string, MeasureValueOptions<TMessage>> measures, TimeValueOptions<TMessage> time, ILogger logger)
            : this(dimensions, measures, time, string.Empty, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamProtocol{TMessage}"/> class.
        /// </summary>
        /// <param name="dimensions">dimension lookups</param>
        /// <param name="measures">measure lookups</param>
        /// <param name="measureName">time function</param>
        /// <param name="logger">injected logger</param>
        public TimestreamProtocol(IReadOnlyDictionary<string, DimensionValueOptions<TMessage>> dimensions, IReadOnlyDictionary<string, MeasureValueOptions<TMessage>> measures, string measureName, ILogger logger)
            : this(dimensions, measures, new(TimeUnitType.Milliseconds, x => DateTimeOffset.UtcNow), measureName, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamProtocol{TMessage}"/> class.
        /// </summary>
        /// <param name="dimensions">dimension lookups</param>
        /// <param name="measures">measure lookups</param>
        /// <param name="time">time function</param>
        /// <param name="measureName">measure name</param>
        /// <param name="logger">injected logger</param>
        public TimestreamProtocol(IReadOnlyDictionary<string, DimensionValueOptions<TMessage>> dimensions, IReadOnlyDictionary<string, MeasureValueOptions<TMessage>> measures, TimeValueOptions<TMessage> time, string measureName, ILogger logger)
            : base(logger)
        {
            this.dimensions = dimensions;
            this.measures = measures;
            this.time = time;
            this.measureName = measureName;

            AddFields(dimensions);
            AddFields(measures);

            void AddFields<TOptions>(IReadOnlyDictionary<string, TOptions> dimensions)
            {
                foreach (var k in dimensions.Keys)
                {
                    if (!this.fields.Add(k))
                    {
                        throw new ArgumentException($"duplicate field:{k} message:{this.type.FullName}");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public virtual WriteRecordsRequest Encode(TMessage value)
        {
            return this.Encode(new[] { value }, this.time);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(IEnumerable<TMessage> values)
        {
            return this.Encode(values.ToArray(), this.time);
        }

        /// <inheritdoc/>
        public virtual WriteRecordsRequest Encode(TMessage[] values)
        {
            return this.Encode(values, this.time);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage message, TimestreamOptions options)
        {
            return this.Encode(message, options, this.time);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage[] messages, TimestreamOptions options)
        {
            return this.Encode(messages, options, this.time);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage message, TimestreamOptions options, TimeValueOptions<TMessage> timestamp)
        {
            return this.Encode(new[] { message }, options, timestamp);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage[] messages, TimestreamOptions options, TimeValueOptions<TMessage> timestamp)
        {
            var request = this.Encode(messages, timestamp);
            request.DatabaseName = options.Database;
            request.TableName = options.Table;

            return request;
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage message, TimeValueOptions<TMessage> timestamp)
        {
            return this.Encode(new[] { message }, timestamp);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage[] messages, TimeValueOptions<TMessage> timestamp)
        {
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

                this.OnRecordTime(message, record, timestamp);

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
                Records = records,
                CommonAttributes = this.OnCreateCommonAttributes(records),
            };

            return writeRecordsRequest;
        }

        /// <summary>
        /// Serializesthe time into the record
        /// </summary>
        /// <param name="message">message </param>
        /// <param name="record">record to update</param>
        /// <param name="timestamp">timestamp lookup</param>
        /// <exception cref="NotSupportedException">currently unsupported time unit</exception>
        protected static void WriteRecordTime(TMessage message, Record record, TimeValueOptions<TMessage> timestamp)
        {
            var time = timestamp.Lookup(message);

            switch (timestamp.Type)
            {
                case TimeUnitType.Milliseconds:
                    record.Time = time.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);
                    record.TimeUnit = TimeUnit.MILLISECONDS;
                    break;
                default:
                    throw new NotSupportedException($"TimeUnitType:{timestamp.Type} currently unsupported");
            }
        }

        /// <summary>
        ///  Hook to implement custom logic that gets the message dimensions values
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>dimension properties</returns>
        protected virtual List<Dimension> OnCreateDimensions(TMessage message)
        {
            var l = new List<Dimension>();

            foreach (var dimension in this.dimensions)
            {
                var d = new Dimension
                {
                    DimensionValueType = DimensionValueType.VARCHAR,
                    Name = dimension.Key,
                    Value = dimension.Value.Lookup(message),
                };

                l.Add(d);
            }

            return l;
        }

        /// <summary>
        ///  Hook to implement custom logic that gets the message mesaure values
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>dimension properties</returns>
        protected virtual List<MeasureValue> OnCreateMeasures(TMessage message)
        {
            var l = new List<MeasureValue>();

            foreach (var measure in this.measures)
            {
                var mv = new MeasureValue
                {
                    Type = measure.Value.Type,
                    Name = measure.Key,
                    Value = measure.Value.Lookup(message),
                };

                l.Add(mv);
            }

            return l;
        }

        /// <summary>
        ///  Hook to implement custom logic that sets the message time value
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="record">timestream record object</param>
        /// <param name="timestamp">timestamp lookup</param>
        protected virtual void OnRecordTime(TMessage message, Record record, TimeValueOptions<TMessage> timestamp)
        {
            WriteRecordTime(message, record, timestamp);
        }

        /// <summary>
        ///  Hook to implement custom logic that creates te common attributes
        /// </summary>
        /// <param name="records">all created records</param>
        /// <returns>Common Attributes record</returns>
        protected virtual Record? OnCreateCommonAttributes(IReadOnlyCollection<Record> records)
        {
            if (this.measureName.IsNotBlank())
            {
                return new Record
                {
                    MeasureName = this.measureName,
                };
            }

            return null;
        }
    }
}
