// -----------------------------------------------------------------------
// <copyright file="IRequestResponse{TRequest,TResponse}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// request response based message handler
    /// </summary>
    /// <typeparam name="TRequest">request type</typeparam>
    /// <typeparam name="TResponse">response type</typeparam>
    public interface IRequestResponse<TRequest, TResponse>
    {
        /// <summary>
        /// Handles a message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task<TResponse> HandleAsync(TRequest message, CancellationToken ct);
    }
}