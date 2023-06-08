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
    public record class DimensionValueOptions<TMessage>(DimensionValueType Type, Func<TMessage, string> Lookup, Func<TMessage, bool> IsNull) : MessagePropertyOption<DimensionValueType, TMessage, string>(Type, Lookup)
    {
        /// <summary>
        /// Creates a Dimension Value
        /// </summary>
        /// <param name="message">message </param>
        /// <param name="name">measure name</param>
        /// <returns>measure value</returns>
        public Dimension? Create(TMessage message, string name)
        {
            if (this.IsNull(message))
            {
                return null;
            }

            var v = this.Lookup(message);

            return new Dimension
            {
                DimensionValueType = this.Type,
                Name = name,
                Value = v,
            };
        }
    }
}
