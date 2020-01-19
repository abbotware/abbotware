// -----------------------------------------------------------------------
// <copyright file="IpifyClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Ipify.Plugins
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Ipify.Configuration;
    using Abbotware.Web.Api.Configuration.Models;
    using Abbotware.Web.Objects;

    /// <summary>
    /// Ipify Client
    /// </summary>
    public class IpifyClient : WebComponent<ApiClientOptions>, IIpifyClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IpifyClient"/> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        public IpifyClient(ILogger logger)
            : base(new ApiClientOptions(Defaults.Endpoint), logger)
        {
        }

        /// <inheritdoc/>
        public async Task<IPAddress> GetInternetAddressAsync(CancellationToken ct)
        {
            this.InitializeIfRequired();

            var data = await this.Client.GetAsync<string>(this.Configuration.BaseUri, ct)
                .ConfigureAwait(false);

            return IPAddress.Parse(data);
        }
    }
}