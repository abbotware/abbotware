// -----------------------------------------------------------------------
// <copyright file="IConnectionFactory{TConnection,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;

    /// <summary>
    ///     factory interface for creating typed connections
    /// </summary>
    /// <remarks>will use the default options unless the overloaded create method is called</remarks>
    /// <typeparam name="TConnection">Connection Type</typeparam>
    /// <typeparam name="TOptions">Connection Options Type</typeparam>
    public interface IConnectionFactory<TConnection, TOptions> : IFactory<TConnection>, IDisposable
        where TConnection : IConnection
    {
        /// <summary>
        /// Gets the default connection options
        /// </summary>
        TOptions DefaultOptions { get; }

        /// <summary>
        ///     creates a typed connection with the supplied config
        /// </summary>
        /// <param name="configuration">connection configuration</param>
        /// <returns>configured connection</returns>
        TConnection Create(TOptions configuration);
    }
}