// -----------------------------------------------------------------------
// <copyright file="OutstandingShares.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.Collections.Generic;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundalmental\OutstandingShares POCO
    /// </summary>
    public record OutstandingShares()
    {
        /// <summary>
        /// Gets the quarterly cash flow data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, OutstandingShare>))]
        public IReadOnlyCollection<OutstandingShare>? Annual { get; init; }

        /// <summary>
        /// Gets the yearly cash flow data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, OutstandingShare>))]
        public IReadOnlyCollection<OutstandingShare>? Quarterly { get; init; }
    }
}