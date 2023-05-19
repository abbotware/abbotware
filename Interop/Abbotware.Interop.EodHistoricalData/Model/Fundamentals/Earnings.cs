// -----------------------------------------------------------------------
// <copyright file="Earnings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.Collections.Generic;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundamental\Earnings POCO
    /// </summary>
    public record Earnings()
    {
        /// <summary>
        /// Gets the Earnings History data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, History>))]
        public IReadOnlyCollection<History>? History { get; init; }

        /// <summary>
        /// Gets the Earnings Trend data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, Trend>))]
        public IReadOnlyCollection<Trend>? Trend { get; init; }

        /// <summary>
        /// Gets the Annual Earnings data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, Annual>))]
        public IReadOnlyCollection<Annual>? Annual { get; init; }
    }
}