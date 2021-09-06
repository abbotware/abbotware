// -----------------------------------------------------------------------
// <copyright file="Ticker.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Ticker POCO
    /// </summary>
    /// <param name="Name">Ticker Code</param>
    /// <param name="Code">Ticker Name</param>
    /// <param name="Country">Country</param>
    /// <param name="Exchange">Exchange Code</param>
    /// <param name="Currency">Currency</param>
    /// <param name="Type">Type</param>
    /// <param name="Isin">ISIN code</param>
    public record Ticker(
        [property: MaxLength(50)] string Code,
        [property: MaxLength(500)] string Name,
        [property: MaxLength(25)] string Country,
        [property: MaxLength(10)] string Exchange,
        [property: MaxLength(10)] string Currency,
        [JsonConverter(typeof(TickerTypeTypoFixer))] TickerType Type,
        [property: MaxLength(12)] string? Isin)
    {
    }
}