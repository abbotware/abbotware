// -----------------------------------------------------------------------
// <copyright file="Bisection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Solvers
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    /// Bisection Numerical Method
    /// </summary>
    public static class Bisection
    {
        /// <summary>
        /// Solve the function for the targe value
        /// </summary>
        /// <param name="func">function</param>
        /// <param name="range">range</param>
        /// <param name="target">target</param>
        /// <param name="tolerance">tolerance</param>
        /// <returns>value</returns>
        /// <exception cref="ArgumentOutOfRangeException">target is not within the interval range</exception>
        public static double? Solve(Func<double, double> func, Interval<double> range, double target, double tolerance)
        {
            var u = range.Upper;
            var l = range.Lower;
            var iterations = 0;

            var upper = func(u);
            var lower = func(l);

            var values = new Interval<double>(upper, lower);

            if (!values.Within(target))
            {
                throw new ArgumentOutOfRangeException($"target:{target} not within range:{range}");
            }

            while (iterations < 1000)
            {
                var m = (u + l) / 2;
                var mid = func(m);

                if (Math.Abs(mid - target) < tolerance)
                {
                    return m;
                }

                // Func is Increasing
                // | L ---------- M ----T------ U |
                //  -100          50    75      100
                if (mid < target && target > lower)
                {
                    l = m;
                    lower = mid;
                }

                // Func is Decreasing
                // | L ----T----- M ----------- U |
                //  100   75      50         -100
                else if (mid < target && target < lower)
                {
                    u = m;
                    upper = mid;
                }

                // Func is Increasing
                // | L -----T----- M ---------- U |
                //  -100   25     50            100
                else if (mid > target && target > lower)
                {
                    u = m;
                    upper = mid;
                }

                // Func is Decreasing
                // | L ---------- M -----T------ U |
                //  100          50    25    -100
                else if (mid > target && target < lower)
                {
                    l = m;
                    lower = mid;
                }

                ++iterations;
            }

            return null;
        }
    }
}
