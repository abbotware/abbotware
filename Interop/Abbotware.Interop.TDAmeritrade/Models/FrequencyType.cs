// -----------------------------------------------------------------------
// <copyright file="FrequencyType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Frequency type
    /// </summary>
    public enum FrequencyType
    {
        /// <summary>
        /// minute
        /// </summary>
        [EnumMember(Value = "minute")]
        Minute,

        /// <summary>
        /// daily
        /// </summary>
        [EnumMember(Value = "daily")]
        Daily,

        /// <summary>
        /// weekly
        /// </summary>
        [EnumMember(Value = "weekly")]
        Weekly,

        /// <summary>
        /// monthly
        /// </summary>
        [EnumMember(Value = "monthly")]
        Monthly,
    }
}
