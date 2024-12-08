// -----------------------------------------------------------------------
// <copyright file="SecApiSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SecApi.Configuration.Models
{
    using Abbotware.Interop.RestSharp.Configuration.Models;
    using Abbotware.Interop.SecApi.Configuration;

    /// <summary>
    /// Editable API Settings for Sec Api Client
    /// </summary>
    public class SecApiSettings : ApiSettings, ISecApiSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecApiSettings"/> class.
        /// </summary>
        public SecApiSettings()
        {
            this.ApiKeyQueryParameterName = "token";
        }
    }
}
