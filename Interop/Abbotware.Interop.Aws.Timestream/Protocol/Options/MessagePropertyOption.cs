// -----------------------------------------------------------------------
// <copyright file="MessagePropertyOption.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;

    /// <summary>
    /// message property options class
    /// </summary>
    /// <typeparam name="TProtocolType"></typeparam>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProperty">messsage property type</typeparam>
    /// <param name="Type">protocol specific type</param>
    /// <param name="Lookup">lookup function</param>
    /// <param name="SourceName">source property name</param>
    /// <param name="TargetName">target name</param>
    public record class MessagePropertyOption<TProtocolType, TMessage, TProperty>(TProtocolType Type, Func<TMessage, TProperty> Lookup, string SourceName, string TargetName);
}
