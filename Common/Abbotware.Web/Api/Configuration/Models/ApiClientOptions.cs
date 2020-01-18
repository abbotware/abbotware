// -----------------------------------------------------------------------
// <copyright file="ApiClientOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Web.Api.Configuration.Models
{
    using System;
    using Abbotware.Core.Serialization;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using Abbotware.Web.Api.Configuration;

    /// <summary>
    /// Configuration class for the Web API Client
    /// </summary>
    public class ApiClientOptions : IApiClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientOptions"/> class.
        /// </summary>
        /// <param name="baseUri">base uri for the web api</param>
        public ApiClientOptions(Uri baseUri)
        {
            this.BaseUri = baseUri;
        }

        /// <inheritdoc/>
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(100);

        /// <inheritdoc/>
        public IStringSerializaton Serializer { get; set; } = new NewtonsoftJsonSerializer();

        /// <inheritdoc/>
        public Uri BaseUri { get; set; }
    }
}