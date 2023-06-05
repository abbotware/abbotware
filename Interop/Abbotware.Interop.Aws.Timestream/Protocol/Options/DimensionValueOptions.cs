// -----------------------------------------------------------------------
// <copyright file="DimensionValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// DimensionValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <param name="Type">DimensionValueType</param>
    /// <param name="Lookup">lookup function</param>
    public record class DimensionValueOptions<TMessage>(DimensionValueType Type, Func<TMessage, string> Lookup) : MessagePropertyOption<DimensionValueType,  TMessage, string>(Type, Lookup);
}
