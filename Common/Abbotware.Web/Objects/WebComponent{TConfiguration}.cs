// -----------------------------------------------------------------------
// <copyright file="WebComponent{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Web.Objects
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Web.Api;
    using Abbotware.Web.Api.Configuration;
    using Abbotware.Web.Api.Plugins;

    /// <summary>
    /// Component that uses a web client
    /// </summary>
    /// <typeparam name="TConfiguration">configuration</typeparam>
    public abstract class WebComponent<TConfiguration> : BaseComponent<TConfiguration>
        where TConfiguration : class, IApiClientOptions
    {
        private IApiClient? client;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebComponent{TConfig}"/> class.
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">injected logger</param>
        protected WebComponent(TConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
        }

        /// <summary>
        /// Gets the client
        /// </summary>
        protected IApiClient Client
        {
            get
            {
                this.InitializeIfRequired();

                return this.client!;
            }
        }

        /// <inheritdoc/>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.client = this.CreateWebClient();
        }

        /// <summary>
        /// Factory method to Create the web client
        /// </summary>
        /// <returns>client</returns>
        protected virtual IApiClient CreateWebClient()
        {
            return new ApiClient(this.Configuration, this.Logger);
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.client?.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}