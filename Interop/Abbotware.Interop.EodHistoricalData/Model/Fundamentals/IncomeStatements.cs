// -----------------------------------------------------------------------
// <copyright file="IncomeStatements.cs" company="Abbotware, LLC">
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
    /// Income Statement POCO
    /// </summary>
    public record IncomeStatements(
        [property: MaxLength(3)]
        [property: JsonProperty(PropertyName = "currency_symbol")]
        string? CurrencySymbol)
    {
        /// <summary>
        /// Gets the income statement data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, IncomeStatement>))]
        public IReadOnlyCollection<IncomeStatement>? Quarterly { get; init; }

        /// <summary>
        /// Gets the yearly income statement data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, IncomeStatement>))]
        public IReadOnlyCollection<IncomeStatement>? Yearly { get; init; }
    }
}