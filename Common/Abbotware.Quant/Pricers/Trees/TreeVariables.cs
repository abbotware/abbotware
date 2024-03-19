namespace Abbotware.Quant.Pricers.Trees
{
    public record class TreeVariables(uint Height, double T, double σ, double RiskFreeRate, double DividendYield)
    {
        public double Δt { get; } = T / (Height - 1);  // TODO: reuse this logic

        public double σ_squared { get; } = σ * σ;

        public double DiscountFactor { get; } = Finance.Equations.DiscountFactor.Continuous(RiskFreeRate - DividendYield, T / (Height - 1)); // TODO: reuse this logic
    }
}