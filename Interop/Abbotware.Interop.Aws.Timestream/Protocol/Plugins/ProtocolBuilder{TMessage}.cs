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
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;
    using Abbotware.Interop.Aws.Timestream.Protocol.Internal;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.Runtime.Internal.Transform;
    using Amazon.TimestreamWrite.Model;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// Protocol Builder
    /// </summary>
    /// <typeparam name="TMessage">message types</typeparam>
    public class ProtocolBuilder<TMessage> : IProtocolBuilder<TMessage>
        where TMessage : notnull
    {
        private readonly Type type = typeof(TMessage);

        private readonly HashSet<string> fields = new();

        private readonly Dictionary<string, IMessagePropertyFactory<TMessage, Dimension>> dimensions = new();

        private readonly Dictionary<string, IMessagePropertyFactory<TMessage, MeasureValue>> measures = new();

        private IRecordUpdater<TMessage>? time;

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
        public IProtocolBuilder<TMessage> AddDimension<TProperty>(DimensionValueOptions<TMessage, TProperty> options)
          where TProperty : notnull => this.OnAddDimension<TMessage>(options);

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddNullableDimension<TProperty>(NullableDimensionValueOptions<TMessage, TProperty?> options)
            => this.OnAddDimension<TMessage>(options);

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddMeasure<TProperty>(MeasureValueOptions<TMessage, TProperty> options)
            where TProperty : notnull
        {
            return this.OnAddMeasure(options);
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddNullableMeasure<TProperty>(MeasureValueOptions<TMessage, TProperty?> options)
        {
            return this.OnAddMeasure(options);
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddTime(Expression<Func<TMessage, DateTimeOffset>> expression, TimeUnitType timeUnitType)
        {
            return this.AddNullableTime(expression, timeUnitType, x => x);
        }

        /// <inheritdoc/>
        public IProtocolBuilder<TMessage> AddNullableTime<TProperty>(Expression<Func<TMessage, TProperty>> expression, TimeUnitType timeUnitType, Func<TProperty, DateTimeOffset> converter)
        {
            if (this.time is not null)
            {
                throw new ArgumentException($"{this.type.FullName} has more than one Time value");
            }

            var (pi, compiled) = this.GetProperty(expression, false);
            var source = pi.Name;

            this.time = new TimeValueOptions<TMessage, TProperty>(timeUnitType, compiled, converter, source);

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
            if (this.MeasureName.IsNotBlank())
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

        private IProtocolBuilder<TMessage> OnAddDimension<TProperty>(IMessagePropertyFactory<TMessage, Dimension> factory)
        {
            if (!this.fields.Add(factory.TargetName))
            {
                throw new ArgumentException($"{factory.TargetName} already added");
            }

            this.dimensions.Add(factory.TargetName, factory);

            return this;
        }

        private IProtocolBuilder<TMessage> OnAddMeasure<TProperty>(MeasureValueOptions<TMessage, TProperty> options)
        {
            if (this.measures.Any())
            {
                if (this.MeasureName.IsBlank())
                {
                    throw new ArgumentException($"{this.type.FullName} has multiple measures - need to use ProtocolBuilder(string meaureName ... ) ");
                }
            }

            this.measures.Add(options.TargetName, options);

            return this;
        }

        private (PropertyInfo PropertyInfo, Func<TMessage, TField> Compiled) GetProperty<TField>(Expression<Func<TMessage, TField>> expression, bool requireUnique)
        {
            var (pi, exp) = ExpressionHelper.GetPropertyExpression(expression);

            if (requireUnique)
            {
                if (!this.fields.Add(pi.Name))
                {
                    throw new ArgumentException($"{pi.Name} already added");
                }
            }

            return (pi, exp);
        }
    }
}
