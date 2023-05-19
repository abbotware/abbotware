// -----------------------------------------------------------------------
// <copyright file="OAuthToken.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Security
{
    using System.Net.Http.Headers;

    /// <summary>
    /// OAuth Token class
    /// </summary>
    public class OAuthToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthToken"/> class.
        /// </summary>
        /// <param name="token">bearer token</param>
        public OAuthToken(string token)
        {
            this.BearerToken = token;
        }

        /// <summary>
        /// Gets the bearer token
        /// </summary>
        public string BearerToken { get; }

        /// <summary>
        ///  Convert to object to an HTTP authorization header value
        /// </summary>
        /// <returns>http authorization header value</returns>
        public AuthenticationHeaderValue ToAuthorizaionHeader()
        {
            return new AuthenticationHeaderValue("Bearer", this.BearerToken);
        }
    }
}