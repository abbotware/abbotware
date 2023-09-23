// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder.Measure-BigInt-Enum.cs" company="Abbotware, LLC">
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
        /// Adds a BIGINT (from Enum) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage, TEnum>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TEnum>> expression, Action<MeasureValueBuilderOptions<TMessage, TEnum>>? configure = null)
            where TEnum : Enum
            where TMessage : notnull => OnAddExpression(builder, expression, configure, AddMeasure<TMessage, TEnum>);

        /// <summary>
        /// Adds a BIGINT (from Enum) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage, TEnum>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, TEnum> function, Action<MeasureValueBuilderOptions<TMessage, TEnum>>? configure = null)
            where TMessage : notnull
            where TEnum : Enum
        {
            var options = new MeasureValueBuilderOptions<TMessage, TEnum>(name, MeasureValueType.BIGINT, function, x => ((long)(object)x).ToString(CultureInfo.InvariantCulture));
            return OnAddMeasure(builder, function, options, configure);
        }
    }
}
