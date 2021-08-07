// -----------------------------------------------------------------------
// <copyright file="AssetType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Asset type
    /// </summary>
    public enum AssetType
    {
        /// <summary>
        /// Equity
        /// </summary>
        [EnumMember(Value = "EQUITY")]
        Equity,

        /// <summary>
        /// Exchange Traded Fund
        /// </summary>
        [EnumMember(Value = "ETF")]
        Etf,

        /// <summary>
        /// Foreign Exchange
        /// </summary>
        [EnumMember(Value = "FOREX")]
        Forex,

        /// <summary>
        /// Future
        /// </summary>
        [EnumMember(Value = "FUTURE")]
        Future,

        /// <summary>
        /// Future Option
        /// </summary>
        [EnumMember(Value = "FUTURE_OPTION")]
        FutureOption,

        /// <summary>
        /// Index
        /// </summary>
        [EnumMember(Value = "INDEX")]
        Index,

        /// <summary>
        /// Indicator
        /// </summary>
        [EnumMember(Value = "INDICATOR")]
        Indicator,

        /// <summary>
        /// Mutual Fund
        /// </summary>
        [EnumMember(Value = "MUTUAL_FUND")]
        MutualFund,

        /// <summary>
        /// Option
        /// </summary>
        [EnumMember(Value = "OPTION")]
        Option,

        /// <summary>
        /// Unknown
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        Unknown,
    }
}
