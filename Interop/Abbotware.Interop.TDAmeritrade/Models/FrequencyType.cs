// -----------------------------------------------------------------------
// <copyright file="FrequencyType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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

    /// <summary>
    /// Valid Frequency for minutes
    /// </summary>
    public enum Minutes : int
    {
        /// <summary>
        /// 1
        /// </summary>
        One = 1,

        /// <summary>
        /// 5
        /// </summary>
        Five = 5,

        /// <summary>
        /// 10
        /// </summary>
        Ten = 10,

        /// <summary>
        /// 15
        /// </summary>
        Fifteen = 15,

        /// <summary>
        /// 30
        /// </summary>
        Thirty = 30,
    }

    /// <summary>
    /// Monthly Frequency Rate
    /// </summary>
    public enum Monthly
    {
        /// <summary>
        /// By Day
        /// </summary>
        ByDay,

        /// <summary>
        /// By Week
        /// </summary>
        ByWeek,
    }

    /// <summary>
    /// Yearly Frequency Rate
    /// </summary>
    public enum Yearly
    {
        /// <summary>
        /// By Day
        /// </summary>
        ByDay,

        /// <summary>
        /// By Week
        /// </summary>
        ByWeek,

        /// <summary>
        /// By Month
        /// </summary>
        ByMonth,
    }

    /// <summary>
    /// YearToDate Frequency Rate
    /// </summary>
    public enum YearToDateRate
    {
        /// <summary>
        /// By Day
        /// </summary>
        ByDay,

        /// <summary>
        /// By Week
        /// </summary>
        ByWeek,
    }
}
