﻿// -----------------------------------------------------------------------
// <copyright file="BaseRestClient{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RestSharp
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Core.Web.Rest;
    using Abbotware.Interop.Newtonsoft;
    using global::RestSharp;
    using global::RestSharp.Serializers.NewtonsoftJson;

    /// <summary>
    /// Base class for RestSharp REST API clients
    /// </summary>
    /// <typeparam name="TConfiguration">configuration type</typeparam>
    public abstract class BaseRestClient<TConfiguration> : BaseComponent<TConfiguration>
        where TConfiguration : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRestClient{TConfiguration}"/> class.
        /// </summary>
        /// <param name="baseUri">base REST API Uri</param>
        /// <param name="configuration">configuration object</param>
        /// <param name="logger">injected logger</param>
        protected BaseRestClient(Uri baseUri, TConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            this.Client = new RestClient(baseUri);

            this.Client.UseNewtonsoftJson(JsonHelper.CreateDefaultSettings());
        }

        /// <summary>
        /// Gets the Rest Client
        /// </summary>
        protected RestClient Client { get; }

        /// <summary>
        /// Execute the request
        /// </summary>
        /// <typeparam name="TResponse">Request Object Type</typeparam>
        /// <typeparam name="TError">Error Object Type</typeparam>
        /// <param name="request">request configuration</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        protected async Task<RestResponse<TResponse, TError>> OnExecuteAsync<TResponse, TError>(RestRequest request, CancellationToken ct)
        {
            this.OnApplyAuthentication(request);

            var response = await this.Client.ExecuteAsync<TResponse>(request, ct)
                .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                var error = JsonHelper.FromString<TError>(response.Content);
                return new(error, response.StatusCode);
            }

            return new(response.Data, response.StatusCode);
        }

        /// <summary>
        /// Apply Authentication to the request
        /// </summary>
        /// <param name="request">request configuration</param>
        protected virtual void OnApplyAuthentication(RestRequest request)
        {
        }
    }
}