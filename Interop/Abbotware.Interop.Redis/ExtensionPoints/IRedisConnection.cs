// -----------------------------------------------------------------------
// <copyright file="IRedisConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.ExtensionPoints
{
    using Abbotware.Core.Cache;
    using Abbotware.Core.Cache.ExtensionPoints;
    using Abbotware.Core.Objects;
    using StackExchange.Redis;

    /// <summary>
    /// Interface for a native redis connection
    /// </summary>
    public interface IRedisConnection : IConnection
    {
        /// <summary>
        /// Gets the default redis database
        /// </summary>
        /// <returns>redis database</returns>
        IRemoteCache<IDatabase> GetDatabase();

        /// <summary>
        /// Gets the default redis database
        /// </summary>
        /// <param name="db">database number</param>
        /// <returns>redis database</returns>
        IRemoteCache<IDatabase> GetDatabase(int db);

        /// <summary>
        /// creates a peoperty set
        /// </summary>
        /// <param name="type">property set name</param>
        /// <param name="id">property set id</param>
        /// <returns>property set</returns>
        PropertySet CreatePropertySet(string type, string id);
    }
}