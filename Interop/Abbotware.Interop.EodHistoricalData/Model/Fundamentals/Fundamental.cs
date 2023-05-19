// -----------------------------------------------------------------------
// <copyright file="Fundamental.cs" company="Abbotware, LLC">
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
    /// Fundamental POCO
    /// </summary>
    public record Fundamental()
    {
        /// <summary>
        /// Gets the general data for the instrument
        /// </summary>
        public General? General { get; init; }

        /// <summary>
        /// Gets the highlights for the instrument
        /// </summary>
        public Highlights? Highlights { get; init; }

        /// <summary>
        /// Gets the Valuation for the instrument
        /// </summary>
        public Valuation? Valuation { get; init; }

        /// <summary>
        /// Gets the earnings for the instrument
        /// </summary>
        public SharesStats? SharesStats { get; init; }

        /// <summary>
        /// Gets the technicals for the instrument
        /// </summary>
        public Technicals? Technicals { get; init; }

        /// <summary>
        /// Gets the splits and dividends for the instrument
        /// </summary>
        public SplitsDividends? SplitsDividends { get; init; }

        /// <summary>
        /// Gets the analyst ratings for the instrument
        /// </summary>
        public AnalystRatings? AnalystRatings { get; init; }

        /// <summary>
        /// Gets the holders for the instrument
        /// </summary>
        public Holders? Holders { get; init; }

        /// <summary>
        /// Gets the Insider Transaction
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, InsiderTransaction>))]
        public IReadOnlyCollection<InsiderTransaction>? InsiderTransactions { get; init; }

        /// <summary>
        /// Gets the ESG Scores for the instrument
        /// </summary>
        [property: JsonProperty(PropertyName = "ESGScores")]
        public EsgScores? EsgScores { get; init; }

        /// <summary>
        /// Gets the Outstanding Shares for the instrument
        /// </summary>
        public OutstandingShares? OutstandingShares { get; init; }

        /// <summary>
        /// Gets the earnings for the instrument
        /// </summary>
        public Earnings? Earnings { get; init; }

        /// <summary>
        /// Gets the financials for the instrument
        /// </summary>
        public Financials? Financials { get; init; }

        /// <summary>
        /// Gets the Etf Data for the instrument
        /// </summary>
        [property: JsonProperty(PropertyName = "ETF_Data")]
        public EtfData? EtfData { get; init; }

        /// <summary>
        /// Gets the Statistics Data for the instrument
        /// </summary>
        public Statistics? Statistics { get; init; }

        /// <summary>
        /// Gets the Mutual Fund Data for the instrument
        /// </summary>
        [property: JsonProperty(PropertyName = "MutualFund_Data")]
        public MutualFundData? MutualFundData { get; init; }

        /// <summary>
        /// Gets the Components Data for the instrument
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, Component>))]
        public IReadOnlyCollection<Component>? Components { get; init; }
    }
}
