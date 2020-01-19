// -----------------------------------------------------------------------
// <copyright file="ICacheableList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using Abbotware.Core.Cache.LocalOperations;

    /// <summary>
    /// interface for reading a cache backed list
    /// </summary>
    /// <typeparam name="T">list element type</typeparam>
    public interface ICacheableList<T> : ICacheable<IWriteList<T>>
    {
    }
}