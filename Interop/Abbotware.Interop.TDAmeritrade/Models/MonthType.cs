// -----------------------------------------------------------------------
// <copyright file="MonthType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// MonthMonth Type
    /// </summary>
    public enum MonthType
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// January
        /// </summary>
        [EnumMember(Value = "JAN")]
        January,

        /// <summary>
        /// February
        /// </summary>
        [EnumMember(Value = "FEB")]
        February,

        /// <summary>
        /// March
        /// </summary>
        [EnumMember(Value = "MAR")]
        March,

        /// <summary>
        /// April
        /// </summary>
        [EnumMember(Value = "APR")]
        April,

        /// <summary>
        /// MAY
        /// </summary>
        [EnumMember(Value = "MAY")]
        May,

        /// <summary>
        /// June
        /// </summary>
        [EnumMember(Value = "JUN")]
        June,

        /// <summary>
        /// July
        /// </summary>
        [EnumMember(Value = "JUL")]
        July,

        /// <summary>
        /// August
        /// </summary>
        [EnumMember(Value = "AUG")]
        August,

        /// <summary>
        /// September
        /// </summary>
        [EnumMember(Value = "SEP")]
        September,

        /// <summary>
        /// October
        /// </summary>
        [EnumMember(Value = "OCT")]
        October,

        /// <summary>
        /// November
        /// </summary>
        [EnumMember(Value = "NOV")]
        November,

        /// <summary>
        /// December
        /// </summary>
        [EnumMember(Value = "DEC")]
        December,
    }
}