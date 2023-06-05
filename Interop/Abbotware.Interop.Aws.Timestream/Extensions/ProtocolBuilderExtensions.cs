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
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Protocol Builder Extension Methods
    /// </summary>
    public static class ProtocolBuilderExtensions
    {
        /// <summary>
        /// Adds a VARCHAR Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, string>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.VARCHAR, x => x);
        }

        /// <summary>
        /// Adds a BIGINT (from long) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, long>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from short) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, short>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from int) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, int>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from sbyte) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, sbyte>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from ulong) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ulong>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from ushort) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ushort>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from uint) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, uint>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BIGINT (from byte) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, byte>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a BOOLEAN Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, bool>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.BOOLEAN, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a DOUBLE Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, double>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.DOUBLE, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a DOUBLE (from float) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, float>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.DOUBLE, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a DOUBLE (from decimal)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, decimal>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.DOUBLE, x => x.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds a TIMESTAMP (from datetime)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTime>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.TIMESTAMP, x => BuildTimestampFromDateTimeOffset(x));
        }

        /// <summary>
        /// Adds a TIMESTAMP (from datetimeoffset)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTimeOffset>> expression)
        {
            return builder.AddMeasure(expression, MeasureValueType.TIMESTAMP, x => BuildTimestampFromDateTimeOffset(x));
        }

        /// <summary>
        /// Adds an optional BIGINT (from long?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, long?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from short?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, short?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from int?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, int?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from sbyte?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, sbyte?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from ulong?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ulong?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from ushort?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ushort?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from uint?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, uint?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BIGINT (from byte?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, byte?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional BOOLEAN (from bool?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, bool?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.BOOLEAN, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional DOUBLE (from double?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, double?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.DOUBLE, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional DOUBLE (from float?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, float?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.DOUBLE, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional DOUBLE (from decimal?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, decimal?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.DOUBLE, x => x?.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Adds an optional TIMESTAMP (from datetime?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTime?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.TIMESTAMP, BuildTimestampFromNullableDateTime);
        }

        /// <summary>
        /// Adds an optional TIMESTAMP (from datetimeoffset?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">propety accessor expression</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTimeOffset?>> expression)
        {
            return builder.AddNullableMeasure(expression, MeasureValueType.TIMESTAMP, BuildTimestampFromNullableDateTimeOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? BuildTimestampFromNullableDateTime(DateTime? dateTime)
        {
            if (dateTime is null)
            {
                return null;
            }

            return BuildTimestampFromDateTime(dateTime.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string BuildTimestampFromDateTime(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                throw new InvalidOperationException("Unable to convert to UTC");
            }

            return BuildTimestampFromDateTimeOffset(new(dateTime));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? BuildTimestampFromNullableDateTimeOffset(DateTimeOffset? dateTimeOffset)
        {
            if (dateTimeOffset is null)
            {
                return null;
            }

            return BuildTimestampFromDateTimeOffset(dateTimeOffset.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string BuildTimestampFromDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);
        }
    }
}