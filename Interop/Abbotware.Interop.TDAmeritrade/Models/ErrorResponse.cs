// -----------------------------------------------------------------------
// <copyright file="ErrorResponse.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using System.Net;

    /// <summary>
    ///  Error response class
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error
        /// </summary>
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets error path
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets error path
        /// </summary>
        public HttpStatusCode? Status { get; set; }

        /// <summary>
        /// Gets or sets the time stamp
        /// </summary>
        public DateTimeOffset? Timestamp { get; set; }
    }
}