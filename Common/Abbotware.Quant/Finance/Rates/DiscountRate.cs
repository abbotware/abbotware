// -----------------------------------------------------------------------
// <copyright file="DiscountRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    using Abbotware.Core.Math;
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Discount Rate
    /// </summary>
    /// <param name="Rate">rate</param>
    public sealed record class DiscountRate(double Rate) : Yield(Rate, double.MaxValue), IRiskFreeRate<double>
    {
        /// <inheritdoc/>
        Interval<double> ICurve<double, double>.Range => this.TimePeriod;

        /// <inheritdoc/>
        public double GetPoint(double x) => this.Rate;

        /// <inheritdoc/>
        public double Nearest(double x) => this.Rate;
    }
}
