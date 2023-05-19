// -----------------------------------------------------------------------
// <copyright file="CashFlows.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Cash Flow POCO
    /// </summary>
    public record CashFlows(
        [property: MaxLength(3)]
        [property: JsonProperty(PropertyName = "currency_symbol")]
        string? CurrencySymbol)
    {
        /// <summary>
        /// Gets the quarterly cash flow data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, CashFlow>))]
        public IReadOnlyCollection<CashFlow>? Quarterly { get; init; }

        /// <summary>
        /// Gets the yearly cash flow data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, CashFlow>))]
        public IReadOnlyCollection<CashFlow>? Yearly { get; init; }
    }
}