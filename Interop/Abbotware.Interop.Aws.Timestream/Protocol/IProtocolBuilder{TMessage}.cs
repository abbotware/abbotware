// -----------------------------------------------------------------------
// <copyright file="IProtocolBuilder{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Linq.Expressions;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// protocol builder
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IProtocolBuilder<TMessage>
    {
        /// <summary>
        /// Adds a VARCHAR Dimension
        /// </summary>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddDimension(Expression<Func<TMessage, string>> expression);

        /// <summary>
        /// Adds a Dimension
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="type">dimension type</param>
        /// <param name="converter">conversion function</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddDimension<TProperty>(Expression<Func<TMessage, TProperty>> expression, DimensionValueType type, Func<TProperty, string> converter);

        /// <summary>
        /// Adds a Measure
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="type">measure type</param>
        /// <param name="converter">conversion function</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddMeasure<TProperty>(Expression<Func<TMessage, TProperty>> expression, MeasureValueType type, Func<TProperty, string> converter);

        /// <summary>
        /// Adds a Nullable Measure
        /// </summary>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="type">measure type</param>
        /// <param name="converter">conversion function</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddNullableMeasure<TProperty>(Expression<Func<TMessage, TProperty?>> expression, MeasureValueType type, Func<TProperty?, string?> converter);

        /// <summary>
        /// Adds Time
        /// </summary>
        /// <param name="expression">propety accessor expression</param>
        /// <param name="timeUnitType">time unit type</param>
        /// <returns>builder</returns>
        public IProtocolBuilder<TMessage> AddTime(Expression<Func<TMessage, DateTimeOffset>> expression, TimeUnitType timeUnitType);
    }
}
