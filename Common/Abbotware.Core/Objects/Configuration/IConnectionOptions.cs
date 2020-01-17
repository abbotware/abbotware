// -----------------------------------------------------------------------
// <copyright file="IConnectionOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;
    using System.Net;

    /// <summary>
    /// read only connection options
    /// </summary>
    public interface IConnectionOptions
    {
        /// <summary>
        /// Gets the connection endpoint
        /// </summary>
        Uri Endpoint { get; }

        /// <summary>
        /// Gets the connection credential
        /// </summary>
        NetworkCredential? Credential { get; }
    }
}