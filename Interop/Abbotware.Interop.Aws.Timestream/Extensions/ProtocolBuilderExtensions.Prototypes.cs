// -----------------------------------------------------------------------
// <copyright file="ProtocolBuilderExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Helpers;
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;

    /// <summary>
    /// Protocol Builder Extension Methods
    /// </summary>
    public static partial class ProtocolBuilderExtensions
    {
        public static IProtocolBuilder<TMessage> AddMeasureGeneric<TMessage, TProperty>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TProperty>> expression, Action<MeasureValueBuilderOptions<TMessage, TProperty>>? configure = null)
            where TMessage : notnull
            where TProperty : notnull
        {
            var (pi, f) = ExpressionHelper.GetPropertyExpression(expression);
            var propertyName = pi.Name;

            return builder.AddMeasureGeneric(propertyName, f, configure);
        }

        public static IProtocolBuilder<TMessage> AddMeasureGeneric<TMessage, TProperty>(this IProtocolBuilder<TMessage> builder, string name, Func<TMessage, TProperty> function, Action<MeasureValueBuilderOptions<TMessage, TProperty>>? configure = null)
            where TMessage : notnull
            where TProperty : notnull
        {
            Type type = typeof(TProperty);

            switch (true)
            {
                case true when typeof(byte).IsAssignableFrom(type):
                    ////var mvbo = new MeasureValueBuilderOptions<TMessage, byte>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
                    ////configure?.Invoke((MeasureValueBuilderOptions < TMessage, TProperty > )(object)mvbo);
                    ////return builder.AddMeasure<TMessage, TProperty>(expression, mvbo);
                    break;
            }

            return builder;
        }
    }
}