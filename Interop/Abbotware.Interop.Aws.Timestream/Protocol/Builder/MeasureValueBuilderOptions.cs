// -----------------------------------------------------------------------
// <copyright file="MeasureValueBuilderOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Builder
{
    using System;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Measure Value Builder Options (non-nullable)
    /// </summary>
    /// <typeparam name="TMessage">Message Type</typeparam>
    /// <typeparam name="TProperty">Property Type</typeparam>
    public class MeasureValueBuilderOptions<TMessage, TProperty> : ValueBuilderOptions<TMessage, TProperty, string, MeasureValueType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasureValueBuilderOptions{TMessage, TProperty}"/> class.
        /// </summary>
        /// <param name="valueType">Timestream MeasureValueType</param>
        /// <param name="converter">sets the default converter</param>
        public MeasureValueBuilderOptions(MeasureValueType valueType, Func<TProperty, string> converter)
            : base(valueType, converter)
        {
        }
    }
}