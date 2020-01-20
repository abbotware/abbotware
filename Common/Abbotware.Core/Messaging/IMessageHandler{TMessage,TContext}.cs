// -----------------------------------------------------------------------
// <copyright file="IMessageHandler{TMessage,TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// context based message handler
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TContext">context type</typeparam>
    public interface IMessageHandler<TMessage, TContext>
    {
        /// <summary>
        /// Handles a message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="context">context</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task HandleAsync(TMessage message, TContext context, CancellationToken ct);
    }
}