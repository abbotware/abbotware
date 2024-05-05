// -----------------------------------------------------------------------
// <copyright file="ErrorAnalysis.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Math;
    using MathNet.Numerics.Distributions;

    /// <summary>
    /// Statistics Error Analysis
    /// </summary>
    public static class ErrorAnalysis
    {
        /// <summary>
        /// Compute the Absolute Percentage Error
        /// </summary>
        /// <typeparam name="T">numeric type</typeparam>
        /// <param name="actual">actual value</param>
        /// <param name="forecast">forcated value</param>
        /// <returns>Absolute Percentage Error</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AbsolutePercentageError<T>(T actual, T forecast)
             where T : INumber<T>
        {
            var delta = actual - forecast;
            var abs = T.Abs(delta);
            var e = abs / actual;
            return e;
        }

        /// <summary>
        /// Compute the Mean Absolute Percentage Error (MAPE) / Mean Absolute Percentage Deviation (MAPD)
        /// </summary>
        /// <remarks>https://en.wikipedia.org/wiki/Mean_absolute_percentage_error</remarks>
        /// <typeparam name="T">numeric type</typeparam>
        /// <param name="actuals">actual values</param>
        /// <param name="forecasts">forecasted values</param>
        /// <returns>Mean Absolute Percentage Error</returns>
        public static T MeanAbsolutePercentageError<T>(IEnumerable<T> actuals, IEnumerable<T> forecasts)
             where T : INumber<T>
        {
            var sum = T.Zero;
            var i = 0;

            foreach (var (a, f) in actuals.Zip(forecasts))
            {
                sum += AbsolutePercentageError(a, f);
                ++i;
            }

            var n = T.CreateChecked(i);
            return sum / n;
        }

        /// <summary>
        /// Calculates the Standard Error (SE)
        /// </summary>
        /// <typeparam name="T">numeric type</typeparam>
        /// <param name="standardDeviation">standard deviation of the population or sample</param>
        /// <param name="n">size of the population or sample</param>
        /// <returns>standard error</returns>
        public static T StandardError<T>(T standardDeviation, double n)
            where T : INumber<T>
            => standardDeviation / T.CreateChecked(Math.Sqrt(n));

        /// <summary>
        /// Calculates the Confidence Interval for a critical value given the mean and standard error
        /// </summary>
        /// <typeparam name="T">numeric type</typeparam>
        /// <param name="mean">mean of the population or sample</param>
        /// <param name="criticalValue">critical value</param>
        /// <param name="standardError">standard error</param>
        /// <returns>computed confidence interval</returns>
        public static Interval<T> ConfidenceInterval<T>(T mean, T criticalValue, T standardError)
            where T : INumber<T>
         => new(mean - (criticalValue * standardError), mean + (criticalValue * standardError));

        /// <summary>
        /// Normal Distribution Critical Value Calculation
        /// </summary>
        /// <param name="confidenceLevel">confidence level</param>
        /// <returns>critical value for the supplied confidence level</returns>
        public static double NormalCriticalValue(double confidenceLevel)
            => Normal.InvCDF(0, 1, (1 - (1 - confidenceLevel)) / 2);
    }
}
