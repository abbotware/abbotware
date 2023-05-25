// -----------------------------------------------------------------------
// <copyright file="Coupon.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.Quant.Periodic;

    public record Coupon(BaseRate Rate, BaseTimePeriod<double> PaymentFrequency)
    {
        public virtual bool IsZeroCoupon => this.Rate.Rate == 0;

        public static Coupon Simple(double annualRate, TimePeriod couponFrequency)
            => new Coupon(new NominalRate(annualRate), new SimplePeriodic<double>(couponFrequency));
    }
}
