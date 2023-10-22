// -----------------------------------------------------------------------
// <copyright file="MeasureValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// MeasureValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="Type">MeasureValueType</param>
    /// <param name="Expression">property evaluator expression</param>
    /// <param name="Converter">converter function</param>
    /// <param name="SourceName">source property name</param>
    /// <param name="TargetName">target name</param>
    public record class MeasureValueOptions<TMessage, TProperty>(MeasureValueType Type, Func<TMessage, TProperty> Expression, Func<TProperty, string?> Converter, string SourceName, string TargetName)
        : MessagePropertyFactoryOptions<MeasureValueType, TMessage, TProperty, string?, MeasureValue>(Type, Expression, Converter, SourceName, TargetName)
        where TMessage : notnull
    {
        /// <summary>
        /// Creates a Measure Value
        /// </summary>
        /// <param name="message">message </param>
        /// <returns>measure value</returns>
        public override MeasureValue? Create(TMessage message)
        {
            var v = this.Expression(message);

            if (v is null)
            {
                return null;
            }

            var s = this.Converter(v);

            if (s is null)
            {
                return null;
            }

            return new MeasureValue
            {
                Type = this.Type,
                Name = this.TargetName,
                Value = s,
            };
        }
    }
}
