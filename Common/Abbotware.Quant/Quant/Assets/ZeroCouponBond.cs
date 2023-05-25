// -----------------------------------------------------------------------
// <copyright file="ZeroCouponBond.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    /// <summary>
    /// Zero Coupon Bond
    /// </summary>
    /// <param name="Maturity">maturity in years</param>
    public record ZeroCouponBond(double Maturity) : Bond(Maturity, new ZeroCoupon())
    {
    }
}
