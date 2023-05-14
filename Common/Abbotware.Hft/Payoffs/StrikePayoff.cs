namespace Abbotware.Quant.Payoffs
{
    /// <summary>
    /// Strike based Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    public abstract record class StrikePayoff(decimal Strike) : BasePayoff()
    {
    }
}
