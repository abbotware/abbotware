// -----------------------------------------------------------------------
// <copyright file="Continuous.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Interest
{
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Continuous Interest Calculator
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public record class Continuous(NominalRate Rate) : CompoundingInterest(Rate)
    {
        /// <inheritdoc/>
        public override decimal AccruedAmount(decimal principal, double t)
        {
            return principal * (decimal)CompoundingFactor.Continuous(this.Rate.Rate, t);
        }
    }
}
