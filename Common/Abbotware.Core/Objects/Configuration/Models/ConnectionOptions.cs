﻿// -----------------------------------------------------------------------
// <copyright file="ConnectionOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Objects.Configuration.Models
{
    using System;
    using System.Net;

    /// <summary>
    /// Connection Options class
    /// </summary>
    public class ConnectionOptions : IConnectionOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionOptions"/> class.
        /// </summary>
        /// <param name="endpoint">connection endpoint</param>
        public ConnectionOptions(Uri endpoint)
            : this(endpoint, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionOptions"/> class.
        /// </summary>
        /// <param name="endpoint">connection endpoint</param>
        /// <param name="credential">optional connection credentials</param>
        public ConnectionOptions(Uri endpoint, NetworkCredential? credential)
        {
            endpoint = Arguments.EnsureNotNull(endpoint, nameof(endpoint));

            this.Endpoint = endpoint;
            this.Credential = credential;
        }

        /// <inheritdoc/>
        public Uri Endpoint { get; }

        /// <inheritdoc/>
        public NetworkCredential? Credential { get; }
    }
}