// -----------------------------------------------------------------------
// <copyright file="NewtonsMethod.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Solvers
{
    using System;

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
        /// <returns>x where function(x) = 0</returns>
        public static double? Solve(Func<double, double> function, Func<double, double> derivative, double guess, double tolerance, uint maxIterations)
        {
            var i = 0;
            var next = guess;

            do
            {
                var f = function(next);
                var d = derivative(next);

                if (d == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(derivative), $"derivative({next}) = 0 for iteration:{i}");
                }

                next = f - (f / d);

                if (Math.Abs(next) < tolerance)
                {
                    return next;
                }

                ++i;
            }
            while (i < maxIterations);

            throw new ArgumentException("Does Not Converge");
        }
    }
}
