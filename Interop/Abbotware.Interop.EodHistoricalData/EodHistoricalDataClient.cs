// -----------------------------------------------------------------------
// <copyright file="EodHistoricalDataClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData
{
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.EodHistoricalData.Configuration.Models;
    using Abbotware.Interop.RestSharp;
    using global::RestSharp;

    /// <summary>
    /// EOD Historical Data API Client
    /// </summary>
    public class EodHistoricalDataClient : BaseRestClient<IEodHistoricalDataSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EodHistoricalDataClient"/> class.
        /// </summary>
        /// <param name="settings">api settings</param>
        /// <param name="logger">injectted logger</param>
        public EodHistoricalDataClient(IEodHistoricalDataSettings settings, ILogger logger)
            : base(new("https://eodhistoricaldata.com/api/"), settings, logger)
        {
        }
    }
}
