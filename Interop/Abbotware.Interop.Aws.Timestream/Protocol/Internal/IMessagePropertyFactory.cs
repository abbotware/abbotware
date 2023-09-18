// -----------------------------------------------------------------------
// <copyright file="IMessagePropertyFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Internal
{
    /// <summary>
    /// Interface to create a native storage type from the message
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TNative">native storage type</typeparam>
    public interface IMessagePropertyFactory<TMessage, TNative>
        where TMessage : notnull
        where TNative : notnull
    {
        /// <summary>
        /// gets the target name
        /// </summary>
        string TargetName { get; }

        /// <summary>
        /// Creates the native storage from the message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>native storage type</returns>
        TNative? Create(TMessage message);
    }
}