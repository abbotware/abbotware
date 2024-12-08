// -----------------------------------------------------------------------
// <copyright file="SecApiClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SecApi
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Net.Http;
    using Abbotware.Interop.RestSharp;
    using Abbotware.Interop.SecApi.Configuration;
    using Abbotware.Interop.SecApi.Model;
    using global::RestSharp;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Sec Api Client
    /// </summary>
    public sealed class SecApiClient : BaseRestClient<ISecApiSettings>, ISecApiMappingClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecApiClient"/> class.
        /// </summary>
        /// <param name="settings">api settings</param>
        /// <param name="logger">injected logger</param>
        public SecApiClient(ISecApiSettings settings, ILogger logger)
            : base(new("https://api.sec-api.io/"), settings, logger)
        {
        }

        /// <summary>
        /// Gets the Mapping api subset
        /// </summary>
        public ISecApiMappingClient Mapping => this;

        /// <inheritdoc/>
        async Task<RestResponse<CompanyDetails[], ErrorMessage>> ISecApiMappingClient.CusipAsync(string cusip, CancellationToken ct)
        {
            _ = this.InitializeIfRequired();

            var request = new RestRequest("mapping/cusip/{CUSIP}", Method.Get);
            _ = request.AddUrlSegment("CUSIP", cusip);

            return await this.OnExecuteAsync<CompanyDetails[], ErrorMessage>(request, ct)
                .ConfigureAwait(false);
        }
    }
}