// -----------------------------------------------------------------------
// <copyright file="NominalRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    using Abbotware.Quant.Finance;

    /// <summary>
    /// Nominal Rate
    /// </summary>
    /// <param name="Rate">r = R/100</param>
    public record class NominalRate(double Rate) : BaseRate(Rate, (int)TimePeriod.Annually)
    {
        /// <summary>
        /// Gets the normalized rate per period
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public double RatePerPeriod => this.Rate / (ushort)this.Units;
    }
}
