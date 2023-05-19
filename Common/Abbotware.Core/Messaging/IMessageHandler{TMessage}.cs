// -----------------------------------------------------------------------
// <copyright file="IMessageHandler{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// message handler
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IMessageHandler<TMessage>
    {
        /// <summary>
        /// Handles a message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task HandleAsync(TMessage message, CancellationToken ct);
    }
}