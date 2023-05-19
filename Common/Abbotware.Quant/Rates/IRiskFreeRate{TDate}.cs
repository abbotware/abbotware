// -----------------------------------------------------------------------
// <copyright file="IRiskFreeRate{TDate}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    using Abbotware.Core.Math;

    /// <summary>
    /// Gets the risk free rate for a given maturity t
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    public interface IRiskFreeRate<TDate> : IFittedCurve<TDate, double>
       where TDate : notnull
    {
    }
}