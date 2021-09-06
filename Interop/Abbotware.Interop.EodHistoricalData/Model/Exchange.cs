// -----------------------------------------------------------------------
// <copyright file="Exchange.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Exchange POCO
    /// </summary>
    /// <param name="Name">Exchange Name</param>
    /// <param name="Code">Exchange Code</param>
    /// <param name="OperatingMIC">Operating Market Identifer Code</param>
    /// <param name="Country">Country</param>
    /// <param name="Currency">Currency</param>
    public record Exchange(
        [property: MaxLength(50)] string Name,
        [property: Key, MaxLength(10)] string Code,
        [property: MaxLength(10)] string? OperatingMIC,
        [property: MaxLength(25)] string Country,
        [property: MaxLength(10)] string Currency)
    {
    }
}