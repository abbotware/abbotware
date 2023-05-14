namespace Abbotware.Quant.InterestRates
{
    using Abbotware.Quant.Enums;

    public record InterestRate(double Rate, AccrualPeriods Periods)
    {
        public static InterestRate Continuous(double rate) => new(rate, AccrualPeriods.Continuous);

        public InterestRate Convert(AccrualPeriods targetPeriods)
        {
            var (source, target) = (this.Periods, targetPeriods);

            if (source == target)
            {
                return this;
            }
            else if (source == AccrualPeriods.Continuous)
            {
                return new InterestRate(Equations.InterestRates.ConvertContinousToPeriodic(Rate, (ushort)target), target);
            }
            else if (target == AccrualPeriods.Continuous)
            {
                return new InterestRate(Equations.InterestRates.ConvertPeriodicToContinuous(Rate, (ushort)target), target);
            }
            else
            {
                return new InterestRate(Equations.InterestRates.ConvertPeriodicToPeriodic(Rate, (ushort)Periods, (ushort)targetPeriods), targetPeriods);
            }
        }
    }
}
