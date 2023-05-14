// -----------------------------------------------------------------------
// <copyright file="BaseRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
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
    /// <param name="Rate">Rate in Percentage</param>
    public record class NominalRate(double Rate) : BaseRate(Rate)
    {
    }

    /// <summary>
    /// Effective Rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public record class EffectiveRate(double Rate) : BaseRate(Rate)
    {
    }
}
