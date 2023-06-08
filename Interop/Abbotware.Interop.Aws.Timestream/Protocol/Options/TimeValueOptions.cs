// -----------------------------------------------------------------------
// <copyright file="TimeValueOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;

    /// <summary>
    /// TimeValue Options class
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <param name="Type">TimeUnitType</param>
    /// <param name="Lookup">lookup function</param>
    /// <param name="SourceName">source property name</param>
    public record class TimeValueOptions<TMessage>(TimeUnitType Type, Func<TMessage, DateTimeOffset> Lookup, string SourceName) : MessagePropertyOption<TimeUnitType, TMessage, DateTimeOffset>(Type, Lookup, SourceName, "Time");
}
