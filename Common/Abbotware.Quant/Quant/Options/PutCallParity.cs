// -----------------------------------------------------------------------
// <copyright file="PutCallParity.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Options
{
    using Abbotware.Quant.Finance.Equations;

    /// <summary>
    /// Put-Call Parity
    /// </summary>
    /// <remarks>
    /// C - P = D(F-K)   (F=Forward Price K=Strike D=Discount Factor)
    /// C - P = DF-DK
    /// C - P = Spot-DK    (Spot = DF)
    /// </remarks>
    public static class PutCallParity
    {
        /// <summary>
        /// Computes the Call from Put via Put-Call Parity
        /// </summary>
        /// <remarks>
        /// C  = P - Spot - DK
        /// </remarks>
        /// <param name="P">put price</param>
        /// <param name="S">Spot price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration in % of year</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed option premium</returns>
        public static double CallFromPut(double P, double S, double K, double τ, double r, double δ)
        {
            var DK = TimeValue.Continuous.PresentValue(K, r, τ);

            var C = P + S - DK;

            return C;
        }

        /// <summary>
        /// Computes the Put from Call via Put-Call Parity
        /// </summary>
        /// <remarks>
        /// P = - Spot + DK + C
        /// </remarks>        /// <param name="C">call price</param>
        /// <param name="S">Spot price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration in % of year</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed option premium</returns>
        public static double PutFromCall(double C, double S, double K, double τ, double r, double δ)
        {
            var DK = TimeValue.Continuous.PresentValue(K, r, τ);

            var P = -S + DK + C;

            return P;
        }
    }
}
