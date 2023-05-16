// -----------------------------------------------------------------------
// <copyright file="IRiskFreeRate{TDate}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    /// <summary>
    /// Gets the risk free rate for a given maturity t
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    public interface IRiskFreeRate<TDate>
    {
        /// <summary>
        /// looks up 
        /// </summary>
        /// <param name="t">maturity</param>
        /// <returns>rate</returns>
        public double Lookup(TDate t);
    }
}
