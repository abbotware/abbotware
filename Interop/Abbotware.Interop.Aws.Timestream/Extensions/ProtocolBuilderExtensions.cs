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
        private static IProtocolBuilder<TMessage> OnAddExpression<TMessage, TProperty, TOptions>(IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TProperty>> expression, Action<TOptions>? configure, Func<IProtocolBuilder<TMessage>, string, Func<TMessage, TProperty>, Action<TOptions>?, IProtocolBuilder<TMessage>> callback)
            where TMessage : notnull
            where TProperty : notnull
            where TOptions : MeasureValueBuilderOptions<TMessage, TProperty>
        {
            var (pi, f) = ExpressionHelper.GetPropertyExpression(expression);
            var propertyName = pi.Name;
            return callback(builder, propertyName, f, configure);
        }

        private static IProtocolBuilder<TMessage> OnAddMeasure<TMessage, TProperty, TOptions>(IProtocolBuilder<TMessage> builder, Func<TMessage, TProperty> f, TOptions options, Action<TOptions>? configure)
            where TMessage : notnull
            where TProperty : notnull
            where TOptions : MeasureValueBuilderOptions<TMessage, TProperty>
        {
            configure?.Invoke(options);

            return builder.AddMeasure(options.Build());
        }

        private static IProtocolBuilder<TMessage> OnAddNullableExpression<TMessage, TProperty, TOptions>(IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TProperty?>> expression, Action<TOptions>? configure, Func<IProtocolBuilder<TMessage>, string, Func<TMessage, TProperty?>, Action<TOptions>?, IProtocolBuilder<TMessage>> callback)
            where TMessage : notnull
            where TOptions : NullableMeasureValueBuilderOptions<TMessage, TProperty?>
        {
            var (pi, f) = ExpressionHelper.GetPropertyExpression(expression);
            var propertyName = pi.Name;
            return callback(builder, propertyName, f, configure);
        }

        private static IProtocolBuilder<TMessage> OnAddNullableMeasure<TMessage, TProperty, TOptions>(IProtocolBuilder<TMessage> builder, Func<TMessage, TProperty> f, TOptions options, Action<TOptions>? configure)
            where TMessage : notnull
            where TOptions : NullableMeasureValueBuilderOptions<TMessage, TProperty>
        {
            configure?.Invoke(options);

            return builder.AddNullableMeasure(options.Build());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? BuildTimestampFrom(DateOnly? date)
        {
            if (date is null)
            {
                return null;
            }

            return BuildTimestampFrom(date.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? BuildTimestampFrom(DateTime? dateTime)
        {
            if (dateTime is null)
            {
                return null;
            }

            return BuildTimestampFrom(dateTime.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string BuildTimestampFrom(DateOnly date)
        {
            return BuildTimestampFrom(date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string BuildTimestampFrom(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                throw new InvalidOperationException("Unable to convert to UTC");
            }

            return BuildTimestampFrom((DateTimeOffset)new(dateTime));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? BuildTimestampFrom(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset is null)
            {
                return null;
            }

            return BuildTimestampFrom(dateTimeOffset.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string BuildTimestampFrom(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);
        }
    }
}