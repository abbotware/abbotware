// -----------------------------------------------------------------------
// <copyright file="ConstantRiskFreeRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------



namespace Abbotware.Quant.Rates.Plugins
{
    using Abbotware.Quant.InterestRates;

    public record class ConstantRiskFreeRate<TDate>(double Rate) : IRiskFreeRate<TDate>
    {
        public double Lookup(TDate t) => Rate;
    }
}
