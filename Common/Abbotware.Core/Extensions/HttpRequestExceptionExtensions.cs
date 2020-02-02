// -----------------------------------------------------------------------
// <copyright file="HttpRequestExceptionExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Helper extension methods for HttpRequestException
    /// </summary>
    public static class HttpRequestExceptionExtensions
    {
        /// <summary>
        /// Gets the Http Status Code embedded inthe exception message since this was removed from the API
        /// </summary>
        /// <remarks>https://github.com/dotnet/runtime/issues/911</remarks>
        /// <param name="exception">exception to parse</param>
        /// <returns>http status code</returns>
        public static HttpStatusCode? StatusCode(this HttpRequestException exception)
        {
            exception = Arguments.EnsureNotNull(exception, nameof(exception));

#if NETSTANDARD2_0
            var text = exception.Message.Replace("Response status code does not indicate success: ", string.Empty);
#else
            var text = exception.Message.Replace("Response status code does not indicate success: ", string.Empty, StringComparison.InvariantCultureIgnoreCase);
#endif
            var code = text.Split(' ').FirstOrDefault();

            if (code.IsBlank())
            {
                return null;
            }

            if (!Enum.TryParse<HttpStatusCode>(code, out var parsed))
            {
                return null;
            }

            return parsed;
        }
    }
}
