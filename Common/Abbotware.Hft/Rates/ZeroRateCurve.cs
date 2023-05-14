// -----------------------------------------------------------------------
// <copyright file="ZeroRateCurve.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    using System.Collections.Generic;
    using Abbotware.Quant;

    /// <summary>
    /// Zero Rate Curve
    /// </summary>
    public class ZeroRateCurve : Curve<InterestRate>
    {
        public ZeroRateCurve(params KeyValuePair<double, InterestRate>[] points)
            : base(points)
        {
        }
    }
}
