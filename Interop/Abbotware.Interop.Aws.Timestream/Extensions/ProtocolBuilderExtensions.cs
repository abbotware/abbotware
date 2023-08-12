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
    using Abbotware.Interop.Aws.Timestream.Protocol.Builder;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Protocol Builder Extension Methods
    /// </summary>
    public static class ProtocolBuilderExtensions
    {
        /// <summary>
        /// Adds a Dimension Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddDimension<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, string>> expression, Action<DimensionValueBuilderOptions<TMessage, string>>? configure = null)
            where TMessage : notnull
        {
            var o = new DimensionValueBuilderOptions<TMessage, string>(DimensionValueType.VARCHAR, x => x);
            configure?.Invoke(o);

            return builder.AddDimension(expression, o);
        }

        /// <summary>
        /// Adds a Nullable Dimension Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableDimension<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, string?>> expression, Action<NullableDimensionValueBuilderOptions<TMessage, string?>>? configure = null)
            where TMessage : notnull
        {
            var o = new NullableDimensionValueBuilderOptions<TMessage, string?>(DimensionValueType.VARCHAR, x => x);
            configure?.Invoke(o);

            return builder.AddNullableDimension(expression, o);
        }

        /// <summary>
        /// Adds a Dimension Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddDimension<TMessage, TProperty>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TProperty>> expression, Action<DimensionValueBuilderOptions<TMessage, TProperty>>? configure = null)
            where TMessage : notnull
            where TProperty : notnull
        {
            var o = new DimensionValueBuilderOptions<TMessage, TProperty>(
                DimensionValueType.VARCHAR,
                x =>
                {
                    if (x is null)
                    {
                        throw new InvalidOperationException("Unexpected Null value found for Dimension:{}, use AddNullableDimension instead");
                    }

                    return x.ToString()!;
                });
            configure?.Invoke(o);

            return builder.AddDimension(expression, o);
        }

        /// <summary>
        /// Adds a Nullable Dimension Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddNullableDimension<TMessage, TProperty>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TProperty?>> expression, Action<NullableDimensionValueBuilderOptions<TMessage, TProperty?>>? configure = null)
            where TMessage : notnull
        {
            var o = new NullableDimensionValueBuilderOptions<TMessage, TProperty?>(DimensionValueType.VARCHAR, x => x?.ToString());
            configure?.Invoke(o);

            return builder.AddNullableDimension(expression, o);
        }

        /// <summary>
        /// Adds a VARCHAR Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, string?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, string?>>? configure = null)
                where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, string?>(MeasureValueType.VARCHAR, x => x);
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from long) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, long>> expression, Action<MeasureValueBuilderOptions<TMessage, long>>? configure = null)
             where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, long>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from short) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, short>> expression, Action<MeasureValueBuilderOptions<TMessage, short>>? configure = null)
              where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, short>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from int) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, int>> expression, Action<MeasureValueBuilderOptions<TMessage, int>>? configure = null)
              where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, int>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from sbyte) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, sbyte>> expression, Action<MeasureValueBuilderOptions<TMessage, sbyte>>? configure = null)
            where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, sbyte>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from ulong) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ulong>> expression, Action<MeasureValueBuilderOptions<TMessage, ulong>>? configure = null)
            where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, ulong>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from ushort) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ushort>> expression, Action<MeasureValueBuilderOptions<TMessage, ushort>>? configure = null)
              where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, ushort>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from uint) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, uint>> expression, Action<MeasureValueBuilderOptions<TMessage, uint>>? configure = null)
            where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, uint>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from byte) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, byte>> expression, Action<MeasureValueBuilderOptions<TMessage, byte>>? configure = null)
            where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, byte>(MeasureValueType.BIGINT, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BIGINT (from byte) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage, TEnum>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, TEnum>> expression, Action<MeasureValueBuilderOptions<TMessage, TEnum>>? configure = null)
            where TMessage : notnull
            where TEnum : Enum
        {
            var o = new MeasureValueBuilderOptions<TMessage, TEnum>(MeasureValueType.BIGINT, x => ((long)(object)x).ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a BOOLEAN Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, bool>> expression, Action<MeasureValueBuilderOptions<TMessage, bool>>? configure = null)
             where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, bool>(MeasureValueType.BOOLEAN, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a DOUBLE Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, double>> expression, Action<MeasureValueBuilderOptions<TMessage, double>>? configure = null)
            where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, double>(MeasureValueType.DOUBLE, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a DOUBLE (from float) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, float>> expression, Action<MeasureValueBuilderOptions<TMessage, float>>? configure = null)
               where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, float>(MeasureValueType.DOUBLE, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a DOUBLE (from decimal)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, decimal>> expression, Action<MeasureValueBuilderOptions<TMessage, decimal>>? configure = null)
                  where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, decimal>(MeasureValueType.DOUBLE, x => x.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a TIMESTAMP (from DateOnly)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateOnly>> expression, Action<MeasureValueBuilderOptions<TMessage, DateOnly>>? configure = null)
               where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, DateOnly>(MeasureValueType.TIMESTAMP, BuildTimestampFromDateTime);
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a TIMESTAMP (from DateTime)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTime>> expression, Action<MeasureValueBuilderOptions<TMessage, DateTime>>? configure = null)
               where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, DateTime>(MeasureValueType.TIMESTAMP, BuildTimestampFromDateTime);
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
        }

        /// <summary>
        /// Adds a TIMESTAMP (from DateTimeOffset)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTimeOffset>> expression, Action<MeasureValueBuilderOptions<TMessage, DateTimeOffset>>? configure = null)
              where TMessage : notnull
        {
            var o = new MeasureValueBuilderOptions<TMessage, DateTimeOffset>(MeasureValueType.TIMESTAMP, BuildTimestampFromDateTimeOffset);
            configure?.Invoke(o);

            return builder.AddMeasure(expression, o);
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
              where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, long?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from short?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, short?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, short?>>? configure = null)
              where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, short?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from int?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, int?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, int?>>? configure = null)
            where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, int?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from sbyte?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, sbyte?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, sbyte?>>? configure = null)
            where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, sbyte?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from ulong?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ulong?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, ulong?>>? configure = null)
                where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, ulong?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from ushort?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, ushort?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, ushort?>>? configure = null)
                where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, ushort?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from uint?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, uint?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, uint?>>? configure = null)
                where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, uint?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BIGINT (from byte?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, byte?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, byte?>>? configure = null)
                 where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, byte?>(MeasureValueType.BIGINT, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional BOOLEAN (from bool?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, bool?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, bool?>>? configure = null)
              where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, bool?>(MeasureValueType.BOOLEAN, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional DOUBLE (from double?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, double?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, double?>>? configure = null)
               where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, double?>(MeasureValueType.DOUBLE, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional DOUBLE (from float?) Measure
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, float?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, float?>>? configure = null)
               where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, float?>(MeasureValueType.DOUBLE, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional DOUBLE (from decimal?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, decimal?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, decimal?>>? configure = null)
              where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, decimal?>(MeasureValueType.DOUBLE, x => x?.ToString(CultureInfo.InvariantCulture));
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional TIMESTAMP (from DateOnly?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateOnly?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, DateOnly?>>? configure = null)
               where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, DateOnly?>(MeasureValueType.TIMESTAMP, BuildTimestampFromNullableDateOnly);
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional TIMESTAMP (from DateTime?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTime?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, DateTime?>>? configure = null)
               where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, DateTime?>(MeasureValueType.TIMESTAMP, BuildTimestampFromNullableDateTime);
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        /// <summary>
        /// Adds an optional TIMESTAMP (from DateTimeOffset?)
        /// </summary>
        /// <typeparam name="TMessage">message types</typeparam>
        /// <param name="builder">extended builder</param>
        /// <param name="expression">property accessor expression</param>
        /// <param name="configure">configure callback</param>
        /// <returns>builder</returns>
        public static IProtocolBuilder<TMessage> AddMeasure<TMessage>(this IProtocolBuilder<TMessage> builder, Expression<Func<TMessage, DateTimeOffset?>> expression, Action<NullableMeasureValueBuilderOptions<TMessage, DateTimeOffset?>>? configure = null)
                where TMessage : notnull
        {
            var o = new NullableMeasureValueBuilderOptions<TMessage, DateTimeOffset?>(MeasureValueType.TIMESTAMP, BuildTimestampFromNullableDateTimeOffset);
            configure?.Invoke(o);

            return builder.AddNullableMeasure(expression, o);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? BuildTimestampFromNullableDateOnly(DateOnly? date)
        {
            if (date is null)
            {
                return null;
            }

            return BuildTimestampFromDateTime(date.Value);
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
        private static string BuildTimestampFromDateTime(DateOnly date)
        {
            return BuildTimestampFromDateTime(date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
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