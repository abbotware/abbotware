﻿// -----------------------------------------------------------------------
// <copyright file="MacVendorsClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.MacVendors.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Interop.MacVendors.Configuration;
    using Abbotware.Web.Api.Configuration.Models;
    using Abbotware.Web.Objects;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// MacVendors Client
    /// </summary>
    public class MacVendorsClient : WebComponent<ApiClientOptions>, IMacVendorsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MacVendorsClient"/> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        public MacVendorsClient(ILogger logger)
            : base(new ApiClientOptions(Defaults.Endpoint), logger)
        {
        }

        /// <inheritdoc/>
        public async Task<string> LookupAsync(string mac, CancellationToken ct)
        {
            this.InitializeIfRequired();

            try
            {
                var uri = new Uri(this.Configuration.BaseUri, mac);

                var data = await this.Client.GetAsync<string>(uri, ct)
                    .ConfigureAwait(false);

                return data;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode() == HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException($"can't find:{mac}", ex);
                }

                throw;
            }
        }
    }
}