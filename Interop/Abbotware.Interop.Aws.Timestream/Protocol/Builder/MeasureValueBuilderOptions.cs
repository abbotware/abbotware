// -----------------------------------------------------------------------
// <copyright file="MeasureValueBuilderOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Builder
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Measure Value Builder Options (non-nullable)
    /// </summary>
    /// <typeparam name="TMessage">Message Type</typeparam>
    /// <typeparam name="TProperty">Property Type</typeparam>
    public class MeasureValueBuilderOptions<TMessage, TProperty> : ValueBuilderOptions<TMessage, TProperty, string, MeasureValueType>
        where TMessage : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasureValueBuilderOptions{TMessage, TProperty}"/> class.
        /// </summary>
        /// <param name="name">column name</param>
        /// <param name="valueType">Timestream MeasureValueType</param>
        /// <param name="function">message to property function</param>
        /// <param name="converter">sets the default converter</param>
        public MeasureValueBuilderOptions(string name, MeasureValueType valueType, Func<TMessage, TProperty> function, Func<TProperty, string> converter)
            : base(name, valueType, function, converter)
        {
        }

        /// <summary>
        /// Build the MeasureValueOptions Options
        /// </summary>
        /// <returns>MeasureValueOptions Options</returns>
        public MeasureValueOptions<TMessage, TProperty> Build()
        {
            return new MeasureValueOptions<TMessage, TProperty>(this.ValueType, this.Function, this.Converter, string.Empty, this.Name);
        }
    }
}