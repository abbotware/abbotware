// -----------------------------------------------------------------------
// <copyright file="IGetInternetAddress.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// interface that can determine the external ip address
    /// </summary>
    public interface IGetInternetAddress : IDisposable
    {
        /// <summary>
        /// Gets the ip address
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task<IPAddress> GetInternetAddressAsync(CancellationToken ct);
    }
}