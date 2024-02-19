// -----------------------------------------------------------------------
// <copyright file="NewtonsMethod.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Solvers
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    /// Newton's Method
    /// </summary>
    public static class NewtonsMethod
    {
        /// <summary>
        /// Solve the function(x) for 0 using Newton's Method
        /// </summary>
        /// <param name="function">function</param>
        /// <param name="derivative">derivative</param>
        /// <param name="guess">range</param>
        /// <param name="tolerance">tolerance</param>
        /// <param name="maxIterations">max iterations to try</param>
        /// <param name="trace">optional trace of path</param>
        /// <returns>x where function(x) = 0</returns>
        public static double? Solve(Func<double, double> function, Func<double, double> derivative, double guess, double tolerance, uint maxIterations = SolverConstants.DefaultMaxIterations, Point<double, double>[]? trace = null)
        {
            var i = 0;
            var next = guess;

            do
            {
                var f = function(next);
                if (f == 0)
                {
                    return next;
                }

                var d = derivative(next);

                if (d == 0)
                {
                    return null;
                    // throw new ArgumentOutOfRangeException(nameof(derivative), $"derivative({next}) = 0 for iteration:{i}");
                }

                next = next - (f / d);

                var v = function(next);

                if (trace is not null)
                {
                    trace[i] = new(next, v);
                }

                // && Math.Abs(f / d) < tolerance <- not sure why we need to check this too
                if (Math.Abs(v) < tolerance && Math.Abs(f / d) < tolerance)
                {
                    return next;
                }

                ++i;
            }
            while (i < maxIterations);

            return null;
            // throw new ArgumentException("Does Not Converge");
        }
    }
}
