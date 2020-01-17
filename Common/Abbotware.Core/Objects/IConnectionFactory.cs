// -----------------------------------------------------------------------
// <copyright file="IConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;

    /// <summary>
    ///     factory interface for creating connections
    /// </summary>
    public interface IConnectionFactory : IDisposable
    {
        /// <summary>
        ///     creates a connection with the default config
        /// </summary>
        /// <returns>default configured connection</returns>
        IConnection Create();

        /// <summary>
        ///     creates a connection with the supplied config
        /// </summary>
        /// <param name="configuration">connection configuration</param>
        /// <returns>configured connection</returns>
        IConnection Create(IConnectionOptions configuration);

        /// <summary>
        ///     Releases a connection object
        /// </summary>
        /// <param name="connection">connection to Release</param>
        void Destroy(IConnection connection);
    }
}