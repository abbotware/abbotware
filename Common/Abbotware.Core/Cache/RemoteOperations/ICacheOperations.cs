// -----------------------------------------------------------------------
// <copyright file="ICacheOperations.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.RemoteOperations
{
    /// <summary>
    /// interrace for cache operations
    /// </summary>
    public interface ICacheOperations : ILoad, ISave
    {
        /// <summary>
        ///     Gets the key used in the remote store
        /// </summary>
        string RemoteKey { get; }
    }
}