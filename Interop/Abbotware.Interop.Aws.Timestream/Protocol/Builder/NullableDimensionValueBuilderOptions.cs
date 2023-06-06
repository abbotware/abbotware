// -----------------------------------------------------------------------
// <copyright file="NullableDimensionValueBuilderOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Builder
{
    using System;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Measure Value Builder Options (nullable)
    /// </summary>
    /// <typeparam name="TMessage">Message Type</typeparam>
    /// <typeparam name="TProperty">Property Type</typeparam>
    public class NullableDimensionValueBuilderOptions<TMessage, TProperty> : ValueBuilderOptions<TMessage, TProperty?, string?, DimensionValueType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDimensionValueBuilderOptions{TMessage,TProperty}"/> class.
        /// </summary>
        /// <param name="measureValueType">Timestream MeasureValueType</param>
        /// <param name="converter">sets the default converter</param>
        public NullableDimensionValueBuilderOptions(DimensionValueType measureValueType, Func<TProperty?, string?> converter)
            : base(measureValueType, converter)
        {
        }
    }
}