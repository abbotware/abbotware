// -----------------------------------------------------------------------
// <copyright file="SecApiClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SecApi;

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
/// <remarks>
/// Initializes a new instance of the <see cref="SecApiClient"/> class.
/// </remarks>
/// <param name="settings">api settings</param>
/// <param name="logger">injected logger</param>
public class SecApiClient(ISecApiSettings settings, ILogger logger)
    : BaseRestClient<ISecApiSettings>(new("https://api.sec-api.io/"), settings, logger),
    ISecApiMappingClient
{
    /// <summary>
    /// Gets the Mapping api subset
    /// </summary>
    public ISecApiMappingClient Mapping => this;

    /// <inheritdoc/>
    public async Task<RestResponse<CompanyDetails[], ErrorMessage>> CusipAsync(string cusip, CancellationToken ct)
    {
        _ = this.InitializeIfRequired();

        var request = new RestRequest("mapping/cusip/{CUSIP}", Method.Get)
            .AddUrlSegment("CUSIP", cusip);

        return await this.OnExecuteAsync<CompanyDetails[], ErrorMessage>(request, ct)
            .ConfigureAwait(false);
    }
}