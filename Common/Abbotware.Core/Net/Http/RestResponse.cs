// -----------------------------------------------------------------------
// <copyright file="RestResponse.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Net.Http
{
    using System.Net;

    /// <summary>
    /// REST response wrapper
    /// </summary>
    public abstract class RestResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse"/> class.
        /// </summary>
        /// <param name="code">HTTP Status code</param>
        /// <param name="rawRequest">raw request</param>
        /// <param name="rawResponse">raw response</param>
        protected RestResponse(HttpStatusCode code, string rawRequest, string rawResponse)
        {
            this.StatusCode = code;
            this.RawRequest = rawRequest;
            this.RawResponse = rawResponse;
        }

        /// <summary>
        /// Gets the raw response
        /// </summary>
        public string? RawRequest { get; }

        /// <summary>
        /// Gets the raw result
        /// </summary>
        public string? RawResponse { get; }

        /// <summary>
        /// Gets the Http Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; }
    }
}