// -----------------------------------------------------------------------
// <copyright file="BlackScholes.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant
{
    using System;
    using Abbotware.Quant.Options;
    using MathNet.Numerics.Distributions;
    using MathNet.Numerics.RootFinding;

    /// <summary>
    /// Black Scholes Equations for European Options
    /// </summary>
    public static class BlackScholes
    {
        /// <summary>
        /// Computes theoretical option price.
        /// </summary>
        /// <param name="optionType">call or put</param>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration in % of year</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed option premium</returns>
        public static double Premium(ContractType optionType, double S, double K, double τ, double σ, double r, double δ)
        {
            var (d1, d2) = D(S, K, τ, σ, r, δ);

            return optionType switch
            {
                ContractType.Call => (S * Math.Exp(-δ * τ) * Normal.CDF(0, 1, d1)) - (K * Math.Exp(-r * τ) * Normal.CDF(0, 1, d2)),
                ContractType.Put => (K * Math.Exp(-r * τ) * Normal.CDF(0, 1, -d2)) - (S * Math.Exp(-δ * τ) * Normal.CDF(0, 1, -d1)),
                _ => throw new NotSupportedException(" Option Type Error:" + optionType + " does not exist!"),
            };
        }

        /// <summary>
        /// Computes the implied volatility provide market price for an option
        /// </summary>
        /// <param name="optionType">call or put</param>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration in % of year</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <param name="marketPrice">option's market price</param>
        /// <returns>computed option premium</returns>
        public static double ImpliedVolatility(ContractType optionType, double S, double K, double τ, double r, double δ, double marketPrice)
        {
            double F(double x) => Premium(optionType, S, K, τ, x, r, δ) - marketPrice;
            double Derivative(double x) => Vega(S, K, τ, x, r, δ);

            return RobustNewtonRaphson.FindRoot(F, Derivative, lowerBound: 0, upperBound: 100, accuracy: 0.001);
        }

        /// <summary>
        /// Computes Vega. The amount of option price change for each 1% change in vol (sigma)
        /// </summary>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration in % of year</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed Vega</returns>
        public static double Vega(double S, double K, double τ, double σ, double r, double δ)
        {
            double d1 = D1(S, K, τ, σ, r, δ);
            double vega = S * Math.Exp(-δ * τ) * Normal.PDF(0, 1, d1) * Math.Sqrt(τ);
            return vega / 100;
        }

        /// <summary>
        /// Computes theta.
        /// </summary>
        /// <param name="optionType">call or put</param>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration (% of year)</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed theta</returns>
        public static double Theta(ContractType optionType, double S, double K, double τ, double σ, double r, double δ)
        {
            var (d1, d2) = D(S, K, τ, σ, r, δ);

            switch (optionType)
            {
                case ContractType.Call:
                    {
                        double theta = -(Math.Exp(-δ * τ) * (S * Normal.PDF(0, 1, d1) * σ) / (2.0 * Math.Sqrt(τ)))
                                - (r * K * Math.Exp(-r * τ) * Normal.CDF(0, 1, d2))
                                + (δ * S * Math.Exp(-δ * τ) * Normal.CDF(0, 1, d1));

                        return theta;
                    }

                case ContractType.Put:
                    {
                        double theta = -(Math.Exp(-δ * τ) * (S * Normal.PDF(0, 1, d1) * σ) / (2.0 * Math.Sqrt(τ)))
                            + (r * K * Math.Exp(-r * τ) * Normal.PDF(0, 1, -d2))
                            - (δ * S * Math.Exp(-δ * τ) * Normal.CDF(0, 1, -d1));

                        return theta;
                    }

                default:
                    throw new NotSupportedException(" Option Type Error:" + optionType + " does not exist!");
            }
        }

        /// <summary>
        /// Computes delta.
        /// </summary>
        /// <param name="optionType">call or put</param>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration (% of year)</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed delta</returns>
        public static double Delta(ContractType optionType, double S, double K, double τ, double σ, double r, double δ)
        {
            double d1 = D1(S, K, τ, σ, r, δ);

            return optionType switch
            {
                ContractType.Call => Math.Exp(-r * τ) * Normal.CDF(0, 1, d1),
                ContractType.Put => -Math.Exp(-r * τ) * Normal.CDF(0, 1, -d1),
                _ => throw new NotSupportedException(" Option Type Error:" + optionType + " does not exist!"),
            };
        }

        /// <summary>
        /// Computes gamma.
        /// </summary>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration (% of year)</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed gamma</returns>
        public static double Gamma(double S, double K, double τ, double σ, double r, double δ)
        {
            double d1 = D1(S, K, τ, σ, r, δ);
            return Math.Exp(-δ * τ) * (Normal.PDF(0, 1, d1) / (S * σ * Math.Sqrt(τ)));
        }

        /// <summary>
        /// Computes delta.
        /// </summary>
        /// <param name="optionType">call or put</param>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration (% of year)</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>computed rho</returns>
        public static double Rho(ContractType optionType, double S, double K, double τ, double σ, double r, double δ)
        {
            var (_, d2) = D(S, K, τ, σ, r, δ);

            return optionType switch
            {
                ContractType.Call => K * τ * Math.Exp(-r * τ) * Normal.CDF(0, 1, d2) / 100,
                ContractType.Put => -K * τ * Math.Exp(-r * τ) * Normal.CDF(0, 1, -d2) / 100,
                _ => throw new NotSupportedException(" Option Type Error:" + optionType + " does not exist!"),
            };
        }

        /// <summary>
        /// Computes the D+/- of the Black-Scholes equation
        /// </summary>
        /// <param name="S">Underlying price</param>
        /// <param name="K">Strike price</param>
        /// <param name="τ">Time to expiration (% of year)</param>
        /// <param name="σ">Volatility</param>
        /// <param name="r">continuously compounded risk-free interest rate</param>
        /// <param name="δ">continuously compounded dividend yield</param>
        /// <returns>D+ and D-</returns>
        public static (double D1, double D2) D(double S, double K, double τ, double σ, double r, double δ)
        {
            var σSqrtτ = VolSqrtτ(τ: τ, σ: σ);
            var d1 = D1(S, K, τ, σ, r, δ, σSqrtτ);
            var d2 = D2(d1, σSqrtτ);

            return (d1, d2);
        }

        private static double D1(double S, double K, double τ, double σ, double r, double δ)
        {
            return D1(S, K, τ, σ, r, δ, VolSqrtτ(τ: τ, σ: σ));
        }

        private static double D1(double S, double K, double τ, double σ, double r, double δ, double σSqrtτ)
        {
            return (Math.Log(S / K) + ((r - δ + (σ * σ / 2)) * τ)) / σSqrtτ;
        }

        private static double D2(double τ, double σ, double d1)
        {
            return D2(d1, VolSqrtτ(τ: τ, σ: σ));
        }

        private static double D2(double d1, double σSqrtτ)
        {
            return d1 - σSqrtτ;
        }

        private static double VolSqrtτ(double τ, double σ) => σ * Math.Sqrt(τ);
    }
}
