// -----------------------------------------------------------------------
// <copyright file="HttpAccept.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    /// <summary>
    ///     Http Accept
    /// </summary>
    public enum HttpAccept
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
}