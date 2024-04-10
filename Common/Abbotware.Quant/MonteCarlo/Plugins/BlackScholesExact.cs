namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Exact Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="r">risk free rate</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly struct BlackScholesExact(double r, double σ, double Δt) : IStockMovement
    {
        /// <inheridoc/>
        public double Next(double Si, double Zi)
        {
            var a = (r - σ * σ * .5) * Δt;
            var b = σ * Math.Sqrt(Δt) * Zi;
            return Si * Math.Exp(a + b);
        }
    }

}
