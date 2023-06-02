// -----------------------------------------------------------------------
// <copyright file="PocoTimestreamPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Aws.Timestream.Attributes;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class PocoTimestreamPublisher<TMessage> : BaseTimestreamPublisher<TMessage>
    {
        private readonly IReadOnlyDictionary<string, (DimensionAttribute, PropertyInfo, DimensionValueType)> dimensions;

        private readonly IReadOnlyDictionary<string, (MeasureValueAttribute, PropertyInfo, MeasureValueType)> measures;

        private readonly (TimeAttribute, PropertyInfo)? time;

        private readonly MeasureNameAttribute? measureNameAttribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoTimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="logger">injected logger</param>
        public PocoTimestreamPublisher(ITimestreamOptions options, ILogger logger)
            : base(options, logger)
        {
            var t = typeof(TMessage);
            var properties = ReflectionHelper.Properties<TMessage>();
            var ds = new Dictionary<string, (DimensionAttribute, PropertyInfo, DimensionValueType)>();
            var ms = new Dictionary<string, (MeasureValueAttribute, PropertyInfo, MeasureValueType)>();
            this.dimensions = ds;
            this.measures = ms;

            foreach (var p in properties)
            {
                var d = ReflectionHelper.SingleOrDefaultAttribute<DimensionAttribute>(p);
                var type = ReflectionHelper.GetPropertyDataType(p);

                if (d is not null)
                {
                    if (!DimensionTypes.TryGetValue(type, out var dvt))
                    {
                        throw new NotSupportedException($"dimension type:{type.FullName} not supported");
                    }

                    ds.Add(p.Name, (d, p, dvt));
                }

                var m = ReflectionHelper.SingleOrDefaultAttribute<MeasureValueAttribute>(p);

                if (m is not null)
                {
                    if (!MeasureTypes.TryGetValue(type, out var mvt))
                    {
                        throw new NotSupportedException($"measure value type:{type.FullName} not supported");
                    }

                    ms.Add(p.Name, (m, p, mvt));
                }

                var tm = ReflectionHelper.SingleOrDefaultAttribute<TimeAttribute>(p);

                if (tm is not null)
                {
                    if (this.time is not null)
                    {
                        throw new InvalidOperationException($"{t.FullName} has more than one Time Attribute");
                    }

                    this.time = (tm, p);
                }
            }

            if (!this.dimensions.Any())
            {
                throw new InvalidOperationException($"{t.FullName} has no Dimension Attributes");
            }

            if (!this.measures.Any())
            {
                throw new InvalidOperationException($"{t.FullName} has no MeasureValue Attributes");
            }

            if (this.measures.Count > 1)
            {
                this.measureNameAttribute = ReflectionHelper.SingleOrDefaultAttribute<MeasureNameAttribute>(t);

                if (this.measureNameAttribute is null)
                {
                    throw new InvalidOperationException($"{t.FullName} has multiple measures and is missing a MeasureName Attribute");
                }
            }
        }

        /// <inheritdoc/>
        protected override List<Dimension> OnCreateDimensions(TMessage message)
        {
            var l = new List<Dimension>(this.dimensions.Count);

            foreach (var dimension in this.dimensions)
            {
                var o = dimension.Value.Item2.GetValue(message);
                if (o is null)
                {
                    continue;
                }

                var d = new Dimension
                {
                    Name = dimension.Key,
                    Value = o.ToString(),
                    DimensionValueType = dimension.Value.Item3,
                };

                l.Add(d);
            }

            return l;
        }

        /// <inheritdoc/>
        protected override List<MeasureValue> OnCreateMeasures(TMessage message)
        {
            var l = new List<MeasureValue>(this.measures.Count);

            foreach (var measure in this.measures)
            {
                var o = measure.Value.Item2.GetValue(message);
                if (o is null)
                {
                    continue;
                }

                string value = o switch
                {
                    DateTimeOffset dt1 => GetTimeMeasureValue(dt1),
                    DateTime dt2 => GetTimeMeasureValue(new DateTimeOffset(dt2.ToUniversalTime())),
                    _ => o.ToString() ?? string.Empty,
                };

                var mv = new MeasureValue
                {
                    Name = measure.Key,
                    Value = value,
                    Type = measure.Value.Item3,
                };

                l.Add(mv);
            }

            return l;
        }

        /// <inheritdoc/>
        protected override void OnSetTime(TMessage message, Record record, DateTimeOffset fallbackTime)
        {
            var time = fallbackTime;
            var type = TimeUnitType.Milliseconds;

            if (this.time is not null)
            {
                var o = this.time.Value.Item2.GetValue(message);
                time = (o as DateTimeOffset?) ?? fallbackTime;
                type = this.time.Value.Item1.TimeUnit;
            }

            WriteRecordTime(record, time, type);
        }

        /// <inheritdoc/>
        protected override Record? OnCreateCommonAttributes()
        {
            if (this.measureNameAttribute is not null)
            {
                return new Record
                {
                    MeasureName = this.measureNameAttribute.Name,
                };
            }

            return null;
        }
    }
}