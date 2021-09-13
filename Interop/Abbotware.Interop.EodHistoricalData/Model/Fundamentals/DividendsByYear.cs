// -----------------------------------------------------------------------
// <copyright file="DividendsByYear.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    /// <summary>
    /// DividendsByYear POCO
    /// </summary>
    public record DividendsByYear(
        short? Year,
        short? Count)
    {
    }
}