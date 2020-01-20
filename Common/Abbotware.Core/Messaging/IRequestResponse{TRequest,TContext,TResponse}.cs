// -----------------------------------------------------------------------
// <copyright file="IRequestResponse{TRequest,TContext,TResponse}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// request response based message handler with a context
    /// </summary>
    /// <typeparam name="TRequest">request type</typeparam>
    /// <typeparam name="TContext">context type</typeparam>
    /// <typeparam name="TResponse">response type</typeparam>
    public interface IRequestResponse<TRequest, TContext, TResponse>
    {
        /// <summary>
        /// Handles a message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="context">context</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task<TResponse> HandleAsync(TRequest message, TContext context, CancellationToken ct);
    }
}