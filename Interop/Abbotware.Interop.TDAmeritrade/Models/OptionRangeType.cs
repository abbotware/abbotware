// -----------------------------------------------------------------------
// <copyright file="OptionRangeType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Option Range Type
    /// </summary>
    public enum OptionRangeType
    {
        /// <summary>
        /// All Strikes
        /// </summary>
        [EnumMember(Value = "ALL")]
        AllStrikes,

        /// <summary>
        /// In-the-money
        /// </summary>
        [EnumMember(Value = "ITM")]
        InTheMoney,

        /// <summary>
        /// Near-the-money
        /// </summary>
        [EnumMember(Value = "NTM")]
        NearTheMoney,

        /// <summary>
        /// Out-of-the-money
        /// </summary>
        [EnumMember(Value = "OTM")]
        OutOfTheMoney,

        /// <summary>
        ///  Strikes Above Market
        /// </summary>
        [EnumMember(Value = "SAK")]
        StrikesAboveMarket,

        /// <summary>
        ///  Strikes Below Market
        /// </summary>
        [EnumMember(Value = "SBK")]
        StrikesBelowMarket,

        /// <summary>
        ///  Strikes Near Market
        /// </summary>
        [EnumMember(Value = "SNK")]
        StrikesNearMarket,
    }
}