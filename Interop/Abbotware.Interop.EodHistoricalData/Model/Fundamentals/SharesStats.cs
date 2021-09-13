// -----------------------------------------------------------------------
// <copyright file="SharesStats.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    /// <summary>
    /// Fundalmental\SharesStats POCO
    /// </summary>
    public record SharesStats(
        double? SharesOutstanding,
        double? SharesFloat,
        double? PercentInsiders,
        double? PercentInstitutions,
        double? SharesShort,
        double? SharesShortPriorMonth,
        double? ShortRatio,
        double? ShortPercentOutstanding,
        double? ShortPercentFloat)
    {
    }
}