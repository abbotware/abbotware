// -----------------------------------------------------------------------
// <copyright file="IExchangeClient.cs" company="Abbotware, LLC">
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
    /// Exchange subset api
    /// </summary>
    public interface IExchangeClient
    {
        /// <summary>
        /// retrieve list of exchanges
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>list of exchanges</returns>
        Task<RestResponse<List<Exchange>, string>> ListAsync(CancellationToken ct);

        /// <summary>
        /// retrieve list of tickers for an exchange
        /// </summary>
        /// <param name="exchange">exchange code</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>list of exchanges</returns>
        Task<RestResponse<List<Ticker>, string>> TickersAsync(string exchange, CancellationToken ct);
    }
}