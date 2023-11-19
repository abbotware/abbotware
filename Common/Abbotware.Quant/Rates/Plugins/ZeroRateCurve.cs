// -----------------------------------------------------------------------
// <copyright file="ZeroRateCurve.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates.Plugins
{
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Quant.Assets;
    using Abbotware.Quant.Extensions;

    /// <summary>
    ///  Zero Rate Curve helper functions
    /// </summary>
    public static class ZeroRateCurve
    {
        /// <summary>
        /// Bootstraps a zero-rate curve from bond prices
        /// </summary>
        /// <param name="bonds">bonds</param>
        /// <returns>zero rate curve</returns>
        public static ZeroRateCurve<double> Bootstrap(IEnumerable<(Bond Bond, decimal Price)> bonds)
        {
            var rates = new List<KeyValuePair<double, double>>();

            var sorted = bonds.OrderBy(x => x.Bond.Maturity).ToList();

            foreach (var p in sorted)
            {
                if (p.Bond.Coupon.IsZeroCoupon)
                {
                    var yield = p.Bond.Yield(p.Price);
                    rates.Add(new(yield.Rate, yield.TimePeriod.Upper));
                }
                else
                {
                    var curve = new ZeroRateCurve<double>(rates.ToArray());
                    var cf = p.Bond.CashflowTheoretical();
                    var df = cf.ForComputation().AsDiscounted(curve);
                }
            }

            return new ZeroRateCurve<double>(rates.ToArray());
        }
    }
}
