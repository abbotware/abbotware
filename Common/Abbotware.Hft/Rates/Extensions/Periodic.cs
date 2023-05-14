// -----------------------------------------------------------------------
// <copyright file="Yearly.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates.Extensions
{
    using Abbotware.Quant.Enums;

    /// <summary>
    /// Yearly Accrual
    /// </summary>
    /// <param name="Periods">Periods per year</param>
    public record Periodic(AccrualPeriods Periods) : Accrual
    {
        /// <summary>
        /// Normalized Rate per period
        /// </summary>
        /// <param name="rate">rate</param>
        /// <returns>rate adjusted per period</returns>
        public override double RatePerPeriod(double rate)
        {
            return rate / (ushort)this.Periods;
        }
    }
}
