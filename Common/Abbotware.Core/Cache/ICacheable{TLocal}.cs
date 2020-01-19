// -----------------------------------------------------------------------
// <copyright file="ICacheable{TLocal}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using Abbotware.Core.Cache.RemoteOperations;

    /// <summary>
    /// interface for a local and remote cache
    /// </summary>
    /// <typeparam name="TLocal">local type</typeparam>
    public interface ICacheable<TLocal> : ILocalAndRemote<TLocal, ICacheOperations>
    {
    }
}