namespace Abbotware.Quant.Statistics
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Runtime.CompilerServices;

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
    }
}
