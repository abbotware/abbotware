// -----------------------------------------------------------------------
// <copyright file="StrikePayoff{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs
{
    using System.Numerics;

    /// <summary>
    /// Strike based Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    /// <typeparam name="T">number type</typeparam>
    public abstract record class StrikePayoff<T>(T Strike)
        : BasePayoff<T>()
       where T : INumber<T>
    {
    }
}
