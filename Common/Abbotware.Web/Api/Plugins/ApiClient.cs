// -----------------------------------------------------------------------
// <copyright file="ApiClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Web.Api.Plugins
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Web.Api.Configuration;

    /// <summary>
    ///     Web Api Client
    /// </summary>
    public class ApiClient : BaseComponent<IApiClientOptions>, IApiClient
    {
        private readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="logger">injected logger</param>
        public ApiClient(IApiClientOptions configuration, ILogger logger)
            : base(configuration, logger)
        {
            this.httpClient.Timeout = this.Configuration.RequestTimeout;
        }

        /// <inheritdoc />
        public Task<TResponse> GetAsync<TResponse>(Uri url)
        {
            return this.GetAsync<TResponse>(url, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TResponse> GetAsync<TResponse>(Uri url, CancellationToken ct)
        {
            using var request = this.OnCreateRequest(HttpMethod.Get, url);

            return await this.OnExecuteAsync<TResponse>(request, ct)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<TResponse> PostAsync<TRequest, TResponse>(Uri url, TRequest request)
        {
            return this.PostAsync<TRequest, TResponse>(url, request, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TResponse> PostAsync<TRequest, TResponse>(Uri url, TRequest request, CancellationToken ct)
        {
            // This works as expected when the user sets the TRequest to HttpRequestMessage
            if (typeof(TRequest) == typeof(HttpRequestMessage))
            {
                Arguments.NotNull(request, nameof(request));

                // Caller is now responsible for disposing
                var httpRequest = (HttpRequestMessage)(object)request!;

                return await this.OnExecuteAsync<TResponse>(httpRequest, ct)
                    .ConfigureAwait(false);
            }

            using (var httpRequest = this.OnCreateRequest(HttpMethod.Post, url, request))
            {
                return await this.OnExecuteAsync<TResponse>(httpRequest, ct)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public Task DeleteAsync(Uri url)
        {
            return this.DeleteAsync(url, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Uri url, CancellationToken ct)
        {
            using var request = this.OnCreateRequest(HttpMethod.Delete, url);
            using var response = await this.OnInnerExecuteAsync(request, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<TResponse> DeleteAsync<TResponse>(Uri url)
        {
            return this.DeleteAsync<TResponse>(url, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TResponse> DeleteAsync<TResponse>(Uri url, CancellationToken ct)
        {
            using var request = this.OnCreateRequest(HttpMethod.Delete, url);

            return await this.OnExecuteAsync<TResponse>(request, ct)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request)
        {
            return this.SendAsync<TResponse>(request, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request, CancellationToken ct)
        {
            return this.OnExecuteAsync<TResponse>(request, ct);
        }

        /// <summary>
        ///     Sends an generic HTTP Request and converts the response to a C# object
        /// </summary>
        /// <typeparam name="TResponse">response object type</typeparam>
        /// <param name="request">request data</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>task for response</returns>
        protected virtual async Task<TResponse> OnExecuteAsync<TResponse>(HttpRequestMessage request, CancellationToken ct)
        {
            // This works as expected when the user sets the TResponse to HttpResponseMessage
            if (typeof(TResponse) == typeof(HttpResponseMessage))
            {
                // Caller is now responsible for disposing
                var response = await this.OnInnerExecuteAsync(request, ct)
                    .ConfigureAwait(false);

                return (TResponse)(object)response;
            }

            using (var response = await this.OnInnerExecuteAsync(request, ct)
                .ConfigureAwait(false))
            {
                return await this.OnCreateResponseAsync<TResponse>(response)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     factory method to creates an http request message
        /// </summary>
        /// <param name="method">http method type</param>
        /// <param name="url">request url</param>
        /// <returns>constructed http request message</returns>
        protected virtual HttpRequestMessage OnCreateRequest(HttpMethod method, Uri url)
        {
            return this.OnCreateRequest(method, url, null);
        }

        /// <summary>
        ///     factory method to creates an http request message and encode an object into the body
        /// </summary>
        /// <typeparam name="TRequest">request object type</typeparam>
        /// <param name="method">http method type</param>
        /// <param name="url">request url</param>
        /// <param name="request">request data</param>
        /// <returns>constructed http request message</returns>
        protected virtual HttpRequestMessage OnCreateRequest<TRequest>(HttpMethod method, Uri url, TRequest request)
        {
            var serializer = this.Configuration.Serializer;
            var s = serializer.Encode(request);

            var content = new StringContent(s, serializer.Encoding, serializer.MimeType);

            return this.OnCreateRequest(method, url, (HttpContent)content);
        }

        /// <summary>
        ///     factory method to creates an http request message
        /// </summary>
        /// <param name="method">http method type</param>
        /// <param name="url">request url</param>
        /// <param name="content">content</param>
        /// <returns>constructed http request message</returns>
        protected virtual HttpRequestMessage OnCreateRequest(HttpMethod method, Uri url, HttpContent? content)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = content,
            };

            return request;
        }

        /// <summary>
        ///     factory method to desrialize an TResponse out of the  HTTP Response message
        /// </summary>
        /// <typeparam name="TResponse">response object type</typeparam>
        /// <param name="response">request data</param>
        /// <returns>desrialized object</returns>
        protected virtual async Task<TResponse> OnCreateResponseAsync<TResponse>(HttpResponseMessage response)
        {
            response = Arguments.EnsureNotNull(response, nameof(response));

            var content = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            if (typeof(TResponse) == typeof(string))
            {
                return (TResponse)(object)content;
            }

            return this.Configuration.Serializer.Decode<TResponse>(content);
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.httpClient?.Dispose();

            base.OnDisposeManagedResources();
        }

        /// <summary>
        ///     Sends an generic HTTP Request
        /// </summary>
        /// <param name="request">request data</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>task for response</returns>
        private async Task<HttpResponseMessage> OnInnerExecuteAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var response = await this.httpClient.SendAsync(request, ct)
                .ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.ProxyAuthenticationRequired)
            {
                response.Dispose();

                throw new AuthenticationException("Proxy Authentication Required");
            }

            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}