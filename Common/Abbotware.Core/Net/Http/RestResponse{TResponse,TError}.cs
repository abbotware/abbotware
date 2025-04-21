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

        /// <summary>
        /// Replace the response with another (usually transformed)
        /// </summary>
        /// <typeparam name="TTransform">other type to transform</typeparam>
        /// <param name="transform">transformed</param>
        /// <returns>new response with a different response type</returns>
        public RestResponse<TTransform, TError> TransformResponse<TTransform>(TTransform? transform)
        {
            return new RestResponse<TTransform, TError>(this.StatusCode, this.RawRequest, this.RawResponse)
            { Response = transform, Error = this.Error };
        }

        /// <summary>
        /// Replace the error with another (usually transformed)
        /// </summary>
        /// <typeparam name="TTransform">other type to transform</typeparam>
        /// <param name="transform">transformed</param>
        /// <returns>new response with a different error type</returns>
        public RestResponse<TResponse, TTransform> TransformError<TTransform>(TTransform? transform)
        {
            return new RestResponse<TResponse, TTransform>(this.StatusCode, this.RawRequest, this.RawResponse)
            { Response = this.Response, Error = transform };
        }
    }
}
