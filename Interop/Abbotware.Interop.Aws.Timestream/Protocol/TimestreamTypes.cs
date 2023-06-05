// -----------------------------------------------------------------------
// <copyright file="TimestreamTypes.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System;
    using System.Collections.Generic;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Timestream Type info
    /// </summary>
    public static class TimestreamTypes
    {
        /// <summary>
        /// map of C# type to timestream measure value type
        /// </summary>
        public static readonly IReadOnlyDictionary<Type, MeasureValueType> MeasureTypes = new Dictionary<Type, MeasureValueType>
        {
            {
                typeof(int), MeasureValueType.BIGINT
            },
            {
                typeof(short), MeasureValueType.BIGINT
            },
            {
                typeof(long), MeasureValueType.BIGINT
            },
            {
                typeof(byte), MeasureValueType.BIGINT
            },
            {
                typeof(uint), MeasureValueType.BIGINT
            },
            {
                typeof(ushort), MeasureValueType.BIGINT
            },
            {
                typeof(ulong), MeasureValueType.BIGINT
            },
            {
                typeof(sbyte), MeasureValueType.BIGINT
            },
            {
                typeof(float), MeasureValueType.DOUBLE
            },
            {
                typeof(double), MeasureValueType.DOUBLE
            },
            {
                typeof(decimal), MeasureValueType.DOUBLE
            },
            {
                typeof(DateTime), MeasureValueType.TIMESTAMP
            },
            {
                typeof(DateTimeOffset), MeasureValueType.TIMESTAMP
            },
            {
                typeof(bool), MeasureValueType.BOOLEAN
            },
            {
                typeof(string), MeasureValueType.VARCHAR
            },
        };

        /// <summary>
        /// map of C# type to timestream dimension type
        /// </summary>
        public static readonly IReadOnlyDictionary<Type, DimensionValueType> DimensionTypes = new Dictionary<Type, DimensionValueType>
        {
            {
                typeof(string), DimensionValueType.VARCHAR
            },
        };
    }
}
