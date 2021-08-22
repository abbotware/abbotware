// -----------------------------------------------------------------------
// <copyright file="PeriodType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Period type
    /// </summary>
    public enum PeriodType
    {
        /// <summary>
        /// day
        /// </summary>
        [EnumMember(Value = "day")]
        Day,

        /// <summary>
        /// month
        /// </summary>
        [EnumMember(Value = "month")]
        Month,

        /// <summary>
        /// year
        /// </summary>
        [EnumMember(Value = "year")]
        Year,

        /// <summary>
        /// year to date
        /// </summary>
        [EnumMember(Value = "ytd")]
        YearToDate,
    }

    /// <summary>
    /// Valid Periods for Day
    /// </summary>
    public enum HowManyDays : int
    {
        /// <summary>
        /// 1
        /// </summary>
        One = 1,

        /// <summary>
        /// 2
        /// </summary>
        Two = 2,

        /// <summary>
        /// 3
        /// </summary>
        Three = 3,

        /// <summary>
        /// 4
        /// </summary>
        Four = 4,

        /// <summary>
        /// 5
        /// </summary>
        Five = 5,

        /// <summary>
        /// 10
        /// </summary>
        Ten = 10,
    }

    /// <summary>
    /// Valid Periods for Month
    /// </summary>
    public enum HowManyMonths : int
    {
        /// <summary>
        /// 1
        /// </summary>
        One = 1,

        /// <summary>
        /// 2
        /// </summary>
        Two = 2,

        /// <summary>
        /// 3
        /// </summary>
        Three = 3,

        /// <summary>
        /// 6
        /// </summary>
        Six = 6,
    }

    /// <summary>
    /// Valid Periods for Year
    /// </summary>
    public enum HowManyYears : int
    {
        /// <summary>
        /// 1
        /// </summary>
        One = 1,

        /// <summary>
        /// 2
        /// </summary>
        Two = 2,

        /// <summary>
        /// 3
        /// </summary>
        Three = 3,

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
        /// 20
        /// </summary>
        Twenty = 20,
    }
}