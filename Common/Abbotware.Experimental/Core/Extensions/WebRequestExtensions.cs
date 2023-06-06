// -----------------------------------------------------------------------
// <copyright file="WebRequestExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     WebRequest Extensions methods
    /// </summary>
    public static class WebRequestExtensions
    {
        /// <summary>
        ///     reads the entire http request and returns a string
        /// </summary>
        /// <param name="request">http web request object</param>
        /// <returns>contents of response</returns>
        [Obsolete("use async variant")]
        public static string ReadToEnd(this WebRequest request)
        {
            Arguments.NotNull(request, nameof(request));

            using var response = request.GetResponse();

            using var responseStream = response.GetResponseStream();
            using var bufferedStream = new BufferedStream(responseStream, 1024 * 16);
            using var reader = new StreamReader(bufferedStream);

            return reader.ReadToEnd();
        }

        /// <summary>
        ///     reads the entire http request and returns a string
        /// </summary>
        /// <param name="request">http web request object</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>contents of response</returns>
        public static async Task<string> ReadToEndAsync(this WebRequest request, CancellationToken ct)
        {
            Arguments.NotNull(request, nameof(request));

            using var response = await request.GetResponseAsync(ct).ConfigureAwait(false);
            using var responseStream = response.GetResponseStream();
            using var bufferedStream = new BufferedStream(responseStream, 1024 * 16);
            using var reader = new StreamReader(bufferedStream);

            return await reader.ReadToEndAsync(ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the Response with cancellation wired up to abort
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>web response</returns>
        public static async Task<WebResponse> GetResponseAsync(this WebRequest request, CancellationToken ct)
        {
            Arguments.NotNull(request, nameof(request));

            using (ct.Register(() => request.Abort(), useSynchronizationContext: false))
            {
                try
                {
                    var response = await request.GetResponseAsync()
                        .ConfigureAwait(false);

                    return response;
                }
                catch (WebException ex)
                {
                    // WebException is thrown when request.Abort() is called,
                    // but there may be many other reasons,
                    // propagate the WebException to the caller correctly
                    if (ct.IsCancellationRequested)
                    {
                        // the WebException will be available as Exception.InnerException
                        throw new OperationCanceledException(ex.Message, ex, ct);
                    }

                    // cancellation hasn't been requested, rethrow the original WebException
                    throw;
                }
            }
        }
    }
}