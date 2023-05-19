// -----------------------------------------------------------------------
// <copyright file="OptionStrategyType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Options Strategy Type
    /// </summary>
    public enum OptionStrategyType
    {
        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "SINGLE")]
        Single,

        /// <summary>
        /// Roll Strategy
        /// (allows use of the volatility, underlyingPrice, interestRate, and daysToExpiration params to calculate theoretical values),
        /// </summary>
        [EnumMember(Value = "ANALYTICAL")]
        Analytical,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "COVERED")]
        Covered,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "VERTICAL")]
        Vertical,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "CALENDAR")]
        Calendar,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "STRANGLE")]
        Strangle,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "STRADDLE")]
        Straddle,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "BUTTERFLY")]
        Butterfly,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "CONDOR")]
        Condor,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "DIAGONAL")]
        Diagonal,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "COLLAR")]
        Collar,

        /// <summary>
        /// Roll Strategy
        /// </summary>
        [EnumMember(Value = "ROLL")]
        Roll,
    }
}