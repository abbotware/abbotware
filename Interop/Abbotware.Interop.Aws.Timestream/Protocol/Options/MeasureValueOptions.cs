// -----------------------------------------------------------------------
// <copyright file="MeasureValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// MeasureValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <param name="Type">MeasureValueType</param>
    /// <param name="Lookup">lookup function</param>
    public record class MeasureValueOptions<TMessage>(MeasureValueType Type, Func<TMessage, string?> Lookup) : MessagePropertyOption<MeasureValueType,  TMessage,  string?>(Type, Lookup);
}
