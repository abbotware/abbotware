// -----------------------------------------------------------------------
// <copyright file="CompoundingInterest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Interest
{
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Base for Compounding Interest
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public abstract record class CompoundingInterest(NominalRate Rate) : InterestCalculator(Rate)
    {
        /// <inheritdoc/>
        public override decimal Interest(decimal principal, double t)
        {
            return this.AccruedAmount(principal, t) - principal;
        }
    }
}
