// -----------------------------------------------------------------------
// <copyright file="IAsyncInitializable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for an initializable object
    /// </summary>
    public interface IAsyncInitializable
    {
        /// <summary>
        ///     Gets a value indicating whether or not object initialization has occurred
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Perform the intialization asynchronously
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task handle indicating whether or not initialization took place</returns>
        Task<bool> InitializeAsync(CancellationToken ct);
    }
}