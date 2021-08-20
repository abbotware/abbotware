// -----------------------------------------------------------------------
// <copyright file="ErrorResponse.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    /// <summary>
    ///  Error response class
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string Error { get; set; } = string.Empty;
    }
}
