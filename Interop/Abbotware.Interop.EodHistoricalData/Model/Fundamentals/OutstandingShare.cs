// -----------------------------------------------------------------------
// <OutstandingShare file="Fundamental.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Outstanding POCO
    /// </summary>
    public record OutstandingShare(
        [property: MaxLength(7)] string Date,
        [property: MaxLength(10)] string DateFormatted,
        double? SharesMln,
        long Shares)
    {
    }
}