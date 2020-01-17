// -----------------------------------------------------------------------
// <copyright file="IWebApiClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// interface for a generic http client
    /// </summary>
    public interface IWebApiClient : IDisposable
    {
        /// <summary>
        /// Gets the config
        /// </summary>
        IWebApiClientOptions Configuration { get; }

        /// <summary>
        /// performs an HTTP Delete
        /// </summary>
        /// <param name="url">url </param>
        /// <returns>async task</returns>
        Task DeleteAsync(Uri url);

        /// <summary>
        /// performs an HTTP Delete
        /// </summary>
        /// <param name="url">request url</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task DeleteAsync(Uri url, CancellationToken ct);

        /// <summary>
        /// performs an HTTP Delete and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="url">request url</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> DeleteAsync<TResponse>(Uri url);

        /// <summary>
        /// performs an HTTP Delete and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="url">request url</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> DeleteAsync<TResponse>(Uri url, CancellationToken ct);

        /// <summary>
        /// performs an HTTP Get and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="url">request url</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> GetAsync<TResponse>(Uri url);

        /// <summary>
        /// performs an HTTP Get and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="url">request url</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> GetAsync<TResponse>(Uri url, CancellationToken ct);

        /// <summary>
        /// performs an HTTP Post with a request object serialized as the body and deserialized the response
        /// </summary>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="url">request url</param>
        /// <param name="request">request date</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(Uri url, TRequest request);

        /// <summary>
        /// performs an HTTP Post with a request object serialized as the body and deserialized the response
        /// </summary>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="url">request url</param>
        /// <param name="request">request date</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(Uri url, TRequest request, CancellationToken ct);

        /// <summary>
        /// Sends an HTTP raw request deserializes the response
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="request">raw request</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request);

        /// <summary>
        /// Sends an HTTP raw request deserializes the response
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="request">raw request</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>asyc task with response</returns>
        Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request, CancellationToken ct = default);
    }
}