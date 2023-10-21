// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder.Dimension-Nullable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Linq.Expressions;
    using Abbotware.Core.Helpers;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Protocol Builder Extension Methods
    /// </summary>
    public static partial class ProtocolBuilderExtensions
    {
        /// <summary>
        /// Adds a Dimension Measure via an expression
        /// </summary>
        /// <remarks>null and empty strings will not be sent to timestream</remarks>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableDimension<TMessage, TProperty>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TProperty?>> expression, Action<NullableDimensionValueBuilderOptions<TMessage, TProperty?>>? configure = null)
            where TMessage : notnull
        {
            var (pi, f) = ExpressionHelper.GetPropertyExpression(expression);
            var propertyName = pi.Name;

            return builder.AddNullableDimension(propertyName, f, configure);
        }

        /// <summary>
        /// Adds a Dimension Measure
        /// </summary>
        /// <remarks>null and empty strings will not be sent to timestream</remarks>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableDimension<TMessage, TProperty>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, TProperty?> function, Action<NullableDimensionValueBuilderOptions<TMessage, TProperty?>>? configure = null)
            where TMessage : notnull
        {
            var options = new NullableDimensionValueBuilderOptions<TMessage, TProperty?>(name, DimensionValueType.VARCHAR, function, x => x?.ToString());
            configure?.Invoke(options);
            var target = options.Name ?? name;

            return builder.AddNullableDimension(options.Build());
        }
    }
}
