﻿namespace Abbotware.Quant
{
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using Abbotware.Quant.Statistics;

    /// <summary>
    /// Analysis Methods
    /// </summary>
    public static class Analysis
    {
        /// <summary>
        /// Compute the Absolute Relative Pricing Error (ARPE)
        /// </summary>
        /// <remarks>
        /// The absolute relative pricing error(ARPE) is a measure used to assess the accuracy of an option pricing model, such as the Black-Scholes model, by comparing the theoretical option price generated by the model to the observed market price of the option.It quantifies the difference between the theoretical/model and actual market prices as a percentage of the market price:
        ///
        /// (Note: this the same as Absolute Percentage Error)
        /// </remarks>
        /// <typeparam name="T">numeric type</typeparam>
        /// <param name="market">actual market value</param>
        /// <param name="model">model / forcasted value</param>
        /// <returns>Absolute Percentage Error</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AbsoluteRelativePricingError<T>(T market, T model)
            where T : INumber<T>
            => ErrorAnalysis.AbsolutePercentageError(market, model);
    }
}