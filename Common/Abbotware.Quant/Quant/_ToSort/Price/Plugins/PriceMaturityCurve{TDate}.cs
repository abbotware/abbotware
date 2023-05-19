// -----------------------------------------------------------------------
// <copyright file="PriceMaturityCurve{TDate}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Price.Plugins
{
    using System.Collections.Generic;
    using Abbotware.Core.Math;

    /// <summary>
    /// Price Curve
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    public class PriceMaturityCurve<TDate> : DiscreteCurve<TDate, decimal>
              where TDate : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriceMaturityCurve{TDate}"/> class.
        /// </summary>
        /// <param name="points">data points to use for the curve</param>
        public PriceMaturityCurve(params KeyValuePair<TDate, decimal>[] points)
            : base(points)
        {
        }
    }
}
