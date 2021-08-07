// -----------------------------------------------------------------------
// <copyright file="ApiSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Configuration.Models
{
    /// <summary>
    /// Editable API Settings
    /// </summary>
    public class ApiSettings : IApiSettings
    {
        /// <inheritdoc/>
        public string? ApiKey { get; set; }

        /// <inheritdoc/>
        public string? BearerToken { get; set; }
    }
}
