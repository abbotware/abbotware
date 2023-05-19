// -----------------------------------------------------------------------
// <copyright file="ContractType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Contract Type
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// day
        /// </summary>
        [EnumMember(Value = "PUT")]
        Put,

        /// <summary>
        /// day
        /// </summary>
        [EnumMember(Value = "CALL")]
        Call,
    }
}