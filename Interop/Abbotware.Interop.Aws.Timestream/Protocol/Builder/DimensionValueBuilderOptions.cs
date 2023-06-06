// -----------------------------------------------------------------------
// <copyright file="DimensionValueBuilderOptions.cs" company="Abbotware, LLC">
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
    public class DimensionValueBuilderOptions<TMessage, TProperty> : ValueBuilderOptions<TMessage, TProperty, string, DimensionValueType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionValueBuilderOptions{TMessage,TProperty}"/> class.
        /// </summary>
        /// <param name="measureValueType">Timestream MeasureValueType</param>
        /// <param name="converter">sets the default converter</param>
        public DimensionValueBuilderOptions(DimensionValueType measureValueType, Func<TProperty, string> converter)
            : base(measureValueType, converter)
        {
        }
    }
}