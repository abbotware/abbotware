// -----------------------------------------------------------------------
// <copyright file=OptionChain.cs company=Abbotware, LLC>
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// StrikePriceMap POCO
    /// </summary>
    /// <param name="PutCall">Contract Type</param>
    /// <param name="Symbol">Option Symbol</param>
    /// <param name="Description">Option Description</param>
    /// <param name="ExchangeName">exhcange name</param>
    /// <param name="Bid">Option's Bid Price</param>
    /// <param name="Ask">Option's Ask Price</param>
    /// <param name="Last">Last Price Option Traded</param>
    /// <param name="Mark">Marked Price</param>
    /// <param name="BidSize">Option's Bid Size</param>
    /// <param name="AskSize">Option's Ask Size</param>
    /// <param name="BidAskSize">Option's BidSizeXAskSize Price</param>
    /// <param name="LastSize">Last Size Option Traded</param>
    /// <param name="HighPrice">High Price</param>
    /// <param name="LowPrice">Low Pirce</param>
    /// <param name="OpenPrice">Open Price</param>
    /// <param name="ClosePrice">Close Price</param>
    /// <param name="TotalVolume">Trade Volume</param>
    /// <param name="TradeDate">trade date</param>
    /// <param name="TradeTimeInLong">trade date in long (Unix time)</param>
    /// <param name="QuoteTimeInLong">quote time in long (Unix Time)</param>
    /// <param name="NetChange">net chanage</param>
    /// <param name="Volatility">volatility</param>
    /// <param name="Delta">Option's Delta</param>
    /// <param name="Gamma">Option's Gamma</param>
    /// <param name="Theta">Option's Theta</param>
    /// <param name="Vega">Option's Vega</param>
    /// <param name="Rho">Option's Rho</param>
    /// <param name="OpenInterest">open interest for contract</param>
    /// <param name="TimeValue">time value</param>
    /// <param name="TheoreticalOptionValue">theoretical option value</param>
    /// <param name="TheoreticalVolatility">theoretical volatility</param>
    /// <param name="StrikePrice">option strike price</param>
    /// <param name="ExpirationDate">expiration date</param>
    /// <param name="DaysToExpiration">days to expiration</param>
    /// <param name="ExpirationType">contract expiration type</param>
    /// <param name="LastTradingDay">last trading date</param>
    /// <param name="Multiplier">price multiplier</param>
    /// <param name="SettlementType">settlement type</param>
    /// <param name="DeliverableNote">deliverable note</param>
    /// <param name="IsIndexOption">flag for is index option</param>
    /// <param name="PercentChange">percentage change</param>
    /// <param name="MarkChange">marked price change amount</param>
    /// <param name="MarkPercentChange">marked price change percentage</param>
    /// <param name="IntrinsicValue">intrinsic value</param>
    /// <param name="PennyPilot">flag for penny pilot</param>
    /// <param name="InTheMoney">flag for in the money</param>
    /// <param name="Mini">flag for mini</param>
    /// <param name="NonStandard">flag for non-standard option</param>
    public record StrikePriceMap(
        ContractType PutCall,
        [property: Key][property: MaxLength(TDAmeritradeConstants.SymbolLength)] string Symbol,
        string? Description,
        ExchangeType ExchangeName,
        decimal? Bid,
        decimal? Ask,
        decimal? Last,
        decimal? Mark,
        double? BidSize,
        double? AskSize,
        string? BidAskSize,
        double? LastSize,
        decimal? HighPrice,
        decimal? LowPrice,
        decimal? OpenPrice,
        decimal? ClosePrice,
        double? TotalVolume,
        string? TradeDate,
        long? TradeTimeInLong,
        long? QuoteTimeInLong,
        decimal? NetChange,
        double? Volatility,
        double? Delta,
        double? Gamma,
        double? Theta,
        double? Vega,
        double? Rho,
        int? OpenInterest,
        double? TimeValue,
        double? TheoreticalOptionValue,
        double? TheoreticalVolatility,
        decimal? StrikePrice,
        long? ExpirationDate,
        int? DaysToExpiration,
        string? ExpirationType,
        long? LastTradingDay,
        double? Multiplier,
        string? SettlementType,
        string? DeliverableNote,
        bool? IsIndexOption,
        double? PercentChange,
        decimal? MarkChange,
        double? MarkPercentChange,
        decimal? IntrinsicValue,
        bool? PennyPilot,
        bool? InTheMoney,
        bool? Mini,
        bool? NonStandard)
    {
        /// <summary>
        /// Gets the list of Option Delieverables
        /// </summary>
        public ICollection<OptionDeliverable>? OptionDeliverablesList { get; init; }

        /// <summary>
        /// Gets the TradeTime inDateTimeOffset
        /// </summary>
        public DateTimeOffset? TradeDateTime => this.TradeTimeInLong.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(this.TradeTimeInLong.Value) : null;

        /// <summary>
        /// Gets the QuoteTime in DateTimeOffset
        /// </summary>
        public DateTimeOffset? QuoteDateTime => this.QuoteTimeInLong.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(this.QuoteTimeInLong.Value) : null;
    }
}