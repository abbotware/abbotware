namespace Abbotware.Quant.MonteCarlo
{
    /// <summary>
    /// Stock Price movement simulator
    /// </summary>
    public interface IStockMovement
    {
        /// <summary>
        /// Computes the next movement of the stock price
        /// </summary>
        /// <param name="Si">intiial position</param>
        /// <param name="Zi">random normal variable</param>
        /// <returns>Stock price moving a time incerment of Δt</returns>
        double Next(double Si, double Zi);
    }
}
