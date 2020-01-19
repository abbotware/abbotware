// -----------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    /// <summary>
    /// Extensions methods for HttpRequestMessage
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        private enum HeaderType
        {
            Basic,
            Bearer,
        }

        /// <summary>
        /// Add Basic Auth authentication header
        /// </summary>
        /// <param name="message">http request message</param>
        /// <param name="credential">credential for basic auth</param>
        public static void AddBasicAuthentication(this HttpRequestMessage message, NetworkCredential credential)
        {
            message = Arguments.EnsureNotNull(message, nameof(message));
            credential = Arguments.EnsureNotNull(credential, nameof(credential));

            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{credential.UserName}:{credential.Password}"));

            AddAuthenticationHeader(message, HeaderType.Basic, encoded);
        }

        /// <summary>
        /// Add OAuth Bearer authentication header
        /// </summary>
        /// <param name="message">http request message</param>
        /// <param name="token">OAuth Access Token</param>
        public static void AddOAuthAuthentication(this HttpRequestMessage message, string token)
        {
            message = Arguments.EnsureNotNull(message, nameof(message));
            token = Arguments.EnsureNotNullOrWhitespace(token, nameof(token));

            AddAuthenticationHeader(message, HeaderType.Bearer, token);
        }

        private static void AddAuthenticationHeader(this HttpRequestMessage message, HeaderType type, string token)
        {
            if (message.Headers.Authorization != null)
            {
                throw new InvalidOperationException("message already has an authentication header");
            }

            message.Headers.Authorization = new AuthenticationHeaderValue(type.ToString(), token);
        }
    }
}