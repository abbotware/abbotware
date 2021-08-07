// -----------------------------------------------------------------------
// <copyright file="IApiSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Configuration
{
    /// <summary>
    /// Readonly interface for api settings
    /// </summary>
    public interface IApiSettings
    {
        /// <summary>
        /// Gets the OAuth User ID / API Key for unauthenticated request for delayed data
        /// </summary>
        string? ApiKey { get; }

        /// <summary>
        /// Gets the Bearer token
        /// </summary>
        string? BearerToken { get; }
    }
}