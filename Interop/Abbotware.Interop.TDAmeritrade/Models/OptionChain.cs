// -----------------------------------------------------------------------
// <copyright file=OptionChain.cs company=Abbotware, LLC>
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Option Chain POCO
    /// </summary>
    /// <param name="Symbol">Symbol the Option is based on</param>
    /// <param name="Status">status</param>
    /// <param name="Strategy">Option Strategy Type</param>
    /// <param name="Interval">???</param>
    /// <param name="IsDelayed">Flag that indicates the price/quote id delated</param>
    /// <param name="IsIndex">Flag that indicates this is an index based option</param>
    /// <param name="DaysToExpiration">Number of days to expiration</param>
    /// <param name="InterestRate">interest rate assumption</param>
    /// <param name="UnderlyingPrice">underlying price</param>
    /// <param name="Volatility">underlying assumption</param>
    /// <param name="NumberOfContracts">number of outstanding contracts</param>
    public record OptionChain(
        [property: Key][property: MaxLength(TDAmeritradeConstants.SymbolLength)] string Symbol,
        [property: MaxLength(10)] string? Status,
        OptionStrategyType Strategy,
        decimal? Interval,
        bool? IsDelayed,
        bool? IsIndex,
        int? DaysToExpiration,
        double? InterestRate,
        decimal? UnderlyingPrice,
        double? Volatility,
        int? NumberOfContracts)
    {
        /// <summary>
        /// Gets the underlying's fundamental data
        /// </summary>
        public Underlying? Underlying { get; init; }

        /// <summary>
        /// Gets the call option strike map
        /// </summary>
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyCollection<StrikePriceMap>>>? CallExpDateMap { get; init; }

        /// <summary>
        /// Gets the put option strike map
        /// </summary>
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, IReadOnlyCollection<StrikePriceMap>>>? PutExpDateMap { get; init; }
    }
}