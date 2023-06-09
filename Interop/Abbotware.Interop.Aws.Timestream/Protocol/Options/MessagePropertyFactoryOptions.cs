// -----------------------------------------------------------------------
// <copyright file="MessagePropertyFactoryOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Options
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Protocol.Internal;

    /// <summary>
    /// message property options class
    /// </summary>
    /// <typeparam name="TProtocolType"></typeparam>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProperty">messsage property type</typeparam>
    /// <typeparam name="TStorage">storage type for the converted property</typeparam>
    /// <typeparam name="TNative">native storage unit</typeparam>
    /// <param name="Type">protocol specific type</param>
    /// <param name="Expression">property evaluator expression</param>
    /// <param name="Converter">converter function</param>
    /// <param name="SourceName">source property name</param>
    /// <param name="TargetName">target name</param>
    public abstract record class MessagePropertyFactoryOptions

        <TProtocolType, TMessage, TProperty, TStorage, TNative>(TProtocolType Type, Func<TMessage, TProperty> Expression, Func<TProperty, TStorage> Converter, string SourceName, string TargetName) : IMessagePropertyFactory<TMessage, TNative>
        where TMessage : notnull
        where TNative : notnull
    {
        /// <summary>
        /// Creates the protocol specific holder
        /// </summary>
        /// <param name="message">source message</param>
        /// <returns>protocol storage</returns>
        public abstract TNative? Create(TMessage message);
    }
}