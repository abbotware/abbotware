// -----------------------------------------------------------------------
// <copyright file="NullableMeasureValueBuilderOptions.cs" company="Abbotware, LLC">
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
    public class NullableMeasureValueBuilderOptions<TMessage, TProperty> : ValueBuilderOptions<TMessage, TProperty?, string?, MeasureValueType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableMeasureValueBuilderOptions{TMessage, TProperty}"/> class.
        /// </summary>
        /// <param name="measureValueType">Timestream MeasureValueType</param>
        /// <param name="converter">sets the default converter</param>
        public NullableMeasureValueBuilderOptions(MeasureValueType measureValueType, Func<TProperty?, string?> converter)
            : base(measureValueType, converter)
        {
        }
    }
}