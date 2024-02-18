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
        /// <param name="xRange">range</param>
        /// <param name="target">target</param>
        /// <param name="tolerance">tolerance</param>
        /// <param name="strictlyMonotoniclyIncreasing">mimics other root finding where the function has to cross zero</param>
        /// <param name="maxIterations">max iterations</param>
        /// <param name="trace">optional trace</param>
        /// <returns>value</returns>
        /// <exception cref="ArgumentOutOfRangeException">target is not within the interval range</exception>
        public static double? Solve(Func<double, double> func, Interval<double> xRange, double target, double tolerance, bool strictlyMonotoniclyIncreasing = false, uint maxIterations = SolverConstants.DefaultMaxIterations, double[]? trace = null)
        {
            var u = xRange.Upper;
            var l = xRange.Lower;
            var iterations = 0;

            var upper = func(u);
            if (upper == target)
            {
                return upper;
            }

            var lower = func(l);
            if (lower == target)
            {
                return lower;
            }

            if (strictlyMonotoniclyIncreasing)
            {
                if (Math.Sign(lower) != -1)
                {
                    return null;
                }

                if (Math.Sign(upper) != 1)
                {
                    return null;
                }
            }

            var yRange = new Interval<double>(lower, upper);

            if (!yRange.Within(target))
            {
                throw new ArgumentOutOfRangeException($"target:{target} not within range:{yRange}");
            }

            while (iterations < maxIterations)
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
                if (mid <= target && target >= lower)
                {
                    l = m;
                    lower = mid;
                }

                // Func is Decreasing
                // | L ----T----- M ----------- U |
                //  100   75      50         -100
                else if (mid <= target && target <= lower)
                {
                    u = m;
                    upper = mid;
                }

                // Func is Increasing
                // | L -----T----- M ---------- U |
                //  -100   25     50            100
                else if (mid >= target && target >= lower)
                {
                    u = m;
                    upper = mid;
                }

                // Func is Decreasing
                // | L ---------- M -----T------ U |
                //  100          50    25    -100
                else if (mid >= target && target <= lower)
                {
                    l = m;
                    lower = mid;
                }
                else
                {
                    throw new NotSupportedException("bug");
                }

                ++iterations;
            }

            return null;
        }
    }
}
