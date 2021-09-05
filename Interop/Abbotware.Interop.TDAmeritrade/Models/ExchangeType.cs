// -----------------------------------------------------------------------
// <copyright file="ExchangeType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Exchange type
    /// </summary>
    public enum ExchangeType
    {
        /// <summary>
        /// minute
        /// </summary>
        [EnumMember(Value = "Unknown")]
        Unknown,

        /// <summary>
        /// Pink Sheet
        /// </summary>
        [EnumMember(Value = "Pink Sheet")]
        PinkSheet,

        /// <summary>
        /// OTC BB
        /// </summary>
        [EnumMember(Value = "OTCBB")]
        OtcBB,

        /// <summary>
        /// BATS
        /// </summary>
        [EnumMember(Value = "BATS")]
        Bats,

        /// <summary>
        /// NYSE
        /// </summary>
        [EnumMember(Value = "NYSE")]
        Nyse,

        /// <summary>
        /// NASDAQ
        /// </summary>
        [EnumMember(Value = "NASDAQ")]
        Nasdaq,

        /// <summary>
        /// Indices
        /// </summary>
        [EnumMember(Value = "Indices")]
        Indices,

        /// <summary>
        /// AMEX Indices
        /// </summary>
        [EnumMember(Value = "AMEX Indices")]
        AmexIndices,

        /// <summary>
        /// Pacific
        /// </summary>
        [EnumMember(Value = "Pacific")]
        Pacific,

        /// <summary>
        /// AMEX
        /// </summary>
        [EnumMember(Value = "AMEX")]
        Amex,
    }
}
