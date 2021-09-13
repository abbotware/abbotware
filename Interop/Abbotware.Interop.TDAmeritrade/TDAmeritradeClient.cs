// -----------------------------------------------------------------------
// <copyright file="TDAmeritradeClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Net.Http;
    using Abbotware.Interop.RestSharp;
    using Abbotware.Interop.TDAmeritrade.Configuration;
    using Abbotware.Interop.TDAmeritrade.Models;
    using global::RestSharp;

    /// <summary>
    /// TD Ameritrade API Client
    /// </summary>
    public class TDAmeritradeClient : BaseRestClient<ITDAmeritradeSettings>
    {
#if NET5_0_OR_GREATER
        /// <summary>
        /// Initializes a new instance of the <see cref="TDAmeritradeClient"/> class.
        /// </summary>
        /// <param name="settings">api settings</param>
        /// <param name="logger">injected logger</param>
        public TDAmeritradeClient(ITDAmeritradeSettings settings, Microsoft.Extensions.Logging.ILogger<TDAmeritradeClient> logger)
            : this(settings, new LoggingAdapter(logger))
        {
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="TDAmeritradeClient"/> class.
        /// </summary>
        /// <param name="settings">api settings</param>
        /// <param name="logger">injectted logger</param>
        public TDAmeritradeClient(ITDAmeritradeSettings settings, ILogger logger)
            : base(new("https://api.tdameritrade.com/v1/"), settings, logger)
        {
        }

        /// <summary>
        /// Gets market hours for the specified markets for the current date
        /// </summary>
        /// <param name="markets">market types</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<IReadOnlyDictionary<string, IReadOnlyDictionary<string, MarketHours>>, ErrorResponse>> MarketHours(MarketType[] markets, CancellationToken ct)
        {
            return this.MarketHours(markets, null, ct);
        }

        /// <summary>
        /// Gets market hours for the specified markets
        /// </summary>
        /// <param name="markets">market types</param>
        /// <param name="date">date for hours</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<IReadOnlyDictionary<string, IReadOnlyDictionary<string, MarketHours>>, ErrorResponse>> MarketHours(MarketType[] markets, DateTime? date, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = new RestRequest("marketdata/hours", Method.GET, DataFormat.None);

            var m = string.Join(",", markets.Distinct().Select(x => EnumHelper.GetEnumMemberValue(x)));
            request.AddQueryParameter("markets", m, false);

            if (date != null)
            {
                request.AddQueryParameter("date", date.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), false);
            }

            return this.OnExecuteAsync<IReadOnlyDictionary<string, IReadOnlyDictionary<string, MarketHours>>, ErrorResponse>(request, ct);
        }

        /// <summary>
        /// Search or retrieve instrument data, including fundamental data
        /// </summary>
        /// <param name="symbol">Value to pass to the search.</param>
        /// <param name="searchType">The type of request</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<IDictionary<string, Instrument>, ErrorResponse>> SearchAsync(string symbol, SearchType searchType, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = new RestRequest("instruments", Method.GET, DataFormat.None);
            request.AddQueryParameter("symbol", symbol, false);
            request.AddQueryParameter("projection", EnumHelper.GetEnumMemberValue(searchType), false);

            return this.OnExecuteAsync<IDictionary<string, Instrument>, ErrorResponse>(request, ct);
        }

        /// <summary>
        /// Get an instrument by CUSIP
        /// </summary>
        /// <param name="cusip">cusip</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<Instrument, ErrorResponse>> InstrumentAsync(string cusip, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = new RestRequest("instruments/{cusip}", Method.GET, DataFormat.None);
            request.AddUrlSegment("cusip", cusip, false);

            return this.OnExecuteAsync<Instrument, ErrorResponse>(request, ct);
        }

        /// <summary>
        /// Search or retrieve instrument data, including fundamental data
        /// </summary>
        /// <param name="symbol">Value to pass to the search.</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public async Task<RestResponse<Instrument, ErrorResponse>> FundamentalDataAsync(string symbol, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var result = await this.SearchAsync(symbol, SearchType.Fundamental, ct)
                .ConfigureAwait(false);

            var data = result!.Response?.Values.FirstOrDefault();
            var code = result.StatusCode;
            var error = result.Error;

            if (data == null)
            {
                code = HttpStatusCode.NotFound;
                error = new ErrorResponse { Error = "Not Found" };
            }

            return new RestResponse<Instrument, ErrorResponse>(code, result.RawRequest, result.RawResponse)
                with
            { Response = data, Error = error };
        }

        /// <summary>
        /// Get Price History
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<CandleList, ErrorResponse>> PriceHistoryAsync(string symbol, CancellationToken ct)
        {
            return this.PriceHistoryAsync(symbol, null, null, null, null, false, ct);
        }

        /// <summary>
        /// Get Price History
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="range">History range</param>
        /// <param name="extendedHoursData">extended market hours data</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<CandleList, ErrorResponse>> PriceHistoryAsync(string symbol, History range, bool extendedHoursData, CancellationToken ct)
        {
            range = Arguments.EnsureNotNull(range, nameof(range));

            return this.PriceHistoryAsync(symbol, range.PeriodType, range.Period, range.FrequencyType, range.Frequency, extendedHoursData, ct);
        }

        /// <summary>
        /// Get Price History
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="periodType">period type</param>
        /// <param name="periods">period count</param>
        /// <param name="frequencyType">frequency type</param>
        /// <param name="frequency">frequency count</param>
        /// <param name="extendedHoursData">extended market hours data</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<CandleList, ErrorResponse>> PriceHistoryAsync(string symbol, PeriodType? periodType, ushort? periods, FrequencyType? frequencyType, ushort? frequency, bool extendedHoursData, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateBasePriceHistoryRequest(symbol, frequencyType, frequency, extendedHoursData);

            if (periodType != null)
            {
                request.AddQueryParameter("periodType", EnumHelper.GetEnumMemberValue(periodType));

                if (periods != null)
                {
                    request.AddQueryParameter("period", periods!.ToString()!);
                }
            }

            return this.OnExecuteAsync<CandleList, ErrorResponse>(request, ct);
        }

        /// <summary>
        /// Get Price History
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<CandleList, ErrorResponse>> PriceHistoryAsync(string symbol, DateTimeOffset startDate, DateTimeOffset? endDate, CancellationToken ct)
        {
            return this.PriceHistoryAsync(symbol, startDate, endDate, null, null, false, ct);
        }

        /// <summary>
        /// Get Price History
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <param name="frequencyType">frequency type</param>
        /// <param name="frequency">frequency count</param>
        /// <param name="extendedHoursData">extended market hours data</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        public Task<RestResponse<CandleList, ErrorResponse>> PriceHistoryAsync(string symbol, DateTimeOffset startDate, DateTimeOffset? endDate, FrequencyType? frequencyType, ushort? frequency, bool extendedHoursData, CancellationToken ct)
        {
            this.InitializeIfRequired();

            var request = CreateBasePriceHistoryRequest(symbol, frequencyType, frequency, extendedHoursData);

            request.AddQueryParameter("startDate", startDate.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture));

            if (endDate != null)
            {
                request.AddQueryParameter("endDate", endDate.Value.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture));
            }

            return this.OnExecuteAsync<CandleList, ErrorResponse>(request, ct);
        }

        /// <inheritdoc/>
        protected override void OnApplyAuthentication(RestRequest request)
        {
            request = Arguments.EnsureNotNull(request, nameof(request));

            // Use Bearer Token
            if (this.Configuration.BearerToken != null)
            {
                request.AddHeader("Authorization", "Bearer " + this.Configuration.BearerToken);
                return;
            }

            // else use query parameter
            base.OnApplyAuthentication(request);
        }

        private static RestRequest CreateBasePriceHistoryRequest(string symbol, FrequencyType? frequencyType, ushort? frequency, bool extendedHoursData)
        {
            var request = new RestRequest("marketdata/{symbol}/pricehistory", Method.GET, DataFormat.None);
            request.AddUrlSegment("symbol", symbol, false);

            if (frequencyType != null)
            {
                request.AddQueryParameter("frequencyType", EnumHelper.GetEnumMemberValue(frequencyType));

                if (frequency != null)
                {
                    request.AddQueryParameter("frequency", frequency.ToString()!);
                }
            }

            if (!extendedHoursData)
            {
                request.AddQueryParameter("needExtendedHoursData", extendedHoursData.ToString());
            }

            return request;
        }
    }
}
