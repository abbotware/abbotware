// -----------------------------------------------------------------------
// <copyright file="ICacheableCategorizedFieldValues.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using Abbotware.Core.Cache.LocalOperations;

    /// <summary>
    /// interface for reading a cache backed list
    /// </summary>
    public interface ICacheableCategorizedFieldValues : ICacheable<ICategorizedFieldValues>
    {
    }
}