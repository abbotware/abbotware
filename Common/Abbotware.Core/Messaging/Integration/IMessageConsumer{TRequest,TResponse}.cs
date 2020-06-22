// -----------------------------------------------------------------------
// <copyright file="IMessageConsumer{TRequest,TResponse}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    /// <summary>
    /// Interface for a consuming a request message and returning a response message
    /// </summary>
    /// <typeparam name="TRequest">Type of Request Message</typeparam>
    /// <typeparam name="TResponse">Type of Response Message</typeparam>
    public interface IMessageConsumer<TRequest, TResponse>
    {
        /// <summary>
        /// Handles a request message and returns a response message
        /// </summary>
        /// <param name="message">request message to process</param>
        /// <returns>reply message</returns>
        TResponse OnHandle(TRequest message);
    }
}