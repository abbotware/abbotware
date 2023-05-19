// -----------------------------------------------------------------------
// <copyright file="BaseRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    /// <summary>
    /// base for rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    /// <param name="Units">time period for this rate</param>
    public record class BaseRate(double Rate, double Units)
    {
    }
}
