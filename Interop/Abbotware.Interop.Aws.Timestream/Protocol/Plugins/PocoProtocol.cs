// -----------------------------------------------------------------------
// <copyright file="PocoProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Helpers;
    using Abbotware.Interop.Aws.Timestream.Attributes;
    using Abbotware.Interop.Aws.Timestream.Protocol.Internal;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamWrite.Model;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Helper class to build a Poco / Attribute based protocol
    /// </summary>
    public static class PocoProtocol
    {
        /// <summary>
        /// Builds a Poco / Attribute based protocol
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="logger">injected logger</param>
        /// <returns>configured protocl</returns>
        /// <exception cref="NotSupportedException">unsupported</exception>
        /// <exception cref="InvalidOperationException">wrong attributes</exception>
        public static ITimestreamProtocol<TMessage> Build<TMessage>(ILogger logger)
            where TMessage : notnull
        {
            var t = typeof(TMessage);
            var properties = ReflectionHelper.Properties<TMessage>();
            var ds = new Dictionary<string, IMessagePropertyFactory<TMessage, Dimension>>();
            var ms = new Dictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>>();
            var nullContext = new NullabilityInfoContext();
            IRecordUpdater<TMessage>? time = null;
            MeasureNameAttribute? measureNameAttribute = null;

            foreach (var p in properties)
            {
                var da = ReflectionHelper.SingleOrDefaultAttribute<DimensionAttribute>(p);
                var type = ReflectionHelper.GetPropertyDataType(p);
                var sourceName = p.Name;

                if (da is not null)
                {
                    var targetName = da.Name ?? sourceName;
                    var nullability = nullContext.Create(p);

                    if (!TimestreamTypes.DimensionTypes.TryGetValue(type, out var dvt))
                    {
                        throw new NotSupportedException($"dimension type:{type.FullName} not supported");
                    }

                    if (p.PropertyType != typeof(string))
                    {
                        throw new InvalidOperationException($"{t.FullName}.{p.Name} is not a string");
                    }

                    if (nullability.WriteState is not NullabilityState.Nullable)
                    {
                        ds.Add(targetName, new DimensionValueOptions<TMessage, string>(dvt, x => (string)p.GetValue(x)!, x => x, sourceName, targetName));
                    }
                    else
                    {
                        ds.Add(targetName, new NullableDimensionValueOptions<TMessage, string>(dvt, x => (string)p.GetValue(x)!, x => x, sourceName, targetName));
                    }
                }

                var mva = ReflectionHelper.SingleOrDefaultAttribute<MeasureAttribute>(p);

                if (mva is not null)
                {
                    var targetName = mva.Name ?? sourceName;

                    if (!TimestreamTypes.MeasureTypes.TryGetValue(type, out var mvt))
                    {
                        throw new NotSupportedException($"measure value type:{type.FullName} not supported");
                    }

                    var c = TypeDescriptorHelper.GetConverter(type);

                    if (type == typeof(DateOnly))
                    {
                        ms.Add(targetName, new MeasureValueOptions<TMessage, DateOnly>(
                            mvt,
                            x =>
                            {
                                var v = p.GetValue(x);

                                if (v is null)
                                {
                                    throw new InvalidOperationException($"unexpected null for:{sourceName}");
                                }

                                return (DateOnly)v;
                            },
                            x => new DateTimeOffset(x.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)).ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture),
                            sourceName,
                            targetName));
                    }
                    else if (type == typeof(DateTime))
                    {
                        ms.Add(targetName, new MeasureValueOptions<TMessage, DateTime>(
                            mvt,
                            x =>
                            {
                                var v = p.GetValue(x);

                                if (v is null)
                                {
                                    throw new InvalidOperationException($"unexpected null for:{sourceName}");
                                }

                                return (DateTime)v;
                            },
                            x => new DateTimeOffset(x).ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture),
                            sourceName,
                            targetName));
                    }
                    else if (type == typeof(DateTimeOffset))
                    {
                        ms.Add(targetName, new MeasureValueOptions<TMessage, DateTimeOffset>(
                            mvt,
                            x =>
                            {
                                var v = p.GetValue(x);
                                if (v is null)
                                {
                                    throw new InvalidOperationException($"unexpected null for:{sourceName}");
                                }

                                return (DateTimeOffset)v;
                            },
                            x => x.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture),
                            sourceName,
                            targetName));
                    }
                    else
                    {
                        ms.Add(targetName, new MeasureValueOptions<TMessage, string?>(
                        mvt,
                        x =>
                            {
                                var v = p.GetValue(x);
                                if (v is null)
                                {
                                    return null;
                                }

                                return c.ConvertToString(v);
                            },
                        x => x,
                        sourceName,
                        targetName));
                    }
                }

                var ta = ReflectionHelper.SingleOrDefaultAttribute<TimeAttribute>(p);

                if (ta is not null)
                {
                    if (time is not null)
                    {
                        throw new InvalidOperationException($"{t.FullName} has more than one Time Attribute");
                    }

                    if (p.PropertyType != typeof(DateTimeOffset))
                    {
                        throw new InvalidOperationException($"{t.FullName}.{p.Name} is not a datetimeoffset");
                    }

                    time = new TimeValueOptions<TMessage, DateTimeOffset>(ta.TimeUnit, x => (DateTimeOffset)p.GetValue(x)!, x => x, sourceName);
                }
            }

            if (!ds.Any())
            {
                throw new InvalidOperationException($"{t.FullName} has no Dimension Attributes");
            }

            if (!ms.Any())
            {
                throw new InvalidOperationException($"{t.FullName} has no MeasureValue Attributes");
            }

            if (ms.Count > 1)
            {
                measureNameAttribute = ReflectionHelper.SingleOrDefaultAttribute<MeasureNameAttribute>(t);

                if (measureNameAttribute is null)
                {
                    throw new InvalidOperationException($"{t.FullName} has multiple measures and is missing a MeasureName Attribute");
                }

                if (time is null)
                {
                    return new TimestreamProtocol<TMessage>(ds, ms, measureNameAttribute.Name, logger);
                }

                return new TimestreamProtocol<TMessage>(ds, ms, time, measureNameAttribute.Name, logger);
            }

            if (time is null)
            {
                return new TimestreamProtocol<TMessage>(ds, ms, logger);
            }

            return new TimestreamProtocol<TMessage>(ds, ms, time, logger);
        }
    }
}
