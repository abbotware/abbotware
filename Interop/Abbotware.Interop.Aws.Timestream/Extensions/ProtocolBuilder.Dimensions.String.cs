// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilder.Dimensions.String.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Linq.Expressions;
    using Abbotware.Core.Helpers;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;

    /// <summary>
    /// Protocol Builder Extension Methods
    /// </summary>
    public static partial class ProtocolBuilderExtensions
    {
        /// <summary>
        /// Adds a String Dimension Measure via an expression
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddDimension<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, string>> expression, Action<DimensionValueBuilderOptions<TMessage, string>>? configure = null)
            where TMessage : notnull
        {
            var (pi, f) = ExpressionHelper.GetPropertyExpression(expression);
            var propertyName = pi.Name;

            return builder.AddDimension(propertyName, f, configure);
        }

        /// <summary>
        /// Adds a String Dimension Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="name">column name</param>
        /// <param name="function">property function</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddDimension<TMessage>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, string> function, Action<DimensionValueBuilderOptions<TMessage, string>>? configure = null)
            where TMessage : notnull
        {
            return builder.AddDimension(name, function, o => { o.Converter = x => x; });
        }
    }
}
