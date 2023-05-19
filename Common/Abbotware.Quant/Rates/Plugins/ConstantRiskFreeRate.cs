// -----------------------------------------------------------------------
// <copyright file="ConstantRiskFreeRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates.Plugins
{
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Constant Risk-Free Rate
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Rate">rate</param>
    public record class ConstantRiskFreeRate<TDate>(double Rate) : IRiskFreeRate<TDate>
          where TDate : notnull
    {
        /// <inheritdoc/>
        public double GetPoint(TDate x) => this.Rate;

        /// <inheritdoc/>
        public double Nearest(TDate x) => this.Rate;
    }
}
