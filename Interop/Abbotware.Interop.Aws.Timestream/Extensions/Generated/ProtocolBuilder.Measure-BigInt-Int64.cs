// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder.Measure-BigInt-Int64.cs" company="Abbotware, LLC">
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
        /// Adds a BIGINT (from long) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, long>> expression, Action<MeasureValueBuilderOptions<TMessage, long>>? configure = null)
            where TMessage : notnull => OnAddExpression(builder, expression, configure, AddMeasure);

        /// <summary>
        /// Adds a BIGINT (from long) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, long> function, Action<MeasureValueBuilderOptions<TMessage, long>>? configure = null)
            where TMessage : notnull
        {
            var options = new MeasureValueBuilderOptions<TMessage, long>(name, MeasureValueType.BIGINT, function, x => x.ToString(CultureInfo.InvariantCulture));
            return OnAddMeasure(builder, function, options, configure);
        }

        /// <summary>
        /// Adds an optional BIGINT (from long?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, long?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, long?>>? configure = null)
            where TMessage : notnull => OnAddNullableExpression(builder, expression, configure, AddMeasure);

        /// <summary>
        /// Adds an optional BIGINT (from long?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, long?> function, Action<NullableMeasureValueBuilderOptions<TMessage, long?>>? configure = null)
                 where TMessage : notnull
        {
            var options = new NullableMeasureValueBuilderOptions<TMessage, long?>(name, MeasureValueType.BIGINT, function, x => x?.ToString(CultureInfo.InvariantCulture));
            return OnAddNullableMeasure(builder, function, options, configure);
        }
    }
}
