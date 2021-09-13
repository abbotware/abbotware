// -----------------------------------------------------------------------
// <copyright file="Exchange.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;
    using Abbotware.Interop.EodHistoricalData.Serialization;
    using global::Newtonsoft.Json;

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
        [property: MaxLength(Length.Country)] string Country,
        [property: JsonConverter(typeof(CurrencyTypeConverter))] CurrencyType Currency)
    {
    }
}