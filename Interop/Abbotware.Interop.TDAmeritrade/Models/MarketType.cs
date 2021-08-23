// -----------------------------------------------------------------------
// <copyright file="MarketType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Asset type
    /// </summary>
    public enum MarketType
    {
        /// <summary>
        /// Equity
        /// </summary>
        [EnumMember(Value = "EQUITY")]
        Equity,

        /// <summary>
        /// Option
        /// </summary>
        [EnumMember(Value = "OPTION")]
        Option,

        /// <summary>
        /// Future
        /// </summary>
        [EnumMember(Value = "FUTURE")]
        Future,

        /// <summary>
        /// Bond
        /// </summary>
        [EnumMember(Value = "BOND")]
        Bond,

        /// <summary>
        /// Foreign Exchange
        /// </summary>
        [EnumMember(Value = "FOREX")]
        Forex,
    }
}
