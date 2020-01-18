// -----------------------------------------------------------------------
// <copyright file="IApiClientExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Web.Api;

    /// <summary>
    /// Provides extension methods for <see cref="IApiClient"/>
    /// </summary>
    public static class IApiClientExtensions
    {
        /// <summary>
        /// performs an HTTP Delete
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        public static void Delete(
            this IApiClient client,
            Uri uri)
        {
            client.Delete(uri, Timeout.InfiniteTimeSpan, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Delete
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public static void Delete(
            this IApiClient client,
            Uri uri,
            CancellationToken cancellationToken)
        {
            client.Delete(uri, Timeout.InfiniteTimeSpan, cancellationToken);
        }

        /// <summary>
        /// performs an HTTP Delete
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="timeout">call timeout</param>
        public static void Delete(
            this IApiClient client,
            Uri uri,
            TimeSpan timeout)
        {
            client.Delete(uri, timeout, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Delete
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="timeout">call timeout</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public static void Delete(
            this IApiClient client,
            Uri uri,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            client = Arguments.EnsureNotNull(client, nameof(client));
            Arguments.NotNull(uri, nameof(uri));

            client.DeleteAsync(uri, cancellationToken).Wait(timeout);
        }

        /// <summary>
        /// performs an HTTP Delete and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <returns>response object</returns>
        public static TResponse Delete<TResponse>(
            this IApiClient client,
            Uri uri)
        {
            return client.Delete<TResponse>(uri, Timeout.InfiniteTimeSpan, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Delete and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="timeout">call timeout</param>
        /// <returns>response object</returns>
        public static TResponse Delete<TResponse>(
            this IApiClient client,
            Uri uri,
            TimeSpan timeout)
        {
            return client.Delete<TResponse>(uri, timeout, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Delete and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>response object</returns>
        public static TResponse Delete<TResponse>(
            this IApiClient client,
            Uri uri,
            CancellationToken cancellationToken)
        {
            Arguments.NotNull(client, nameof(client));

            return client.Delete<TResponse>(uri, Timeout.InfiniteTimeSpan, cancellationToken);
        }

        /// <summary>
        /// performs an HTTP Delete and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="timeout">call timeout</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>response object</returns>
        public static TResponse Delete<TResponse>(
            this IApiClient client,
            Uri uri,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            client = Arguments.EnsureNotNull(client, nameof(client));
            Arguments.NotNull(uri, nameof(uri));

            var task = client.DeleteAsync<TResponse>(uri, cancellationToken);

            task.Wait(timeout);

            return task.Result;
        }

        /// <summary>
        /// performs an HTTP Get and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <returns>response object</returns>
        public static TResponse Get<TResponse>(
            this IApiClient client,
            Uri uri)
        {
            Arguments.NotNull(client, nameof(client));

            return client.Get<TResponse>(uri, Timeout.InfiniteTimeSpan, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Get and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>response object</returns>
        public static TResponse Get<TResponse>(
            this IApiClient client,
            Uri uri,
            CancellationToken cancellationToken)
        {
            Arguments.NotNull(client, nameof(client));

            return client.Get<TResponse>(uri, Timeout.InfiniteTimeSpan, cancellationToken);
        }

        /// <summary>
        /// performs an HTTP Get and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="timeout">call timeout</param>
        /// <returns>response object</returns>
        public static TResponse Get<TResponse>(
            this IApiClient client,
            Uri uri,
            TimeSpan timeout)
        {
            Arguments.NotNull(client, nameof(client));

            return client.Get<TResponse>(uri, timeout, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Get and deserializes the response to an object
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="timeout">call timeout</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>response object</returns>
        public static TResponse Get<TResponse>(
            this IApiClient client,
            Uri uri,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            client = Arguments.EnsureNotNull(client, nameof(client));
            Arguments.NotNull(uri, nameof(uri));

            var task = client.GetAsync<TResponse>(uri, cancellationToken);

            task.Wait(timeout);

            return task.Result;
        }

        /// <summary>
        /// performs an HTTP Post with a request object serialized as the body and deserialized the response
        /// </summary>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="request">request date</param>
        /// <returns>response object</returns>
        public static TResponse Post<TRequest, TResponse>(
            this IApiClient client,
            Uri uri,
            TRequest request)
        {
            return client.Post<TRequest, TResponse>(uri, request, Timeout.InfiniteTimeSpan, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Post with a request object serialized as the body and deserialized the response
        /// </summary>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="request">request date</param>
        /// <param name="timeout">call timeout</param>
        /// <returns>response object</returns>
        public static TResponse Post<TRequest, TResponse>(
            this IApiClient client,
            Uri uri,
            TRequest request,
            TimeSpan timeout)
        {
            return client.Post<TRequest, TResponse>(uri, request, timeout, CancellationToken.None);
        }

        /// <summary>
        /// performs an HTTP Post with a request object serialized as the body and deserialized the response
        /// </summary>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="request">request date</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>response object</returns>
        public static TResponse Post<TRequest, TResponse>(
            this IApiClient client,
            Uri uri,
            TRequest request,
            CancellationToken cancellationToken)
        {
            return client.Post<TRequest, TResponse>(uri, request, Timeout.InfiniteTimeSpan, cancellationToken);
        }

        /// <summary>
        /// performs an HTTP Post with a request object serialized as the body and deserialized the response
        /// </summary>
        /// <typeparam name="TRequest">request type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="client">client</param>
        /// <param name="uri">uri</param>
        /// <param name="request">request date</param>
        /// <param name="timeout">call timeout</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>response object</returns>
        public static TResponse Post<TRequest, TResponse>(
            this IApiClient client,
            Uri uri,
            TRequest request,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            client = Arguments.EnsureNotNull(client, nameof(client));
            Arguments.NotNull(uri, nameof(uri));

            var task = client.PostAsync<TRequest, TResponse>(uri, request, cancellationToken);

            task.Wait(timeout);

            return task.Result;
        }

        /// <summary>
        /// Performs an HTTP Get
        /// </summary>
        /// <typeparam name="TResponse">method return type</typeparam>
        /// <param name="client">client</param>
        /// <param name="action">request route action</param>
        /// <param name="id">request route id</param>
        /// <returns>data of type T</returns>
        public static Task<TResponse> RestGetAsync<TResponse>(this IApiClient client, string action, string id)
            where TResponse : new()
        {
            client = Arguments.EnsureNotNull(client, nameof(client));
            id = Arguments.EnsureNotNull(id, nameof(id));

            var uri = client.Configuration.BaseUri.Append(action, id.ToString(CultureInfo.InvariantCulture));

            return client.GetAsync<TResponse>(uri);
        }

        /// <summary>
        /// Performs an HTTP Get
        /// </summary>
        /// <typeparam name="TRequest">method request type</typeparam>
        /// <typeparam name="TResponse">method return type</typeparam>
        /// <param name="client">client</param>
        /// <param name="action">request route action</param>
        /// <param name="request">request object data</param>
        /// <returns>data of type T</returns>
        public static Task<TResponse> RestPostAsync<TRequest, TResponse>(this IApiClient client, string action, TRequest request)
            where TResponse : new()
        {
            client = Arguments.EnsureNotNull(client, nameof(client));

            var uri = client.Configuration.BaseUri.Append(action);

            return client.PostAsync<TRequest, TResponse>(uri, request);
        }
    }
}