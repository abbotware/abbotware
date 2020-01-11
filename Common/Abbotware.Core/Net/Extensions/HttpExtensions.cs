// -----------------------------------------------------------------------
// <copyright file="HttpExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    using System;

    /// <summary>
    ///     Http Constants
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        ///     converts http method to string
        /// </summary>
        /// <param name="method">http method </param>
        /// <returns>string</returns>
        public static string ToString(this HttpMethod method)
        {
            return method switch
            {
                HttpMethod.Get => "GET",
                HttpMethod.Post => "POST",
                _ => throw new ArgumentException("Unknown Method"),
            };
        }

        /// <summary>
        ///     converts http accept to string
        /// </summary>
        /// <param name="accept">http accept</param>
        /// <returns>string</returns>
        public static string ToString(this HttpAccept accept)
        {
            return accept switch
            {
                HttpAccept.Csv => "text/csv",
                HttpAccept.Xml => "text/xml",
                HttpAccept.Json => "application/json",
                HttpAccept.Html => "text/html",
                _ => throw new ArgumentException("Unknown Accept"),
            };
        }

        /// <summary>
        ///     converts http contentType to string
        /// </summary>
        /// <param name="contentType">http contentType</param>
        /// <returns>string</returns>
        public static string ToString(this HttpContentType contentType)
        {
            return contentType switch
            {
                HttpContentType.Html => "text/html",
                HttpContentType.Form => "application/x-www-form-urlencoded",
                HttpContentType.Json => "application/json",
                _ => throw new ArgumentException("Unknown ContentType"),
            };
        }

        /// <summary>
        ///     Default file extensions for http accept type
        /// </summary>
        /// <param name="accept">http accept </param>
        /// <returns>file name extensions</returns>
        public static string DefaultFileExtension(this HttpAccept accept)
        {
            return accept switch
            {
                HttpAccept.Json => "json",
                HttpAccept.Csv => "csv",
                HttpAccept.Xml => "xml",
                HttpAccept.Html => "html",
                _ => "txt",
            };
        }
    }
}