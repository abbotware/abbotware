// -----------------------------------------------------------------------
// <copyright file="IRemoteCache{TNative}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    /// <summary>
    /// remote cache manager with access to native object
    /// </summary>
    /// <typeparam name="TNative">native object</typeparam>
    public interface IRemoteCache<TNative> : IRemoteCache
    {
        /// <summary>
        /// Gets the native cache interface
        /// </summary>
        TNative Native { get; }
    }
}