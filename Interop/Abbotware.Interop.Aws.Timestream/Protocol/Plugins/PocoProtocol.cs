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
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Aws.Timestream.Attributes;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;

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
        {
            var t = typeof(TMessage);
            var properties = ReflectionHelper.Properties<TMessage>();
            var ds = new Dictionary<string, DimensionValueOptions<TMessage>>();
            var ms = new Dictionary<string, MeasureValueOptions<TMessage>>();
            TimeValueOptions<TMessage>? time = null;
            MeasureNameAttribute? measureNameAttribute = null;

            foreach (var p in properties)
            {
                var da = ReflectionHelper.SingleOrDefaultAttribute<DimensionAttribute>(p);
                var type = ReflectionHelper.GetPropertyDataType(p);

                if (da is not null)
                {
                    if (!TimestreamTypes.DimensionTypes.TryGetValue(type, out var dvt))
                    {
                        throw new NotSupportedException($"dimension type:{type.FullName} not supported");
                    }

                    if (p.PropertyType != typeof(string))
                    {
                        throw new InvalidOperationException($"{t.FullName}.{p.Name} is not a string");
                    }

                    ds.Add(da.Name ?? p.Name, new(dvt, x => (string)p.GetValue(x)!, x => false));
                }

                var mva = ReflectionHelper.SingleOrDefaultAttribute<MeasureValueAttribute>(p);

                if (mva is not null)
                {
                    if (!TimestreamTypes.MeasureTypes.TryGetValue(type, out var mvt))
                    {
                        throw new NotSupportedException($"measure value type:{type.FullName} not supported");
                    }

                    var c = TypeDescriptorHelper.GetConverter(type);

                    if (type == typeof(DateTime))
                    {
                        ms.Add(mva.Name ?? p.Name, new(mvt, x => new DateTimeOffset((DateTime)p.GetValue(x)).ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture)));
                    }
                    else if (type == typeof(DateTimeOffset))
                    {
                        ms.Add(mva.Name ?? p.Name, new(mvt, x => ((DateTimeOffset)p.GetValue(x)).ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        ms.Add(mva.Name ?? p.Name, new(mvt, x => c.ConvertToString(p.GetValue(x))));
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

                    time = new(ta.TimeUnit, x => (DateTimeOffset)p.GetValue(x)!);
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
