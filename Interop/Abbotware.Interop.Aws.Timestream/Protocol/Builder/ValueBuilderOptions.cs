// -----------------------------------------------------------------------
// <copyright file="ValueBuilderOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Builder
{
    using System;

    /// <summary>
    /// Measure Value Builder Options
    /// </summary>
    /// <typeparam name="TMessage">Message Type</typeparam>
    /// <typeparam name="TProperty">Property Type</typeparam>
    /// <typeparam name="TStorage">string type (for non-nullable reference support)</typeparam>
    /// <typeparam name="TValueType">value type</typeparam>
    public class ValueBuilderOptions<TMessage, TProperty, TStorage, TValueType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueBuilderOptions{TMessage, TProperty, TStorage, TValueType}"/> class.
        /// </summary>
        /// <param name="valueType">Timestream valueType</param>
        /// <param name="converter">sets the default converter</param>
        public ValueBuilderOptions(TValueType valueType, Func<TProperty, TStorage> converter)
        {
            this.Converter = converter;
            this.ValueType = valueType;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the conversion fuction for serialization
        /// </summary>
        public Func<TProperty, TStorage> Converter { get; set; }

        /// <summary>
        /// Gets the Timestream MeasureValueType
        /// </summary>
        public TValueType ValueType { get; }
    }
}