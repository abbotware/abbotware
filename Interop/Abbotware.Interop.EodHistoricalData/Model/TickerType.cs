// -----------------------------------------------------------------------
// <copyright file="TickerType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Ticker Type
    /// </summary>
    public enum TickerType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "")]
        Unknown,

        /// <summary>
        /// Common Share
        /// </summary>
        [EnumMember(Value = "Common Stock")]
        CommonShare,

        /// <summary>
        /// Preferred Share
        /// </summary>
        [EnumMember(Value = "Preferred Share")]
        PreferredShare,

        /// <summary>
        /// Warrant
        /// </summary>
        Warrant,

        /// <summary>
        /// Bond
        /// </summary>
        [EnumMember(Value = "BOND")]
        Bond,

        /// <summary>
        /// Currency
        /// </summary>
        Currency,

        /// <summary>
        /// Index
        /// </summary>
        [EnumMember(Value = "INDEX")]
        Index,

        /// <summary>
        /// Fund?
        /// </summary>
        [EnumMember(Value = "FUND")]
        Fund,

        /// <summary>
        /// Mutual Fund
        /// </summary>
        [EnumMember(Value = "Mutual Fund")]
        MutualFund,

        /// <summary>
        /// Exchange-Traded Fund
        /// </summary>
        [EnumMember(Value = "ETF")]
        ExchangeTradedFund,

        /// <summary>
        /// Exchange-Traded Node
        /// </summary>
        [EnumMember(Value = "ETN")]
        ExchangeTradedNode,
    }
}