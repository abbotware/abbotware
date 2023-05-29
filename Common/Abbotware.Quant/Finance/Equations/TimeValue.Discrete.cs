// -----------------------------------------------------------------------
// <copyright file="TimeValue.Discrete.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Equations
{
    using System;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Math;

    /// <summary>
    /// Time Value of Money Functions
    /// </summary>
    public static partial class TimeValue
    {
        /// <summary>
        /// Discrete Time Value of Money Functions
        /// </summary>
        public static class Discrete
        {
            /// <summary>
            /// Computes the future value given the present value
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="r">rate for the peroid</param>
            /// <param name="n">number of periods</param>
            /// <returns>future value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static decimal FutureValue(decimal presentValue, double r, double n)
                => presentValue * (decimal)CompoundingFactor.Discrete(r, n);

            /// <summary>
            /// Computes the present value given the future value
            /// </summary>
            /// <param name="futureValue">future value</param>
            /// <param name="r">rate for the peroid</param>
            /// <param name="n">number of periods</param>
            /// <returns>present value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static decimal PresentValue(decimal futureValue, double r, double n)
                => futureValue * (decimal)DiscountFactor.Discrete(r, n);

            /// <summary>
            /// Computes the future value of a given present value with compounded periods over a unit of time
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="r">interest rate per unit of time</param>
            /// <param name="n">number of compounding periods per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>future value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static decimal FutureValue(decimal presentValue, double r, double n, double t)
                => FutureValue(presentValue, r / n, t * n);

            /// <summary>
            /// Computes the present value of a given future value with compounded periods over a unit of time
            /// </summary>
            /// <param name="futureValue">prevent value</param>
            /// <param name="r">interest rate per unit of time</param>
            /// <param name="n">number of compounding periods per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>present value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static decimal PresentValue(decimal futureValue, double r, double n, double t)
                => PresentValue(futureValue, r / n, t * n);

            /// <summary>
            /// Determines the continuous rate
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="futureValue">future value</param>
            /// <param name="n">number of compounding periods per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>rate value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static double Rate(double presentValue, double futureValue, double n, double t)
                => (Functions.NthRoot(futureValue / presentValue, n * t) - 1) / n;

            /// <summary>
            /// Determines the continuous rate
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="futureValue">future value</param>
            /// <param name="n">number of compounding periods per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>rate value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static double Rate(decimal presentValue, decimal futureValue, double n, double t)
                => (Functions.NthRoot((double)(futureValue / presentValue), n * t) - 1) / n;
        }
    }
}
