// -----------------------------------------------------------------------
// <copyright file="IConnectionFactory{TConnection,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    ///     factory interface for creating typed connections
    /// </summary>
    /// <typeparam name="TConnection">Connection Type</typeparam>
    /// <typeparam name="TOptions">Connection Options Type</typeparam>
    public interface IConnectionFactory<TConnection, TOptions>
        where TConnection : IConnection
    {
        /// <summary>
        /// Gets the default connection options
        /// </summary>
        TOptions DefaultOptions { get; }

        /// <summary>
        ///     creates a typed connection
        /// </summary>
        /// <returns>default configured connection</returns>
        TConnection Create();

        /// <summary>
        ///     creates a typed connection with the supplied config
        /// </summary>
        /// <param name="configuration">connection configuration</param>
        /// <returns>configured connection</returns>
        TConnection Create(TOptions configuration);

        /// <summary>
        ///     releases a typed connection
        /// </summary>
        /// <param name="connection">connection to Release</param>
        void Destroy(TConnection connection);
    }
}