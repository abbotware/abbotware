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
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Timestream.Attributes;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Amazon;
    using Amazon.Runtime.Internal.Transform;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class TimestreamPublisher<TMessage> : BaseComponent<ITimestreamOptions>, IMessagePublisher<TMessage>
    {
        private static readonly IReadOnlyDictionary<Type, MeasureValueType> MeasureTypes = new Dictionary<Type, MeasureValueType>
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

        private static readonly IReadOnlyDictionary<Type, DimensionValueType> DimensionTypes = new Dictionary<Type, DimensionValueType>
        {
            {
                typeof(string), DimensionValueType.VARCHAR
            },
        };

        private readonly AmazonTimestreamWriteClient writeClient;

        private readonly IReadOnlyDictionary<string, (DimensionAttribute, PropertyInfo, DimensionValueType)> dimensions;

        private readonly IReadOnlyDictionary<string, (MeasureValueAttribute, PropertyInfo, MeasureValueType)> measures;

        private readonly (TimeAttribute, PropertyInfo)? time;

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
                    record.MeasureValueType = MeasureValueType.MULTI;
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
            var l = new List<Dimension>(this.dimensions.Count);

            foreach (var dimension in this.dimensions)
            {
                var o = dimension.Value.Item2.GetValue(message);

                var d = new Dimension
                {
                    Name = dimension.Key,
                    Value = o?.ToString(),
                    DimensionValueType = dimension.Value.Item3,
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
        protected virtual List<MeasureValue> OnMessageMeasures(TMessage message)
        {
            var l = new List<MeasureValue>(this.measures.Count);

            foreach (var measure in this.measures)
            {
                var o = measure.Value.Item2.GetValue(message);

                var mv = new MeasureValue
                {
                    Name = measure.Key,
                    Value = o?.ToString(),
                    Type = measure.Value.Item3,
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
        /// <param name="fallbackTime">fallback time if there is none present on the message</param>
        protected virtual void OnMessageTime(TMessage message, Record record, DateTimeOffset fallbackTime)
        {
            if (this.time is not null)
            {
                var o = this.time.Value.Item2.GetValue(message);
                record.Time = o?.ToString();
                record.TimeUnit = this.time.Value.Item1.TimeUnit;
            }
            else
            {
                var t = fallbackTime.ToUnixTimeMilliseconds();
                record.Time = t.ToString(CultureInfo.InvariantCulture);
                record.TimeUnit = TimeUnit.MILLISECONDS;
            }
        }
    }
}
