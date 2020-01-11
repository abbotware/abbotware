// -----------------------------------------------------------------------
// <copyright file="HttpContentType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    /// <summary>
    ///     Http ContentType
    /// </summary>
    public enum HttpContentType
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
}