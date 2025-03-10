﻿// -----------------------------------------------------------------------
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
    /// <typeparam name="TStorage">storage type for the converted property</typeparam>
    /// <param name="Type">protocol specific type</param>
    /// <param name="Expression">property evaluator expression</param>
    /// <param name="Converter">converter function</param>
    /// <param name="SourceName">source property name</param>
    /// <param name="TargetName">target name</param>
    public record class MessagePropertyOption<TProtocolType, TMessage, TProperty, TStorage>(TProtocolType Type, Func<TMessage, TProperty> Expression, Func<TProperty, TStorage> Converter, string SourceName, string TargetName)
        where TMessage : notnull;
}