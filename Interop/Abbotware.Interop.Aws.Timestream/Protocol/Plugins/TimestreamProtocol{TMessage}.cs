// -----------------------------------------------------------------------
// <copyright file="TimestreamProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Internal;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamQuery.Model;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// message encoding protocol
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamProtocol<TMessage> : BaseComponent, ITimestreamProtocol<TMessage>
        where TMessage : notnull
    {
        private readonly System.Type type = typeof(TMessage);

        private readonly HashSet<string> fields = new();

        private readonly IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, Dimension>> dimensions;

        private readonly IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>> measures;

        private readonly IRecordUpdater<TMessage> time;

        private readonly string measureName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamProtocol{TMessage}"/> class.
        /// </summary>
        /// <param name="dimensions">dimension lookups</param>
        /// <param name="measures">measure lookups</param>
        /// <param name="logger">injected logger</param>
        public TimestreamProtocol(IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, Dimension>> dimensions, IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>> measures, ILogger logger)
            : this(dimensions, measures, new TimeValueOptions<TMessage, DateTimeOffset>(TimeUnitType.Milliseconds, x => DateTimeOffset.UtcNow, x => x, string.Empty), string.Empty, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamProtocol{TMessage}"/> class.
        /// </summary>
        /// <param name="dimensions">dimension lookups</param>
        /// <param name="measures">measure lookups</param>
        /// <param name="time">time function</param>
        /// <param name="logger">injected logger</param>
        public TimestreamProtocol(IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, Dimension>> dimensions, IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>> measures, IRecordUpdater<TMessage> time, ILogger logger)
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
        public TimestreamProtocol(IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, Dimension>> dimensions, IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>> measures, string measureName, ILogger logger)
            : this(dimensions, measures, new TimeValueOptions<TMessage, DateTimeOffset>(TimeUnitType.Milliseconds, x => DateTimeOffset.UtcNow, x => x, string.Empty), measureName, logger)
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
        public TimestreamProtocol(IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, Dimension>> dimensions, IReadOnlyDictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>> measures, IRecordUpdater<TMessage> time, string measureName, ILogger logger)
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

        /// <summary>
        /// Gets a value indicating whether or not this is a MULTI record
        /// </summary>
        public bool IsMulti => this.measureName.IsNotBlank();

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
        public WriteRecordsRequest Encode(TMessage message, TimestreamOptions options)
        {
            return this.Encode(message, options, this.time);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(IEnumerable<TMessage> messages, TimestreamOptions options)
        {
            return this.Encode(messages, options, this.time);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage message, TimestreamOptions options, IRecordUpdater<TMessage> timestamp)
        {
            return this.Encode(new[] { message }, options, timestamp);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(IEnumerable<TMessage> messages, TimestreamOptions options, IRecordUpdater<TMessage> timestamp)
        {
            var request = this.Encode(messages, timestamp);
            request.DatabaseName = options.Database;
            request.TableName = options.Table;

            return request;
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(TMessage message, IRecordUpdater<TMessage> timestamp)
        {
            return this.Encode(new[] { message }, timestamp);
        }

        /// <inheritdoc/>
        public WriteRecordsRequest Encode(IEnumerable<TMessage> messages, IRecordUpdater<TMessage> timestamp)
        {
            var records = new List<Record>();

            foreach (var message in messages)
            {
                var d = this.OnCreateDimensions(message);

                if (!d.Any())
                {
                    throw new InvalidOperationException($"Record is missing dimension values (they might all be null?) message:{message}");
                }

                var m = this.OnCreateMeasures(message);

                if (!m.Any())
                {
                    throw new InvalidOperationException($"Record is missing measure values (they might all be null?) message:{message}");
                }

                var record = new Record
                {
                    Dimensions = d,
                    Version = 1,
                };

                timestamp.Update(message, record);

                if (this.IsMulti)
                {
                    record.MeasureValues = m;
                    record.MeasureValueType = MeasureValueType.MULTI;
                }
                else
                {
                    var s = m.Single();
                    record.MeasureName = s.Name;
                    record.MeasureValue = s.Value;
                    record.MeasureValueType = s.Type;
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

        /// <inheritdoc/>
        public TMessage Decode(Row storage)
        {
            if (typeof(TMessage) == typeof(Row))
            {
                return (TMessage)(object)storage;
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<TMessage> Decode(QueryResponse storage)
        {
            foreach (var r in storage.Rows)
            {
                yield return this.Decode(r);
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
                var d = dimension.Value.Create(message);

                if (d is null)
                {
                    continue;
                }

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
                var m = measure.Value.Create(message);

                if (m is null)
                {
                    continue;
                }

                l.Add(m);
            }

            return l;
        }

        /// <summary>
        ///  Hook to implement custom logic that creates te common attributes
        /// </summary>
        /// <param name="records">all created records</param>
        /// <returns>Common Attributes record</returns>
        protected virtual Record? OnCreateCommonAttributes(IReadOnlyCollection<Record> records)
        {
            if (this.IsMulti)
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
