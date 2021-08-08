// -----------------------------------------------------------------------
// <copyright file="UriExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extensions methods for Uri
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Append paths to uri
        /// </summary>
        /// <param name="uri">uri being extedded</param>
        /// <param name="paths">paths to append to uri</param>
        /// <returns>new uri with paths appeneded</returns>
        public static Uri Append(this Uri uri, params string[] paths)
        {
            uri = Arguments.EnsureNotNull(uri, nameof(uri));

            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format(CultureInfo.InvariantCulture, "{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }

        /// <summary>
        /// Sets query parameters on the uri using a name value collection
        /// </summary>
        /// <param name="uri">uri being extedded</param>
        /// <param name="urlParameters">query parameters</param>
        /// <returns>uri with query parameters</returns>
        public static Uri SetQueryParams(this Uri uri, NameValueCollection urlParameters)
        {
            uri = Arguments.EnsureNotNull(uri, nameof(uri));

            if (urlParameters == null)
            {
                return uri;
            }

            if (urlParameters.Count == 0)
            {
                return uri;
            }

            var keyValuePairs = new List<string>();

            var keys = urlParameters.AllKeys.Where(x => x != null).Cast<string>().ToArray();

            foreach (string key in keys)
            {
                keyValuePairs.Add($"{Uri.EscapeDataString(key!)}={Uri.EscapeDataString(urlParameters[key]!)}");
            }

            return new Uri($"{uri.AbsoluteUri}?{string.Join("&", keyValuePairs)}");
        }

        /// <summary>
        /// Sets query parameters on the uri using a string
        /// </summary>
        /// <param name="uri">uri being extedded</param>
        /// <param name="parameters">query parameters</param>
        /// <returns>uri with query parameters</returns>
        public static Uri SetQueryParams(this Uri uri, string parameters)
        {
            uri = Arguments.EnsureNotNull(uri, nameof(uri));

            if (string.IsNullOrWhiteSpace(parameters))
            {
                return uri;
            }

            return new Uri($"{uri.AbsoluteUri}?{parameters}");
        }

        /// <summary>
        /// Sets query parameters on the uri list of key value pairs
        /// </summary>
        /// <param name="uri">uri being extedded</param>
        /// <param name="urlParameters">query parameters</param>
        /// <returns>uri with query parameters</returns>
        public static Uri SetQueryParams(this Uri uri, IEnumerable<KeyValuePair<string, string>> urlParameters)
        {
            return uri.SetQueryParams(string.Join("&", urlParameters.Select(p => $"{p.Key}={p.Value}")));
        }
    }
}
