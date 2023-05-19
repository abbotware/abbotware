// -----------------------------------------------------------------------
// <copyright file="StrikePayoff.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

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
