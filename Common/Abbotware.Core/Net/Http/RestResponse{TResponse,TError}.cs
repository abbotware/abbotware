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
    public class RestResponse<TResponse, TError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TResponse, TError}"/> class.
        /// </summary>
        /// <param name="response">response object</param>
        /// <param name="code">HTTP Status code</param>
        /// <param name="raw">raw result</param>
        public RestResponse(TResponse response, HttpStatusCode code, string raw)
            : this(response, default, code, raw)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TResponse, TError}"/> class.
        /// </summary>
        /// <param name="error">error object</param>
        /// <param name="code">HTTP Status code</param>
        /// <param name="raw">raw result</param>
        public RestResponse(TError error, HttpStatusCode code, string raw)
            : this(default, error, code, raw)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TResponse, TError}"/> class.
        /// </summary>
        /// <param name="response">response object</param>
        /// <param name="error">error object</param>
        /// <param name="code">HTTP Status code</param>
        /// <param name="raw">raw result</param>
        public RestResponse(TResponse? response, TError? error, HttpStatusCode code, string raw)
        {
            this.Response = response;
            this.Error = error;
            this.StatusCode = code;
            this.Raw = raw;
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        public TResponse? Response { get; }

        /// <summary>
        /// Gets the error
        /// </summary>
        public TError? Error { get; }

        /// <summary>
        /// Gets the Http Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets raw result
        /// </summary>
        public string Raw { get; }
    }
}
