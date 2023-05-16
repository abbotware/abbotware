// -----------------------------------------------------------------------
// <copyright file="BaseRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    using Abbotware.Quant.Enums;

    public record class Yield<TDate>(double Rate, TDate Start, TDate End) : BaseRate(Rate);

    /// <summary>
    /// base for rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public record class BaseRate(double Rate)
    {
    }

    /// <summary>
    /// Nominal Rate
    /// </summary>
    /// <param name="Rate">Rate in percentage (R/100)</param>
    /// <param name="Period">Time period for Rate</param>
    public record class NominalRate(double Rate, TimePeriod Period) : BaseRate(Rate)
    {
        /// <summary>
        /// Gets the normalized rate per period
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public double RatePerPeriod => this.Rate / (ushort)this.Period;
    }

    /// <summary>
    /// Effective Rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public record class EffectiveRate(double Rate) : BaseRate(Rate)
    {
    }

    /// <summary>
    /// Effective Rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public record class ActualRate(double Rate) : BaseRate(Rate)
    {
    }
}
