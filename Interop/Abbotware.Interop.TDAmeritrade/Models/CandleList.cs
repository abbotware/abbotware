// -----------------------------------------------------------------------
// <copyright file="CandleList.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Candle List Poco
    /// </summary>
    /// <param name="Symbol">symbol</param>
    /// <param name="Empty">value indicating the list is empty</param>
    public record CandleList(
        [property: Key][property: MaxLength(10)] string Symbol,
        bool Empty)
    {
        /// <summary>
        /// Gets the list of candles
        /// </summary>
        public ICollection<Candle>? Candles { get; init; }
    }
}
