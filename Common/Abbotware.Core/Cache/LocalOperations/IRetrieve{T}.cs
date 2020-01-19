// -----------------------------------------------------------------------
// <copyright file="IRetrieve{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.LocalOperations
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// retrives from the remote cache
    /// </summary>
    /// <typeparam name="T">data type</typeparam>
    public interface IRetrieve<T>
    {
        /// <summary>
        /// Loads the data from the cache
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>data</returns>
        Task<T> RetrieveAsync(CancellationToken ct);
    }
}