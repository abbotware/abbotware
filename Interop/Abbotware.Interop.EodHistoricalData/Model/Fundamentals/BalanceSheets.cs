// -----------------------------------------------------------------------
// <copyright file="BalanceSheets.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Balance Sheet POCO
    /// </summary>
    public record BalanceSheets(
        [property: JsonProperty(PropertyName = "currency_symbol")]
        [property: MaxLength(3)]
        string? CurrencySymbol)
    {
        /// <summary>
        /// Gets the quarterly balance sheet data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, BalanceSheet>))]
        public IReadOnlyCollection<BalanceSheet>? Quarterly { get; init; }

        /// <summary>
        /// Gets the yearly balance sheet data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, BalanceSheet>))]
        public IReadOnlyCollection<BalanceSheet>? Yearly { get; init; }
    }
}