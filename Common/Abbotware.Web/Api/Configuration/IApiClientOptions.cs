// -----------------------------------------------------------------------
// <copyright file="IApiClientOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Web.Api.Configuration
{
    using System;
    using Abbotware.Core.Serialization;

    /// <summary>
    /// Readonly Interface for the Web API Configuration class
    /// </summary>
    public interface IApiClientOptions
    {
        /// <summary>
        /// Gets the Request Timeout
        /// </summary>
        /// <remarks>
        /// Used for HttpClient.Timeout
        /// https://msdn.microsoft.com/en-us/library/system.net.http.httpclient.timeout%28v=vs.110%29.aspx
        /// </remarks>
        TimeSpan RequestTimeout { get; }

        /// <summary>
        /// Gets the object serializer
        /// </summary>
        IStringSerializaton Serializer { get; }

        /// <summary>
        /// Gets the Base Uri for web client
        /// </summary>
        Uri BaseUri { get; }
    }
}