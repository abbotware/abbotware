// -----------------------------------------------------------------------
// <copyright file="IMaturityPrice{TDate}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Price
{
    /// <summary>
    /// Gets the price for a given maturity t
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    public interface IMaturityPrice<TDate>
    {
        /// <summary>
        /// looks ups price for maturirty
        /// </summary>
        /// <param name="t">maturity</param>
        /// <returns>rate</returns>
        public decimal Lookup(TDate t);
    }
}
