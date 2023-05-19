// -----------------------------------------------------------------------
// <copyright file="OptionType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Option Type
    /// </summary>
    public enum OptionType
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// Standard contracts
        /// </summary>
        [EnumMember(Value = "S")]
        Standard,

        /// <summary>
        /// Non-standard contracts
        /// </summary>
        [EnumMember(Value = "NS")]
        NonStandard,
    }
}