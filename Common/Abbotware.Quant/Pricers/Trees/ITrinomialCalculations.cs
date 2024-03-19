namespace Abbotware.Quant.Pricers.Trees
{
    public interface ITrinomialCalculations : IBinomialCalculations
    {
        /// <summary>
        /// Gets the probability of the middle path
        /// </summary>
        double MiddleProbability { get; }

        /// <summary>
        /// Gets the shift of the middle path
        /// </summary>
        double MiddleShift { get; }
    }
}