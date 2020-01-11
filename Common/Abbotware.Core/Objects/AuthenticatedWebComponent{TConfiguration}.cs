// -----------------------------------------------------------------------
// <copyright file="AuthenticatedWebComponent{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Net;
    using Abbotware.Core.Net.Plugins;

    /// <summary>
    ///     Component that uses a web client
    /// </summary>
    /// <typeparam name="TConfiguration">configuration</typeparam>
    public abstract class AuthenticatedWebComponent<TConfiguration> : WebComponent<TConfiguration>
        where TConfiguration : class, IWebApiClientConfiguration
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
        protected override IWebApiClient CreateWebClient()
        {
            return new AuthenticatedWebApiClient(this.secret, this.Configuration, this.Logger);
        }
    }
}