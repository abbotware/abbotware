// -----------------------------------------------------------------------
// <copyright file="ZeroCoupon.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.Quant.Periodic;

    /// <summary>
    /// Represents a Zero Coupton
    /// </summary>
    public record ZeroCoupon() : Coupon(new NominalRate(0), new SimplePeriodic<double>(TimePeriod.None))
    {
        /// <inheritdoc/>
        public override bool IsZeroCoupon => true;
    }
}
