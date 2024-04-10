namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Exact Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="r">risk free rate</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly struct BlackScholesExactOptimized(double r, double σ, double Δt) : IStockMovement
    {
        /// <summary>
        /// Gets constant C1 for this set of parameters
        /// </summary>
        public double C1 { get; } = Math.Exp(r - (.5 * σ * σ * Δt));

        /// <summary>
        /// Gets constant C2 for this set of parameters
        /// </summary>
        public double C2 { get; } = Math.Exp(σ * Math.Sqrt(Δt));

        /// <inheridoc/>
        public double Next(double Si, double Zi)
        {
            return Si * C1 * Math.Pow(C2, Zi);
        }
    }

}
