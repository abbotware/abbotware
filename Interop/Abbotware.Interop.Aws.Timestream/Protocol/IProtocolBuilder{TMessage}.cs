// -----------------------------------------------------------------------
// <copyright file="IProtocolBuilder{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Linq.Expressions;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;

    /// <summary>
    /// protocol builder
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IProtocolBuilder<TMessage>
        where TMessage : notnull
    {
        /// <summary>
        /// Adds a Dimension
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="options">Dimension Value Options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddDimension<TProperty>(DimensionValueOptions<TMessage, TProperty> options)
            where TProperty : notnull;

        /// <summary>
        /// Adds a Nullable Dimension
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="options">Dimension Value Options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableDimension<TProperty>(NullableDimensionValueOptions<TMessage, TProperty?> options);

        /// <summary>
        /// Adds a Measure
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="options">Measure Value Options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddMeasure<TProperty>(MeasureValueOptions<TMessage, TProperty> options)
            where TProperty : notnull;

        /// <summary>
        /// Adds a Nullable Measure
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="options">Measure Value Options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableMeasure<TProperty>(MeasureValueOptions<TMessage, TProperty?> options);

        /// <summary>
        /// Adds Time
        /// </summary>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="timeUnitType">time unit type</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddTime(Expression<Func<TMessage, DateTimeOffset>> expression, TimeUnitType timeUnitType);

        /// <summary>
        /// Adds a Time value that can be nullable - must supply default
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="timeUnitType">time unit type</param>
        /// <param name="converter">conversion function</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableTime<TProperty>(Expression<Func<TMessage, TProperty>> expression, TimeUnitType timeUnitType, Func<TProperty, DateTimeOffset> converter);
    }
}
