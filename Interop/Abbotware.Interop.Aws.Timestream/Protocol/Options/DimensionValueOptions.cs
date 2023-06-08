// -----------------------------------------------------------------------
// <copyright file="DimensionValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using Amazon.TimestreamWrite;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// DimensionValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <param name="Type">DimensionValueType</param>
    /// <param name="Lookup">lookup function</param>
    /// <param name="IsNull">IsNull lookup function</param>
    /// <param name="SourceName">source property name</param>
    /// <param name="TargetName">target name</param>
    public record class DimensionValueOptions<TMessage>(DimensionValueType Type, Func<TMessage, string> Lookup, Func<TMessage, bool> IsNull, string SourceName, string TargetName) : MessagePropertyOption<DimensionValueType, TMessage, string>(Type, Lookup, SourceName, TargetName)
    {
        /// <summary>
        /// Creates a Dimension Value
        /// </summary>
        /// <param name="message">message </param>
        /// <returns>measure value</returns>
        public Dimension? Create(TMessage message)
        {
            if (this.IsNull(message))
            {
                return null;
            }

            var v = this.Lookup(message);

            return new Dimension
            {
                DimensionValueType = this.Type,
                Name = this.TargetName,
                Value = v,
            };
        }
    }
}
