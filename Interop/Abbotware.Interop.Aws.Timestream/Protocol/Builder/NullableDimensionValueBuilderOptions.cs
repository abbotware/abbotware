// -----------------------------------------------------------------------
// <copyright file="NullableDimensionValueBuilderOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Builder
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Measure Value Builder Options (nullable)
    /// </summary>
    /// <typeparam name="TMessage">Message Type</typeparam>
    /// <typeparam name="TProperty">Property Type</typeparam>
    public class NullableDimensionValueBuilderOptions<TMessage, TProperty> : ValueBuilderOptions<TMessage, TProperty?, string?, DimensionValueType>
        where TMessage : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableDimensionValueBuilderOptions{TMessage,TProperty}"/> class.
        /// </summary>
        /// <param name="name">column name</param>
        /// <param name="measureValueType">Timestream MeasureValueType</param>
        /// <param name="function">message to property function</param>
        /// <param name="converter">sets the default converter</param>
        public NullableDimensionValueBuilderOptions(string name, DimensionValueType measureValueType, Func<TMessage, TProperty?> function, Func<TProperty?, string?> converter)
            : base(name, measureValueType, function, converter)
        {
        }

        /// <summary>
        /// Build the DimensionValue Options
        /// </summary>
        /// <returns>DimensionValue Options</returns>
        public NullableDimensionValueOptions<TMessage, TProperty> Build()
        {
            return new NullableDimensionValueOptions<TMessage, TProperty>(this.ValueType, this.Function, this.Converter, string.Empty, this.Name);
        }
    }
}