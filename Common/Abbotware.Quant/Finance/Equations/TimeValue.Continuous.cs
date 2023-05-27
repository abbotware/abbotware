namespace Abbotware.Quant.Finance.Equations
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Time Value of Money Functions
    /// </summary>
    public static partial class TimeValue
    {
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

            /// <summary>
            /// Determines the continuous rate
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="futureValue">future value</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>rate value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static double Rate(double presentValue, double futureValue, double t)
                => Math.Log(futureValue / presentValue) / t;

            /// <summary>
            /// Determines the continuous rate
            /// </summary>
            /// <param name="presentValue">prevent value</param>
            /// <param name="futureValue">future value</param>
            /// <param name="t">unit(s) of time</param>
            /// <returns>rate value</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static double Rate(decimal presentValue, decimal futureValue, double t)
                => Math.Log((double)(futureValue / presentValue)) / t;
        }
    }
}
