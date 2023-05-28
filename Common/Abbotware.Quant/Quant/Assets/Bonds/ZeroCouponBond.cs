// -----------------------------------------------------------------------
// <copyright file="ZeroCouponBond.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    using Abbotware.Core.Math;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Zero Coupon Bond
    /// </summary>
    /// <param name="Maturity">maturity in years</param>
    public record ZeroCouponBond(double Maturity) : Bond(Maturity, new ZeroCoupon())
    {
        /// <inheritdoc/>>
        public override Yield Yield(decimal price, Interval<double> t)
        {
            if (t.Upper < this.Maturity)
            {
                return new Yield(0, t);
            }

            var y = TimeValue.Continuous.Rate(price, this.Notional, t.Upper);

            return new Yield(y, t);
        }
    }
}