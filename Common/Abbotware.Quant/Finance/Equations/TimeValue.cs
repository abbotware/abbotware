﻿namespace Abbotware.Quant.Finance.Equations
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Time Value of Money Functions
    /// </summary>
    public static class TimeValue
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
        }

        /// <summary>
        /// Continuous Time Value of Money Functions
        /// </summary>
        public static class Continuous
        {
            /// <summary>
            /// Computes the future value given the present value
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="r">interest rate per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>future value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static decimal FutureValue(decimal presentValue, double r, double t)
                => presentValue * (decimal)CompoundingFactor.Continuous(r, t);

            /// <summary>
            /// Computes the present value given the future value
            /// </summary>
            /// <param name="futureValue">future value</param>
            /// <param name="r">interest rate per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>present value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static decimal PresentValue(decimal futureValue, double r, double t)
                => futureValue * (decimal)DiscountFactor.Continuous(r, t);

            /// <summary>
            /// Computes the future value given the present value
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="r">interest rate per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>future value</returns>

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static double FutureValue(double presentValue, double r, double t)
                => presentValue * CompoundingFactor.Continuous(r, t);

            /// <summary>
            /// Computes the present value given the future value
            /// </summary>
            /// <param name="futureValue">future value</param>
            /// <param name="r">interest rate per unit of time</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>present value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static double PresentValue(double futureValue, double r, double t)
                => futureValue * DiscountFactor.Continuous(r, t);
        }
    }
}