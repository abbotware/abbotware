// -----------------------------------------------------------------------
// <copyright file="BaseRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    using Abbotware.Quant.Finance;

    /// <summary>
    /// base for rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    /// <param name="Units">time period for this rate</param>
    public record class BaseRate(double Rate, double Units)
    {
    }

    /// <summary>
    /// Effective Rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public record class EffectiveRate(double Rate) : BaseRate(Rate, (int)TimePeriod.Annually)
    {
    }

    /// <summary>
    /// Effective Rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public record class ActualRate(double Rate) : BaseRate(Rate, (int)TimePeriod.Annually)
    {
    }
}
