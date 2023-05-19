// -----------------------------------------------------------------------
// <copyright file="RestResponse.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Net.Http
{
    using System.Net;

    /// <summary>
    /// REST response wrapper
    /// </summary>
    /// <param name="StatusCode">HTTP Status code</param>
    /// <param name="RawRequest">raw request</param>
    /// <param name="RawResponse">raw response</param>
    public abstract record RestResponse(HttpStatusCode StatusCode, string RawRequest, string? RawResponse)
    {
    }
}