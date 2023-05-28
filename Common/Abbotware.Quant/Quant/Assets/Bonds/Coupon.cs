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

    /// <summary>
    /// Represents a Bond's Coupon
    /// </summary>
    /// <param name="Rate">The coupon rate </param>
    /// <param name="PaymentFrequency">the coupon frequency</param>
    public record Coupon(BaseRate Rate, BaseTimePeriod<double> PaymentFrequency)
    {
        /// <summary>
        /// Gets a value indicating whether this is a zero rate coupon
        /// </summary>
        public virtual bool IsZeroCoupon => this.Rate.Rate == 0 || this.PaymentFrequency.UnitsPerPeriod() == 0;

        /// <summary>
        /// Creates a Simple Coupon
        /// </summary>
        /// <param name="annualRate">annual rate</param>
        /// <param name="couponFrequency">frequency</param>
        /// <returns>coupon</returns>
        public static Coupon Simple(double annualRate, TimePeriod couponFrequency)
            => new(new NominalRate(annualRate), new SimplePeriodic<double>(couponFrequency));
    }
}
