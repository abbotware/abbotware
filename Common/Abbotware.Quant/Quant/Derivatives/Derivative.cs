// -----------------------------------------------------------------------
// <copyright file="Derivative.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Derivatives
{
    using Abbotware.Quant.Assets;

    /// <summary>
    /// Base Model for a Derivative Security
    /// </summary>
    /// <typeparam name="TUnderlying">type of the underlying</typeparam>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Maturity">Maturity Date</param>
    public abstract record Derivative<TUnderlying, TDate>(TDate Maturity)
        : Asset<TDate>(Maturity)
    {
    }

    /// <summary>
    /// Forward Contract
    /// </summary>
    /// <typeparam name="TUnderlying">type of the underlying</typeparam>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Maturity">Maturity Date</param>
    public record Forward<TUnderlying, TDate>(TDate Maturity) : Derivative<TUnderlying, TDate>(Maturity)
    {
    }

    /// <summary>
    /// Forward Rate Agreement Contract
    /// </summary>
    /// <typeparam name="TUnderlying">type of the underlying</typeparam>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Maturity">Maturity Date</param>
    public record ForwardRateAgreement<TUnderlying, TDate>(TDate Maturity) : Forward<TUnderlying, TDate>(Maturity)
    {
    }
}
