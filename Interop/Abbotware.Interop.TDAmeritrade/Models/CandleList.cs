// -----------------------------------------------------------------------
// <copyright file="CandleList.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Candle List Poco
    /// </summary>
    /// <param name="Empty">value indicating the list is empty</param>
    /// <param name="Symbol">symbol</param>
    public record CandleList(
        bool Empty,
        string Symbol)
    {
        /// <summary>
        /// Gets the list of candles
        /// </summary>
        public ICollection<Candle>? Candles { get; init; }
    }
}
