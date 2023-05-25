// -----------------------------------------------------------------------
// <copyright file="CompoundingFrequency.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance
{
    /// <summary>
    /// Accrual Period
    /// </summary>
    public enum CompoundingFrequency : ushort
    {
        /// <summary>
        /// No compounding (ie simple interest)
        /// </summary>
        None = 0,

        /// <summary>
        /// Yearly
        /// </summary>
        Yearly = 1,

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
        /// Daily
        /// </summary>
        Daily = 365,

        /// <summary>
        /// Continuous
        /// </summary>
        Continuous = ushort.MaxValue,
    }
}
