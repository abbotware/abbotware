// -----------------------------------------------------------------------
// <copyright file="IFundamentalClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Net.Http;
    using Abbotware.Interop.EodHistoricalData.Models;

    /// <summary>
    /// Fundamentals subset api
    /// </summary>
    public interface IFundamentalClient
    {
        /// <summary>
        /// retrieve all fundamental data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Fundamental, string>> GetAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental earnings data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Earnings, string>> EarningsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental financials data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Financials, string>> FinancialsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental general data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<General, string>> GeneralAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental highlights data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Highlights, string>> HighlightsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Valuation data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Valuation, string>> ValuationAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Shares Stats data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<SharesStats, string>> SharesStatsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Technicals data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Technicals, string>> TechnicalsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Splits/Dividends data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<SplitsDividends, string>> SplitsDividendsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Analyst Ratings data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<AnalystRatings, string>> AnalystRatingsAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Holders data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Holders, string>> HoldersAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental EsgScores data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<EsgScores, string>> EsgScoresAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Outstanding Shares data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<OutstandingShares, string>> OutstandingSharesAsync(string symbol, string exchange, CancellationToken ct);

        /// <summary>
        /// retrieve fundamental Insider Transaction data for the supplied symbol
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="exchange">exchange</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<Dictionary<int, InsiderTransaction>, string>> InsiderTransactionsAsync(string symbol, string exchange, CancellationToken ct);
    }
}