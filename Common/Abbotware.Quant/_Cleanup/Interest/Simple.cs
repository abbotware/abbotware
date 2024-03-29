﻿// -----------------------------------------------------------------------
// <copyright file="Simple.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Interest
{
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Simple Interest Calculator
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public record class Simple(NominalRate Rate) : InterestCalculator(Rate)
    {
        /// <inheritdoc/>
        public override decimal Interest(decimal principal, double t)
        {
            return principal * (decimal)(this.Rate.Rate * t);
        }

        /// <inheritdoc/>
        public override decimal AccruedAmount(decimal principal, double t)
        {
            return principal + this.Interest(principal, t);
        }
    }
}
