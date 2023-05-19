// -----------------------------------------------------------------------
// <copyright file="ISave.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.RemoteOperations
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Saves data to the cache
    /// </summary>
    public interface ISave
    {
        /// <summary>
        /// Saves any changes to the backing cache store
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async handle</returns>
        Task SaveAsync(CancellationToken ct);
    }
}