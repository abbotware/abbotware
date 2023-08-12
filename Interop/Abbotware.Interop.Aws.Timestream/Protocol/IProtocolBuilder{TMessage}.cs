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
        /// <param name="name">property name</param>
        /// <param name="function">property function</param>
        /// <param name="options">builder options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddDimension<TProperty>(string name, Func<TMessage, TProperty> function, DimensionValueBuilderOptions<TMessage, TProperty> options)
            where TProperty : notnull;

        /// <summary>
        /// Adds a Dimension
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="options">builder options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddDimension<TProperty>(Expression<Func<TMessage, TProperty>> expression, DimensionValueBuilderOptions<TMessage, TProperty> options)
            where TProperty : notnull;

        /// <summary>
        /// Adds a Nullable Dimension
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="options">builder options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableDimension<TProperty>(Expression<Func<TMessage, TProperty?>> expression, NullableDimensionValueBuilderOptions<TMessage, TProperty?> options);

        /// <summary>
        /// Adds a Nullable Dimension
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="name">property name</param>
        /// <param name="function">property function</param>
        /// <param name="options">builder options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableDimension<TProperty>(string name, Func<TMessage, TProperty> function, NullableDimensionValueBuilderOptions<TMessage, TProperty?> options)
            where TProperty : notnull;

        /// <summary>
        /// Adds a Measure
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="options">builder options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddMeasure<TProperty>(Expression<Func<TMessage, TProperty>> expression, MeasureValueBuilderOptions<TMessage, TProperty> options)
            where TProperty : notnull;

        /// <summary>
        /// Adds a Nullable Measure
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="options">builder options</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableMeasure<TProperty>(Expression<Func<TMessage, TProperty?>> expression, NullableMeasureValueBuilderOptions<TMessage, TProperty?> options);

        /// <summary>
        /// Adds Time
        /// </summary>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="timeUnitType">time unit type</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddTime(Expression<Func<TMessage, DateTimeOffset>> expression, TimeUnitType timeUnitType);

        /// <summary>
        /// Adds Time
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="timeUnitType">time unit type</param>
        /// <param name="converter">conversion function</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddTime<TProperty>(Expression<Func<TMessage, TProperty>> expression, TimeUnitType timeUnitType, Func<TProperty, DateTimeOffset> converter)
            where TProperty : notnull;
    }
}
