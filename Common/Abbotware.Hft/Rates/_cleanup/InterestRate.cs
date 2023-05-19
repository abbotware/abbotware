// -----------------------------------------------------------------------
// <copyright file="InterestRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates
{
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.Finance;

    /// <summary>
    /// Represents an interest rate
    /// </summary>
    /// <param name="AnnualPercentageRate">annual percentage rate</param>
    /// <param name="CompoundingFrequency">Compounding frequen for this rate</param>
    public record struct InterestRate(double AnnualPercentageRate, CompoundingFrequency CompoundingFrequency)
    {
        /// <summary>
        /// Creates a new continously compounded interest rate
        /// </summary>
        /// <param name="annualPercentageRate">annual percentage rate</param>
        /// <returns>new interest rate continuously compounded</returns>
        public static InterestRate Continuous(double annualPercentageRate) => new(annualPercentageRate, CompoundingFrequency.Continuous);

        /// <summary>
        /// Converts to an equivalent interest rate with a different compounding frequency
        /// </summary>
        /// <param name="targetFrequency">target compounding frequency</param>
        /// <returns>equivalent interest rate under the new compounding frequency</returns>
        public InterestRate Convert(CompoundingFrequency targetFrequency)
        {
            var (source, target) = (this.CompoundingFrequency, targetFrequency);

            if (source == target)
            {
                return this;
            }
            else if (source == CompoundingFrequency.Continuous)
            {
                return new InterestRate(InterestRate.ConvertContinousToPeriodic(this.AnnualPercentageRate, (ushort)target), target);
            }
            else if (target == CompoundingFrequency.Continuous)
            {
                return new InterestRate(InterestRateEquations.ConvertPeriodicToContinuous(this.AnnualPercentageRate, (ushort)target), target);
            }
            else
            {
                return new InterestRate(InterestRateEquations.ConvertPeriodicToPeriodic(this.AnnualPercentageRate, (ushort)this.CompoundingFrequency, (ushort)targetFrequency), targetFrequency);
            }
        }
    }
}
