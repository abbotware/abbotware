// -----------------------------------------------------------------------
// <copyright file="EodHistoricalDataClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData
{
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Net.Http;
    using Abbotware.Interop.EodHistoricalData.Configuration.Models;
    using Abbotware.Interop.EodHistoricalData.Models;
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

        /// <summary>
        /// retrieve all fundamental data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<Fundamental, string>> FundamentalAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            return this.OnExecuteAsync<Fundamental, string>(request, ct);
        }

        /// <summary>
        /// retrieve fundamental highlights data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<Highlights, string>> FundamentalHighlightsAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            request.AddParameter("filter", "Highlights");

            return this.OnExecuteAsync<Highlights, string>(request, ct);
        }

        /// <summary>
        /// retrieve fundamental earnings data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<Earnings, string>> FundamentalEarningsAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            request.AddParameter("filter", "Earnings");

            return this.OnExecuteAsync<Earnings, string>(request, ct);
        }

        /// <summary>
        /// retrieve fundamental financials data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<Financials, string>> FundamentalFinancialsAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            request.AddParameter("filter", "Financials");

            return this.OnExecuteAsync<Financials, string>(request, ct);
        }

        /// <summary>
        /// retrieve fundamental general data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<General, string>> FundamentalGeneralAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            request.AddParameter("filter", "General");

            return this.OnExecuteAsync<General, string>(request, ct);
        }

        private static RestRequest CreateFundamentalRequest(string symbol, string exchange)
        {
            var request = new RestRequest("fundamentals/{symbol}.{exchange}", Method.GET, DataFormat.None);
            request.AddUrlSegment("symbol", symbol, false);
            request.AddUrlSegment("exchange", exchange, false);
            return request;
        }
    }
}
