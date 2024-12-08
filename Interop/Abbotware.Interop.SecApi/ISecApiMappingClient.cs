// -----------------------------------------------------------------------
// <copyright file="ISecApiMappingClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SecApi
{
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Net.Http;
    using Abbotware.Interop.SecApi.Model;

    /// <summary>
    /// Mapping Subset the Sec API
    /// </summary>
    public interface ISecApiMappingClient
    {
        /// <summary>
        /// Get Company Details via CUSIP
        /// </summary>
        /// <param name="cusip">CUSIP code</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<CompanyDetails[], ErrorMessage>> CusipAsync(string cusip, CancellationToken ct);
    }
}