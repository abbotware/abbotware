// -----------------------------------------------------------------------
// <copyright file="ZeroRateCurve.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Abbotware.Core.Math;
    using Abbotware.Quant.Assets;
    using Abbotware.Quant.Extensions;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Price.Plugins;

    /// <summary>
    /// Zero Rate Curve
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    public class ZeroRateCurve<TDate> : DiscreteCurve<TDate, double>, IRiskFreeRate<TDate>
              where TDate : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroRateCurve{TDate}"/> class.
        /// </summary>
        public ZeroRateCurve()
            : base(Array.Empty<KeyValuePair<TDate, double>>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroRateCurve{TDate}"/> class.
        /// </summary>
        /// <param name="points">data points to use for the curve</param>
        public ZeroRateCurve(params KeyValuePair<TDate, double>[] points)
            : base(points)
        {
        }

        /// <summary>
        /// Bootstraps a zero-rate curve from bond prices
        /// </summary>
        /// <param name="bonds">bonds</param>
        /// <param name="t0"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ZeroRateCurve<double> Bootstrap(IEnumerable<(Bond Bond, decimal Price)> bonds, double t0)
        {
            var rates = new List<KeyValuePair<double, double>>();

            var sorted = bonds.OrderBy(x => x.Bond.Maturity).ToList();
            var f = sorted.First();

            var yield = f.Bond.Yield(f.Price, t0);
            rates.Add(new(yield.Rate, yield.End));

            foreach (var p in bonds.OrderBy(x => x.Bond.Maturity))
            {
                var curve = new ZeroRateCurve<double>(rates.ToArray());

                var cf = p.Bond.Cashflow(t0, curve.Last().X);
                var df = cf.AsDiscounted(curve);
            }

            throw new NotImplementedException();
        }
    }
}
