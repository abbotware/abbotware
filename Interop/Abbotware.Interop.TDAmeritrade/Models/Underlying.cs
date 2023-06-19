// -----------------------------------------------------------------------
// <copyright file="Underlying.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Underlying POCO (Fundamlental Data for an Option's Underlying)
    /// </summary>
    /// <param name="Ask">Ask Price</param>
    /// <param name="AskSize">Ask Size</param>
    /// <param name="Bid">Bid Pirce</param>
    /// <param name="BidSize">Bid Size</param>
    /// <param name="Change">Change in price</param>
    /// <param name="Close">Close Price</param>
    /// <param name="Delayed">flag to indicate quote/price is delayed</param>
    /// <param name="Description">Description</param>
    /// <param name="ExchangName">Exchange</param>
    /// <param name="FiftyTwoWeekHigh">High Price - Last 52 weeks</param>
    /// <param name="FiftyTwoWeekLow">Low Price - Last 52 weeks</param>
    /// <param name="HighPrice">High Price</param>
    /// <param name="Last">Last Price</param>
    /// <param name="LowPrice">Low Pirce</param>
    /// <param name="Mark">current ark</param>
    /// <param name="MarkChange">Change in Mark</param>
    /// <param name="MarkPercentChange">Percentage change in Mark</param>
    /// <param name="OpenPrice">Open Price</param>
    /// <param name="PercentChange">Precentage change in Price</param>
    /// <param name="Symbol">Option's Underlingy Symbol</param>
    /// <param name="QuoteTime">Quote Time</param>
    /// <param name="TotalVolume">Total Trading Volume</param>
    /// <param name="TradeTime">Trade Time</param>
    public record Underlying(
        decimal? Ask,
        double AskSize,
        decimal? Bid,
        double BidSize,
        decimal? Change,
        decimal? Close,
        bool? Delayed,
        string? Description,
        ExchangeType ExchangName,
        decimal? FiftyTwoWeekHigh,
        decimal? FiftyTwoWeekLow,
        decimal? HighPrice,
        decimal? Last,
        decimal? LowPrice,
        decimal? Mark,
        decimal? MarkChange,
        double? MarkPercentChange,
        decimal? OpenPrice,
        double? PercentChange,
        [property: MaxLength(TDAmeritradeConstants.SymbolLength)] string Symbol,
        DateTimeOffset QuoteTime,
        double TotalVolume,
        DateTimeOffset TradeTime);
}
