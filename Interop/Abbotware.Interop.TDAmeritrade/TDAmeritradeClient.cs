// -----------------------------------------------------------------------
// <copyright file="TDAmeritradeClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Web.Rest;
    using Abbotware.Interop.RestSharp;
    using Abbotware.Interop.TDAmeritrade.Configuration;
    using global::RestSharp;

    /// <summary>
    /// TD Ameritrade API Client
    /// </summary>
    public class TDAmeritradeClient : BaseRestClient<IApiSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TDAmeritradeClient"/> class.
        /// </summary>
        /// <param name="settings">api settings</param>
        /// <param name="logger">injectted logger</param>
        public TDAmeritradeClient(IApiSettings settings, ILogger logger)
            : base(new("https://api.tdameritrade.com/v1/"), settings, logger)
        {
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
        public Task<RestResponse<Instrument, ErrorResponse>> GetInstrumentAsync(string cusip, CancellationToken ct)
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

            return new(data, error, code);
        }

        /// <inheritdoc/>
        protected override void OnApplyAuthentication(RestRequest request)
        {
            request = Arguments.EnsureNotNull(request, nameof(request));

            if (this.Configuration.ApiKey != null)
            {
                request.AddQueryParameter("apikey", this.Configuration.ApiKey);
            }
            else
            {
                request.AddHeader("Authorization", "Bearer " + this.Configuration.BearerToken);
            }
        }
    }
}
