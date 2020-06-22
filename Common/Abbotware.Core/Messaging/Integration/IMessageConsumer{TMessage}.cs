// -----------------------------------------------------------------------
// <copyright file="IMessageConsumer{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    /// <summary>
    /// Interface for a Message consumer
    /// </summary>
    /// <typeparam name="TMessage">Message type</typeparam>
    public interface IMessageConsumer<TMessage>
    {
        /// <summary>
        /// Handles a message object
        /// </summary>
        /// <param name="message">Message to process</param>
        void OnHandle(TMessage message);
    }
}