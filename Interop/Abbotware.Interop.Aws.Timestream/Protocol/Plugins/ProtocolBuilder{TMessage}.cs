// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Logging.Plugins;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.Auth.AccessControlPolicy;

    /// <summary>
    /// Protocol Builder
    /// </summary>
    /// <typeparam name="TMessage">message types</typeparam>
    public class ProtocolBuilder<TMessage> : IProtocolBuilder<TMessage>
    {
        private readonly Type type = typeof(TMessage);

        private readonly HashSet<string> fields = new();

        private readonly Dictionary<string, DimensionValueOptions<TMessage>> dimensions = new();

        private readonly Dictionary<string, MeasureValueOptions<TMessage>> measures = new();

        private TimeValueOptions<TMessage>? time;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolBuilder{TMessage}"/> class.
        /// </summary>
        public ProtocolBuilder()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolBuilder{TMessage}"/> class.
        /// </summary>
        /// <param name="measureName">measure name for Multi-Measure</param>
        public ProtocolBuilder(string measureName)
        {
            this.MeasureName = measureName;
        }

        /// <summary>
        /// Gets the Measure Name
        /// </summary>
        private string MeasureName { get; }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddDimension<TProperty>(Expression<Func<TMessage, TProperty>> expression, DimensionValueBuilderOptions<TMessage, TProperty> options)
        {
            var (pi, compiled) = this.GetProperty(expression);
            var source = pi.Name;
            var target = options.Name ?? source;

            this.dimensions.Add(target, new(options.ValueType, x => options.Converter(compiled(x)), x => false, source, target));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddNullableDimension<TProperty>(Expression<Func<TMessage, TProperty?>> expression, NullableDimensionValueBuilderOptions<TMessage, TProperty?> options)
        {
            var (pi, compiled) = this.GetProperty(expression);
            var source = pi.Name;
            var target = options.Name ?? source;

            this.dimensions.Add(target, new(options.ValueType, x => options.Converter(compiled(x)) ?? string.Empty, x => compiled(x) == null, source, target));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddMeasure<TProperty>(Expression<Func<TMessage, TProperty>> expression, MeasureValueBuilderOptions<TMessage, TProperty> options)
        {
            if (this.measures.Any())
            {
                if (this.MeasureName.IsBlank())
                {
                    throw new ArgumentException($"{this.type.FullName} has multiple measures - need to use ProtocolBuilder(string meaureName ... ) ");
                }
            }

            var (pi, compiled) = this.GetProperty(expression);
            var source = pi.Name;
            var target = options.Name ?? source;

            this.measures.Add(target, new(options.ValueType, x => options.Converter(compiled(x)), source, target));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddNullableMeasure<TProperty>(Expression<Func<TMessage, TProperty?>> expression, NullableMeasureValueBuilderOptions<TMessage, TProperty?> options)
        {
            if (this.measures.Any())
            {
                if (this.MeasureName.IsBlank())
                {
                    throw new ArgumentException($"{this.type.FullName} has multiple measures - need to use ProtocolBuilder(string meaureName ... ) ");
                }
            }

            var (pi, compiled) = this.GetProperty(expression);
            var source = pi.Name;
            var target = options.Name ?? source;

            this.measures.Add(target, new(options.ValueType, x => options.Converter(compiled(x)), source, target));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddTime(Expression<Func<TMessage, DateTimeOffset>> expression, TimeUnitType timeUnitType)
        {
            return this.AddTime(expression, timeUnitType, x => x);
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddTime<TProperty>(Expression<Func<TMessage, TProperty>> expression, TimeUnitType timeUnitType, Func<TProperty, DateTimeOffset> converter)
        {
            if (this.time is not null)
            {
                throw new ArgumentException($"{this.type.FullName} has more than one Time value");
            }

            var (pi, compiled) = this.GetProperty(expression);
            var source = pi.Name;

            this.time = new(timeUnitType, x => converter(compiled(x)), source);

            return this;
        }

        /// <summary>
        /// builds the protocol
        /// </summary>
        /// <returns>configured protocol</returns>
        public ITimestreamProtocol<TMessage> Build()
        {
            return this.Build(NullLogger.Instance);
        }

        /// <summary>
        /// builds the protocol
        /// </summary>
        /// <param name="logger">injected logger</param>
        /// <returns>configured protocol</returns>
        public ITimestreamProtocol<TMessage> Build(ILogger logger)
        {
            if (this.measures.Count > 1)
            {
                if (this.time is not null)
                {
                    return new TimestreamProtocol<TMessage>(this.dimensions, this.measures, this.time, this.MeasureName, logger);
                }

                return new TimestreamProtocol<TMessage>(this.dimensions, this.measures, this.MeasureName, logger);
            }
            else
            {
                if (this.time is not null)
                {
                    return new TimestreamProtocol<TMessage>(this.dimensions, this.measures, this.time, logger);
                }

                return new TimestreamProtocol<TMessage>(this.dimensions, this.measures, logger);
            }
        }

        private (PropertyInfo PropertyInfo, Func<TMessage, TField> Compiled) GetProperty<TField>(Expression<Func<TMessage, TField>> expression)
        {
            var (pi, exp) = ExpressionHelper.GetPropertyExpression(expression);

            if (!this.fields.Add(pi.Name))
            {
                throw new ArgumentException($"{pi.Name} already added");
            }

            return (pi, exp);
        }
    }
}
