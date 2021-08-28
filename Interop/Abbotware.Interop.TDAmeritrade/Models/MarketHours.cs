// -----------------------------------------------------------------------
// <copyright file="MarketHours.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Market Hours Poco
    /// </summary>
    /// <param name="Date">cusip</param>
    /// <param name="MarketType">market type price</param>
    /// <param name="Exchange">Exchange name</param>
    /// <param name="Category">category</param>
    /// <param name="Product">Product Code</param>
    /// <param name="ProductName">Product description</param>
    /// <param name="IsOpen">volume</param>
    public record MarketHours(
        DateTimeOffset? Date,
        string? MarketType,
        string? Exchange,
        string? Category,
        string? Product,
        string? ProductName,
        bool? IsOpen)
    {
        /// <summary>
        /// Gets the session hours
        /// </summary>
        public IReadOnlyDictionary<string, Hours[]>? SessionHours { get; init; }
    }

    /// <summary>
    /// Session Hours
    /// </summary>
    /// <param name="Start">session start time</param>
    /// <param name="End">session end time</param>
    public record Hours(DateTimeOffset? Start, DateTimeOffset? End);
}