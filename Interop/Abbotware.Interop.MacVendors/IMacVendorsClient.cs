// -----------------------------------------------------------------------
// <copyright file="IMacVendorsClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.MacVendors
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// interface for an api.macvendors.com client
    /// </summary>
    public interface IMacVendorsClient
    {
        /// <summary>
        /// looks up the mac address
        /// </summary>
        /// <param name="mac">mac address</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        Task<string> LookupAsync(string mac, CancellationToken ct);
    }
}