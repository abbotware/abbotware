// -----------------------------------------------------------------------
// <copyright file="ILoad.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.RemoteOperations
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Loads data from the cache
    /// </summary>
    public interface ILoad
    {
        /// <summary>
        /// Loads the data from the cache
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>data</returns>
        Task LoadAsync(CancellationToken ct);
    }
}