// -----------------------------------------------------------------------
// <copyright file="IApiSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RestSharp.Configuration
{
    /// <summary>
    /// Readonly interface for api settings
    /// </summary>
    public interface IApiSettings
    {
        /// <summary>
        /// Gets the API Key
        /// </summary>
        string? ApiKey { get; }

        /// <summary>
        /// Gets the API Key Query Parameter Name
        /// </summary>
        string? ApiKeyQueryParameterName { get; }
    }
}