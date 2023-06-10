// -----------------------------------------------------------------------
// <copyright file="AuthenticatedWebComponent{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Web.Objects
{
    using Abbotware.Web.Api;
    using Abbotware.Web.Api.Configuration;
    using Abbotware.Web.Api.Plugins;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Component that uses a web client
    /// </summary>
    /// <typeparam name="TConfiguration">configuration</typeparam>
    public abstract class AuthenticatedWebComponent<TConfiguration> : WebComponent<TConfiguration>
        where TConfiguration : class, IApiClientOptions
    {
        private readonly string secret;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthenticatedWebComponent{TConfiguration}" /> class.
        /// </summary>
        /// <param name="secret">secret</param>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">injected logger</param>
        protected AuthenticatedWebComponent(string secret, TConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            this.secret = secret;
        }

        /// <summary>
        ///     Factory method to Create the web client
        /// </summary>
        /// <returns>client</returns>
        protected override IApiClient CreateWebClient()
        {
            return new AuthenticatedApiClient(this.secret, this.Configuration, this.Logger);
        }
    }
}