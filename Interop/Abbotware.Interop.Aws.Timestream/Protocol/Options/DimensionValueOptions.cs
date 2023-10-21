// -----------------------------------------------------------------------
// <copyright file="DimensionValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using Abbotware.Core.Extensions;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// DimensionValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="Type">DimensionValueType</param>
    /// <param name="Expression">property evaluator expression</param>
    /// <param name="Converter">converter function</param>
    /// <param name="SourceName">source property name</param>
    /// <param name="TargetName">target name</param>
    public record class DimensionValueOptions<TMessage, TProperty>(DimensionValueType Type, Func<TMessage, TProperty> Expression, Func<TProperty, string> Converter, string SourceName, string TargetName) 
        : MessagePropertyFactoryOptions<DimensionValueType, TMessage, TProperty, string, Dimension>(Type, Expression, Converter, SourceName, TargetName)
        where TMessage : notnull
        where TProperty : notnull
    {
        /// <summary>
        /// Creates a Dimension Value
        /// </summary>
        /// <param name="message">message </param>
        /// <returns>measure value</returns>
        public override Dimension? Create(TMessage message)
        {
            var v = this.Expression(message);

            var s = this.Converter(v);

            if (s.IsBlank())
            {
                throw new ArgumentNullException(this.TargetName, $"Dimension {this.SourceName}->{this.TargetName} is null/empty string even after converter function was called. if this is expected, use a nullable dimension instead");
            }

            return new Dimension
            {
                DimensionValueType = this.Type,
                Name = this.TargetName,
                Value = s,
            };
        }
    }
}