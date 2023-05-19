// -----------------------------------------------------------------------
// <copyright file="Fundamental.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Fundamental POCO
    /// </summary>
    /// <param name="Symbol">Symbol</param>
    /// <param name="High52">High Price - Last 52 weeks</param>
    /// <param name="Low52">Low Price - Last 52 weeks</param>
    /// <param name="DividendAmount">Dividend Amount</param>
    /// <param name="DividendYield">Dividend Yield</param>
    /// <param name="DividendDate">Dividend Date</param>
    /// <param name="PeRatio">Price-to-Earnings (P/E) Ratio</param>
    /// <param name="PegRatio">Price/Earnings-to-Growth (PEG) Ratio</param>
    /// <param name="PbRatio">Price-To-Book (P/B) Ratio</param>
    /// <param name="PrRatio">Price-To-Revenue Ratio</param>
    /// <param name="PcfRatio">Price-to-Cash Flow (P/CF) Ratio</param>
    /// <param name="GrossMarginTTM">Gross Margin (TTM) (%) [trailing twelve months]</param>
    /// <param name="GrossMarginMRQ">Gross Margin (MRQ) (%) [most recent quarter]</param>
    /// <param name="NetProfitMarginTTM">Net Profit Margin (TTM) (%) [trailing twelve months]</param>
    /// <param name="NetProfitMarginMRQ">Net Profit Margin (MRQ) (%) [most recent quarter]</param>
    /// <param name="OperatingMarginTTM">Operating Margin (TTM) [trailing twelve months]</param>
    /// <param name="OperatingMarginMRQ">Operating Margin (MRQ) [most recent quarter]</param>
    /// <param name="ReturnOnEquity">Return On Equity</param>
    /// <param name="ReturnOnAssets">Return On Assets</param>
    /// <param name="ReturnOnInvestment">Return On Investment</param>
    /// <param name="QuickRatio">The Quick Ratio is the total of cash, Accounts Receivable, and short term investments divided by current Liabilities</param>
    /// <param name="CurrentRatio">Current Ratio</param>
    /// <param name="InterestCoverage">Interest Coverage Ratio</param>
    /// <param name="TotalDebtToCapital">Total Debt To Capital Ratio</param>
    /// <param name="LtDebtToEquity">Long-Term Debt To Equity Ratio</param>
    /// <param name="TotalDebtToEquity">Total Debt To Equity</param>
    /// <param name="EpsTTM">Earnings per share [trailing twelve months]</param>
    /// <param name="EpsChangePercentTTM">Earnings per share change (%) [trailing twelve months]</param>
    /// <param name="EpsChangeYear">Earnings per share Year To Date?</param>
    /// <param name="EpsChange">Earnings per share Change</param>
    /// <param name="RevChangeYear">Revenue Change Year To Date?</param>
    /// <param name="RevChangeTTM">Revenue Change [trailing twelve months]</param>
    /// <param name="RevChangeIn">Revenue Change (in Assets Liabilities?)</param>
    /// <param name="SharesOutstanding">Shares Outstanding</param>
    /// <param name="MarketCapFloat">Free Float Market Capitalization</param>
    /// <param name="MarketCap">Market Capitalization</param>
    /// <param name="BookValuePerShare">Book Value Per Share</param>
    /// <param name="ShortIntToFloat">Short Interest To Float</param>
    /// <param name="ShortIntDayToCover">Short Interest Days To Cover</param>
    /// <param name="DivGrowthRate3Year">Dividend Growth Rate 3-Year</param>
    /// <param name="DividendPayAmount">Dividend Pay Amount</param>
    /// <param name="DividendPayDate">Dividend Pay Date</param>
    /// <param name="Beta">Beta</param>
    /// <param name="Vol1DayAvg">Average Trading Volume - 1 Day Average</param>
    /// <param name="Vol10DayAvg">Average Trading Volume - 10 Day Average</param>
    /// <param name="Vol3MonthAvg">Average Trading Volume - 3 Month Average</param>
    public record Fundamental(
        [property: Key][property: MaxLength(10)] string Symbol,
        decimal? High52,
        decimal? Low52,
        decimal? DividendAmount,
        double? DividendYield,
        DateTime? DividendDate,
        double? PeRatio,
        double? PegRatio,
        double? PbRatio,
        double? PrRatio,
        double? PcfRatio,
        double? GrossMarginTTM,
        double? GrossMarginMRQ,
        double? NetProfitMarginTTM,
        double? NetProfitMarginMRQ,
        double? OperatingMarginTTM,
        double? OperatingMarginMRQ,
        double? ReturnOnEquity,
        double? ReturnOnAssets,
        double? ReturnOnInvestment,
        double? QuickRatio,
        double? CurrentRatio,
        double? InterestCoverage,
        double? TotalDebtToCapital,
        double? LtDebtToEquity,
        double? TotalDebtToEquity,
        double? EpsTTM,
        double? EpsChangePercentTTM,
        decimal? EpsChangeYear,
        decimal? EpsChange,
        decimal? RevChangeYear,
        double? RevChangeTTM,
        double? RevChangeIn,
        double? SharesOutstanding,
        decimal? MarketCapFloat,
        decimal? MarketCap,
        decimal? BookValuePerShare,
        double? ShortIntToFloat,
        double? ShortIntDayToCover,
        double? DivGrowthRate3Year,
        decimal? DividendPayAmount,
        DateTime? DividendPayDate,
        double? Beta,
        double? Vol1DayAvg,
        double? Vol10DayAvg,
        double? Vol3MonthAvg)
    {
    }
}