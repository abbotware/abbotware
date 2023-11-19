// -----------------------------------------------------------------------
// <copyright file="ZeroRateCurve{TDate}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates.Plugins
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Math;
    using Abbotware.Quant.InterestRates;

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
    }
}
