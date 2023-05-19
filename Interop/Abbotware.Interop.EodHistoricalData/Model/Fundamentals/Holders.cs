// -----------------------------------------------------------------------
// <copyright file="Holders.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.Collections.Generic;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundalmental\Holders POCO
    /// </summary>
    public record Holders()
    {
        /// <summary>
        /// Gets the Institution Holders
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, Holder>))]
        public IReadOnlyCollection<Holder>? Institutions { get; init; }

        /// <summary>
        /// Gets the Fund Holders
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, Holder>))]
        public IReadOnlyCollection<Holder>? Funds { get; init; }
    }
}