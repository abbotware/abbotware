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
    /// <param name="Type">MeasureValueType</param>
    /// <param name="Lookup">lookup function</param>
    public record class MeasureValueOptions<TMessage>(MeasureValueType Type, Func<TMessage, string?> Lookup) : MessagePropertyOption<MeasureValueType, TMessage, string?>(Type, Lookup)
    {
        /// <summary>
        /// Creates a Measure Value
        /// </summary>
        /// <param name="message">message </param>
        /// <param name="name">measure name</param>
        /// <returns>measure value</returns>
        public MeasureValue? Create(TMessage message, string name)
        {
            var v = this.Lookup(message);

            if (v is null)
            {
                return null;
            }

            return new MeasureValue
            {
                Type = this.Type,
                Name = name,
                Value = v,
            };
        }
    }
}
