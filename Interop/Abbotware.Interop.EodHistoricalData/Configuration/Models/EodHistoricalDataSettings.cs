// -----------------------------------------------------------------------
// <copyright file="EodHistoricalDataSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Configuration.Models
{
    using Abbotware.Interop.RestSharp.Configuration.Models;

    /// <summary>
    /// Editable API Settings for EOD Historical Data Client
    /// </summary>
    public class EodHistoricalDataSettings : ApiSettings, IEodHistoricalDataSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EodHistoricalDataSettings"/> class.
        /// </summary>
        public EodHistoricalDataSettings()
        {
            this.ApiKeyQueryParameterName = "api_token";
        }
    }
}
