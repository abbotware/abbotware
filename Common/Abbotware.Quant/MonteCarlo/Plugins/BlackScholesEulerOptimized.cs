namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Euler Discretization Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="r">risk free rate</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly struct BlackScholesEulerOptimized(double r, double σ, double Δt) : IStockMovement
    {
        /// <summary>
        /// Gets constant C1 for this set of parameters
        /// </summary>
        public double C1 { get; } = 1 + r * Δt;

        /// <summary>
        /// Gets constant C2 for this set of parameters
        /// </summary>
        public double C2 { get; } = σ * Math.Sqrt(Δt);

        /// <inheridoc/>
        public double Next(double Si, double Zi)
        {
            return Si * (C1 + C2 * Zi);
        }
    }

}
