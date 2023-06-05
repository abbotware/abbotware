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
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamWrite;

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
        public IProtocolBuilder<TMessage> AddDimension(Expression<Func<TMessage, string>> expression)
        {
            return this.AddDimension(expression, DimensionValueType.VARCHAR, x => x);
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddDimension<TField>(Expression<Func<TMessage, TField>> expression, DimensionValueType type, Func<TField, string> converter)
        {
            var (pi, compiled) = this.GetProperty(expression);

            this.dimensions.Add(pi.Name, new(type, x => converter(compiled(x))));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddMeasure<TField>(Expression<Func<TMessage, TField>> expression, MeasureValueType type, Func<TField, string> converter)
        {
            if (this.measures.Any())
            {
                if (this.MeasureName.IsBlank())
                {
                    throw new ArgumentException($"{this.type.FullName} has multiple measures - need to use ProtocolBuilder(string meaureName ... ) ");
                }
            }

            var (pi, compiled) = this.GetProperty(expression);

            this.measures.Add(pi.Name, new(type, x => converter(compiled(x))));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddNullableMeasure<TField>(Expression<Func<TMessage, TField?>> expression, MeasureValueType type, Func<TField?, string?> converter)
        {
            if (this.measures.Any())
            {
                if (this.MeasureName.IsBlank())
                {
                    throw new ArgumentException($"{this.type.FullName} has multiple measures - need to use ProtocolBuilder(string meaureName ... ) ");
                }
            }

            var (pi, compiled) = this.GetProperty(expression);

            this.measures.Add(pi.Name, new(type, x => converter(compiled(x))));

            return this;
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddTime(Expression<Func<TMessage, DateTimeOffset>> expression, TimeUnitType timeUnitType)
        {
            if (this.time is not null)
            {
                throw new ArgumentException($"{this.type.FullName} has more than one Time value");
            }

            var (_, compiled) = this.GetProperty(expression);

            this.time = new(timeUnitType, compiled);

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
