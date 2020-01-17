// -----------------------------------------------------------------------
// <copyright file="AuthenticatedWebApiClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net.Plugins
{
    using System;
    using System.Net.Http;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;

    /// <summary>
    /// Authenticated Web Api Client
    /// </summary>
    public class AuthenticatedWebApiClient : WebApiClient
    {
        private readonly string secret;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedWebApiClient"/> class.
        /// </summary>
        /// <param name="secret">secret</param>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">injected logger</param>
        public AuthenticatedWebApiClient(string secret, IWebApiClientOptions configuration, ILogger logger)
            : base(configuration, logger)
        {
            this.secret = secret;
        }

        /// <inheritdoc/>
        protected override HttpRequestMessage OnCreateRequest(HttpMethod method, Uri url, HttpContent? content)
        {
            var request = base.OnCreateRequest(method, url, content);
            request.AddOAuthAuthentication(this.secret);

            return request;
        }
    }
}