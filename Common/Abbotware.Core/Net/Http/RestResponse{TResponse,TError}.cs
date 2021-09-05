// -----------------------------------------------------------------------
// <copyright file="RestResponse{TResponse,TError}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
    public class RestResponse<TResponse, TError> : RestResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TResponse, TError}"/> class.
        /// </summary>
        /// <param name="response">response object</param>
        /// <param name="code">HTTP Status code</param>
        /// <param name="rawRequest">raw request</param>
        /// <param name="rawResponse">raw response</param>
        public RestResponse(TResponse response, HttpStatusCode code, string rawRequest, string rawResponse)
            : this(response, default, code, rawRequest, rawResponse)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TResponse, TError}"/> class.
        /// </summary>
        /// <param name="error">error object</param>
        /// <param name="code">HTTP Status code</param>
        /// <param name="rawRequest">raw request</param>
        /// <param name="rawResponse">raw response</param>
        public RestResponse(TError error, HttpStatusCode code, string rawRequest, string rawResponse)
            : this(default, error, code, rawRequest, rawResponse)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TResponse, TError}"/> class.
        /// </summary>
        /// <param name="response">response object</param>
        /// <param name="error">error object</param>
        /// <param name="code">HTTP Status code</param>
        /// <param name="rawRequest">raw request</param>
        /// <param name="rawResponse">raw response</param>
        public RestResponse(TResponse? response, TError? error, HttpStatusCode code, string rawRequest, string rawResponse)
            : base(code, rawRequest, rawResponse)
        {
            this.Response = response;
            this.Error = error;
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        public TResponse? Response { get; }

        /// <summary>
        /// Gets the error
        /// </summary>
        public TError? Error { get; }
    }
}
