// -----------------------------------------------------------------------
// <copyright file="IConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    ///     factory interface for creating IConnection objects
    /// </summary>
    /// <remarks>will use the default options unless the overloaded create method is called</remarks>
    public interface IConnectionFactory : IFactory<IConnection>
    {
        /// <summary>
        /// Gets the default connection options
        /// </summary>
        IConnectionOptions DefaultOptions { get; }

        /// <summary>
        ///     creates a connection with the supplied config
        /// </summary>
        /// <param name="configuration">connection configuration</param>
        /// <returns>configured connection</returns>
        IConnection Create(IConnectionOptions configuration);
    }
}