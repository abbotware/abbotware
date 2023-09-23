// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder.Measure-Timestamp-DateTimeOffset.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Protocol Builder Extension Methods
    /// </summary>
    public static partial class ProtocolBuilderExtensions
    {
        /// <summary>
        /// Adds a TIMESTAMP (from DateTimeOffset) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTimeOffset>> expression, Action<MeasureValueBuilderOptions<TMessage, DateTimeOffset>>? configure = null)
            where TMessage : notnull => OnAddExpression(builder, expression, configure, AddMeasure);

        /// <summary>
        /// Adds a TIMESTAMP (from DateTimeOffset) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, DateTimeOffset> function, Action<MeasureValueBuilderOptions<TMessage, DateTimeOffset>>? configure = null)
            where TMessage : notnull
        {
            var options = new MeasureValueBuilderOptions<TMessage, DateTimeOffset>(name, MeasureValueType.TIMESTAMP, function, BuildTimestampFrom);
            return OnAddMeasure(builder, function, options, configure);
        }

        /// <summary>
        /// Adds an optional TIMESTAMP (from DateTimeOffset?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTimeOffset?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, DateTimeOffset?>>? configure = null)
            where TMessage : notnull => OnAddNullableExpression(builder, expression, configure, AddNullableMeasure);

        /// <summary>
        /// Adds an optional TIMESTAMP (from DateTimeOffset?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, DateTimeOffset?> function, Action<NullableMeasureValueBuilderOptions<TMessage, DateTimeOffset?>>? configure = null)
                 where TMessage : notnull
        {
            var options = new NullableMeasureValueBuilderOptions<TMessage, DateTimeOffset?>(name, MeasureValueType.TIMESTAMP, function, BuildTimestampFrom);
            return OnAddNullableMeasure(builder, function, options, configure);
        }
    }
}
