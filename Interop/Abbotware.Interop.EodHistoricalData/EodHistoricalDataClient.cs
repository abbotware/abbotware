﻿// -----------------------------------------------------------------------
// <copyright file="EodHistoricalDataClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Net.Http;
    using Abbotware.Interop.EodHistoricalData.Configuration.Models;
    using Abbotware.Interop.EodHistoricalData.Models;
    using Abbotware.Interop.RestSharp;
    using global::RestSharp;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// EOD Historical Data API Client
    /// </summary>
    public sealed class EodHistoricalDataClient : BaseRestClient<IEodHistoricalDataSettings>, IEodHistoricalDataClient
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
        /// Gets the fundamentals api subset
        /// </summary>
        public IFundamentalClient Fundamentals => this;

        /// <summary>
        /// Gets the exchange api subset
        /// </summary>
        public IExchangeClient Exchanges => this;

        /// <inheritdoc/>
        Task<RestResponse<List<Exchange>, string>> IExchangeClient.ListAsync(CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = new RestRequest("exchanges-list", Method.Get);

            return this.OnExecuteAsync<List<Exchange>, string>(request, ct);
        }

        /// <inheritdoc/>
        async Task<RestResponse<List<Ticker>, string>> IExchangeClient.TickersAsync(string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = new RestRequest("exchange-symbol-list/{EXCHANGE_CODE}", Method.Get);
            request.AddUrlSegment("EXCHANGE_CODE", exchange);
            request.AddParameter("fmt", "json");

            var r = await this.OnExecuteAsync<List<Ticker>, string>(request, ct)
                .ConfigureAwait(false);

            if (r.StatusCode == HttpStatusCode.OK)
            {
                var altered = r!.Response!.Select(x => x with { SourceExchange = exchange })
                    .ToList();

                r = r with { Response = altered };
            }

            return r;
        }

        /// <inheritdoc/>
        async Task<RestResponse<Fundamental, string>> IFundamentalClient.GetAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            var result = await this.OnExecuteAsync<Fundamental, string>(request, ct)
                .ConfigureAwait(false);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result!.Response!.General!.Exchange == null)
                {
                    result = result with { Response = result.Response with { General = result.Response.General with { Exchange = string.Empty } } };
                }

                if (result!.Response!.General!.Name == null)
                {
                    result = result with { Response = result.Response with { General = result.Response.General with { Name = string.Empty } } };
                }
            }

            return result;
        }

        /// <inheritdoc/>
        async Task<RestResponse<Fundamental, string>> IFundamentalClient.GetRawAsync(string symbol, string exchange, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            this.OnApplyAuthentication(request);

            var response = await this.Client.ExecuteAsync(request, ct)
              .ConfigureAwait(false);

            var content = response.Content ?? string.Empty;
            var status = response?.StatusCode ?? HttpStatusCode.InternalServerError;
            var responseUri = response?.ResponseUri?.ToString() ?? string.Empty;

            return new(status, responseUri, content) { Response = new Fundamental() };
        }

        /// <inheritdoc/>
        Task<RestResponse<Highlights, string>> IFundamentalClient.HighlightsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Highlights>(symbol, exchange, "Highlights", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<Earnings, string>> IFundamentalClient.EarningsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Earnings>(symbol, exchange, "Earnings", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<Financials, string>> IFundamentalClient.FinancialsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Financials>(symbol, exchange, "Financials", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<General, string>> IFundamentalClient.GeneralAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<General>(symbol, exchange, "General", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<Valuation, string>> IFundamentalClient.ValuationAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Valuation>(symbol, exchange, "Valuation", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<SharesStats, string>> IFundamentalClient.SharesStatsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<SharesStats>(symbol, exchange, "SharesStats", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<Technicals, string>> IFundamentalClient.TechnicalsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Technicals>(symbol, exchange, "Technicals", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<SplitsDividends, string>> IFundamentalClient.SplitsDividendsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<SplitsDividends>(symbol, exchange, "SplitsDividends", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<AnalystRatings, string>> IFundamentalClient.AnalystRatingsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<AnalystRatings>(symbol, exchange, "AnalystRatings", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<Holders, string>> IFundamentalClient.HoldersAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Holders>(symbol, exchange, "Holders", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<EsgScores, string>> IFundamentalClient.EsgScoresAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<EsgScores>(symbol, exchange, "ESGScores", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<OutstandingShares, string>> IFundamentalClient.OutstandingSharesAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<OutstandingShares>(symbol, exchange, "outstandingShares", ct);
        }

        /// <inheritdoc/>
        Task<RestResponse<Dictionary<int, InsiderTransaction>, string>> IFundamentalClient.InsiderTransactionsAsync(string symbol, string exchange, CancellationToken ct)
        {
            return this.FilteredFundamentalRequestAsync<Dictionary<int, InsiderTransaction>>(symbol, exchange, "InsiderTransactions", ct);
        }

        private static RestRequest CreateFundamentalRequest(string symbol, string exchange)
        {
            var request = new RestRequest("fundamentals/{symbol}.{exchange}", Method.Get);
            request.AddUrlSegment("symbol", symbol, false);
            request.AddUrlSegment("exchange", exchange, false);
            return request;
        }

        private Task<RestResponse<TRecord, string>> FilteredFundamentalRequestAsync<TRecord>(string symbol, string exchange, string filter, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateFundamentalRequest(symbol, exchange);

            request.AddParameter("filter", filter);

            return this.OnExecuteAsync<TRecord, string>(request, ct);
        }
    }
}