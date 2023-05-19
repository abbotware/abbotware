// -----------------------------------------------------------------------
// <copyright file="RestResponse{TResponse,TError}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Net.Http
{
    using System.Net;

    /// <summary>
    /// REST response wrapper
    /// </summary>
    /// <typeparam name="TResponse">response object type</typeparam>
    /// <typeparam name="TError">error object type</typeparam>
    /// <param name="StatusCode">HTTP Status code</param>
    /// <param name="RawRequest">raw request</param>
    /// <param name="RawResponse">raw response</param>
    public record RestResponse<TResponse, TError>(HttpStatusCode StatusCode, string RawRequest, string? RawResponse)
        : RestResponse(StatusCode, RawRequest, RawResponse)
    {
        /// <summary>
        /// Gets the response
        /// </summary>
        public TResponse? Response { get; init; }

        /// <summary>
        /// Gets the error
        /// </summary>
        public TError? Error { get; init; }
    }
}
