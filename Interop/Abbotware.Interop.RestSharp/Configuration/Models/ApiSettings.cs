// -----------------------------------------------------------------------
// <copyright file="ApiSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RestSharp.Configuration.Models
{
    /// <summary>
    /// Editable API Settings
    /// </summary>
    public class ApiSettings : IApiSettings
    {
        /// <inheritdoc/>
        public string? ApiKey { get; set; }

        /// <inheritdoc/>
        public string? ApiKeyQueryParameterName { get; protected set; } = "apikey";
    }
}
