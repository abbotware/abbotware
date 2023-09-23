// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder.Measure-BigInt-UInt64.cs" company="Abbotware, LLC">
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
        /// Adds a BIGINT (from ulong) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ulong>> expression, Action<MeasureValueBuilderOptions<TMessage, ulong>>? configure = null)
            where TMessage : notnull => OnAddExpression(builder, expression, configure, AddMeasure);

        /// <summary>
        /// Adds a BIGINT (from ulong) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, ulong> function, Action<MeasureValueBuilderOptions<TMessage, ulong>>? configure = null)
            where TMessage : notnull
        {
            var options = new MeasureValueBuilderOptions<TMessage, ulong>(name, MeasureValueType.BIGINT, function, x => x.ToString(CultureInfo.InvariantCulture));
            return OnAddMeasure(builder, function, options, configure);
        }

        /// <summary>
        /// Adds an optional BIGINT (from ulong?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ulong?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, ulong?>>? configure = null)
            where TMessage : notnull => OnAddNullableExpression(builder, expression, configure, AddNullableMeasure);

        /// <summary>
        /// Adds an optional BIGINT (from ulong?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, ulong?> function, Action<NullableMeasureValueBuilderOptions<TMessage, ulong?>>? configure = null)
                 where TMessage : notnull
        {
            var options = new NullableMeasureValueBuilderOptions<TMessage, ulong?>(name, MeasureValueType.BIGINT, function, x => x?.ToString(CultureInfo.InvariantCulture));
            return OnAddNullableMeasure(builder, function, options, configure);
        }
    }
}
