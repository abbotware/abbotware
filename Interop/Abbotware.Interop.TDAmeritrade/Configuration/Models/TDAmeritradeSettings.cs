// -----------------------------------------------------------------------
// <copyright file="TDAmeritradeSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Configuration.Models
{
    using Abbotware.Interop.RestSharp.Configuration.Models;

    /// <summary>
    /// Editable API Settings for TD Ameritrade Client
    /// </summary>
    public class TDAmeritradeSettings : ApiSettings, ITDAmeritradeSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TDAmeritradeSettings"/> class.
        /// </summary>
        public TDAmeritradeSettings()
        {
            this.ApiKey = "apikey";
        }

        /// <inheritdoc/>
        public string? BearerToken { get; set; }
    }
}
