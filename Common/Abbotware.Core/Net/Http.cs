// -----------------------------------------------------------------------
// <copyright file="Http.cs" company="Abbotware, LLC">
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
#pragma warning disable CA1724 // Type names should not match namespaces
    public static class Http
#pragma warning restore CA1724 // Type names should not match namespaces
    {
        /// <summary>
        ///     Http Accept
        /// </summary>
        public enum Accept
        {
            /// <summary>
            ///     Http Accept for "text/csv"
            /// </summary>
            Csv,

            /// <summary>
            ///     Http Accept for "text/xml"
            /// </summary>
            Xml,

            /// <summary>
            ///     Http Accept for "application/json""
            /// </summary>
            Json,

            /// <summary>
            ///     Http Accept for "text/html"
            /// </summary>
            Html,
        }

        /// <summary>
        ///     Http ContentType
        /// </summary>
        public enum ContentType
        {
            /// <summary>
            ///     Http ContentType for "text/html"
            /// </summary>
            Html,

            /// <summary>
            ///     Http ContentType for "application/x-www-form-urlencoded"
            /// </summary>
            Form,

            /// <summary>
            ///     Http ContentType for "application/json"
            /// </summary>
            Json,
        }

        /// <summary>
        ///     Http Method
        /// </summary>
        public enum Method
        {
            /// <summary>
            ///     Http method 'GET'
            /// </summary>
            Get,

            /// <summary>
            ///     Http method 'POST'
            /// </summary>
            Post,
        }

        /// <summary>
        ///     converts http method to string
        /// </summary>
        /// <param name="method">http method </param>
        /// <returns>string</returns>
        public static string ToString(Method method)
        {
            return method switch
            {
                Method.Get => "GET",
                Method.Post => "POST",
                _ => throw new ArgumentException("Unknown Method"),
            };
        }

        /// <summary>
        ///     converts http accept to string
        /// </summary>
        /// <param name="accept">http accept</param>
        /// <returns>string</returns>
        public static string ToString(Accept accept)
        {
            return accept switch
            {
                Accept.Csv => "text/csv",
                Accept.Xml => "text/xml",
                Accept.Json => "application/json",
                Accept.Html => "text/html",
                _ => throw new ArgumentException("Unknown Accept"),
            };
        }

        /// <summary>
        ///     converts http contentType to string
        /// </summary>
        /// <param name="contentType">http contentType</param>
        /// <returns>string</returns>
        public static string ToString(ContentType contentType)
        {
            return contentType switch
            {
                ContentType.Html => "text/html",
                ContentType.Form => "application/x-www-form-urlencoded",
                ContentType.Json => "application/json",
                _ => throw new ArgumentException("Unknown ContentType"),
            };
        }

        /// <summary>
        ///     Default file extensions for http accept type
        /// </summary>
        /// <param name="accept">http accept </param>
        /// <returns>file name extensions</returns>
        public static string DefaultFileExtension(Accept accept)
        {
            return accept switch
            {
                Accept.Json => "json",
                Accept.Csv => "csv",
                Accept.Xml => "xml",
                Accept.Html => "html",
                _ => "txt",
            };
        }
    }
}