// -----------------------------------------------------------------------
// <copyright file="Asset.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    /// <summary>
    ///  base model for an asset class
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Maturity">Maturity</param>
    public abstract record Asset<TDate>(TDate Maturity)
    {
    }
}