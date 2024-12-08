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
    /// Fundamentals subset api
    /// </summary>
    public interface ISecApiMappingClient
    {
        /// <summary>
        /// retrieve Comapny Details via CUSIP
        /// </summary>
        /// <param name="cusip">CUSIP code</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>search result</returns>
        Task<RestResponse<CompanyDetails[], ErrorMessage>> CusipAsync(string cusip, CancellationToken ct);
    }
}