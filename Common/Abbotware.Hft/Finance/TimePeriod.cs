// -----------------------------------------------------------------------
// <copyright file="TimePeriod.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Enums
{
    /// <summary>
    /// Time Period
    /// </summary>
    public enum TimePeriod : ushort
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// Annually
        /// </summary>
        Annually = 1,

        /// <summary>
        /// Semi-Annually (2x a year)
        /// </summary>
        SemiAnnually = 2,

        /// <summary>
        /// Quarterly (4x a year)
        /// </summary>
        Quarterly = 4,

        /// <summary>
        /// Bi-Monthly  (Every 2 months)
        /// </summary>
        BiMonthly = 6,

        /// <summary>
        /// Monthly
        /// </summary>
        Monthly = 12,

        /// <summary>
        /// Semi-Monthly (2x a month)
        /// </summary>
        SemiMonthly = 24,

        /// <summary>
        /// Bi-Weekly (Every 2 weeks)
        /// </summary>
        BiWeekly = 26,

        /// <summary>
        /// Weekly (2x a year)
        /// </summary>
        Weekly = 52,

        /// <summary>
        /// Daily (365x a year)
        /// </summary>
        /// <remarks>Day Count Covention may be required</remarks>
        Daily = 365,
    }
}
