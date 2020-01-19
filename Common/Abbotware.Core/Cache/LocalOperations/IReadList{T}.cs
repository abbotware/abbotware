// -----------------------------------------------------------------------
// <copyright file="IReadList{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.LocalOperations
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for reading a cache backed list
    /// </summary>
    /// <typeparam name="T">list element type</typeparam>
    public interface IReadList<T>
    {
        /// <summary>
        /// Gets the data as an Enumerable list
        /// </summary>
        /// <returns>list of data</returns>
        IEnumerable<T> AsEnumerable();
    }
}