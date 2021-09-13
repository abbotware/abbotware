// -----------------------------------------------------------------------
// <copyright file="EsgScores.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundalmental\ESGScores POCO
    /// </summary>
    public record EsgScores(
        DateTimeOffset? RatingDate,
        double? TotalEsg,
        double? TotalEsgPercentile,
        double? EnvironmentScore,
        double? EnvironmentScorePercentile,
        double? SocialScore,
        double? SocialScorePercentile,
        double? GovernanceScore,
        double? GovernanceScorePercentile,
        double? ControversyLevel)
    {
        /// <summary>
        /// Gets the Dislcaimer message
        /// </summary>
        [NotMapped]
        public string? Disclaimer { get; init; }

        /// <summary>
        /// Gets the Activities Involvement
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, ActivitiesInvolvement>))]
        public IReadOnlyCollection<ActivitiesInvolvement>? ActivitiesInvolvement { get; init; }
    }
}