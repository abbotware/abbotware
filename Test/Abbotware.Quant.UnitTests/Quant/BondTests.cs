namespace Abbotware.UnitTests.Core.Quant
{
    using System.Collections.Generic;
    using Abbotware.Quant.Assets;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Periodic;
    using Abbotware.Quant.Rates.Plugins;
    using Abbotware.Quant.UnitTests;
    using NUnit.Framework;

    public class BondTests
    {
        [Test]
        public void Price_Lecture02_Slide21()
        {
            // Table 4.2 - page 84
            var zeroRateCurve = new ZeroRateCurve<double>(
           KeyValuePair.Create(.5d, .05),
           KeyValuePair.Create(1d, .058),
           KeyValuePair.Create(1.5d, .064),
           KeyValuePair.Create(2d, .068));

            var bond = new Bond(2, new NominalRate(.06, TimePeriod.Annually), new SimplePeriodic<double>(TimePeriod.SemiAnnually));

            var price = bond.Price(zeroRateCurve);

            Assert.That(price, Is.EqualTo(98.39).Within(Precision.VeryLow));
        }

        [TestCase(.015, 1, 98.5112214597145d)]
        [TestCase(.025, 2, 98.0353890082365d)]
        [TestCase(.03, 5, 95.3791749943793d)]
        [TestCase(.04, 10, 97.7993896923126d)]
        public void Price_Lecture02_BondPricing_xls(double couponRate, double maturity, double xls)
        {
            var zeroRateCurve = new ZeroRateCurve<double>(
            KeyValuePair.Create(1d, .03),
            KeyValuePair.Create(2d, .035),
            KeyValuePair.Create(5d, .04),
            KeyValuePair.Create(10d, .0425));

            var bond = new Bond(maturity, new NominalRate(couponRate, TimePeriod.Annually), new SimplePeriodic<double>(TimePeriod.SemiAnnually));

            var price = bond.Price(zeroRateCurve);

            Assert.That(price, Is.EqualTo(xls).Within(Precision.High));
        }

        [Test]
        public void PriceFromYield()
        {
            var bond = new Bond(2, new NominalRate(.06, TimePeriod.Annually), new SimplePeriodic<double>(TimePeriod.SemiAnnually));

            var price = bond.Price(new ConstantRiskFreeRate<double>(.0676));

            Assert.That(price, Is.EqualTo(98.39).Within(Precision.VeryLow));
        }

        [Test]
        public void YieldFromPrice()
        {
            var bond = new Bond(2, new NominalRate(.06, TimePeriod.Annually), new SimplePeriodic<double>(TimePeriod.SemiAnnually));

            var yield = bond.YieldFromPrice(98.39M);

            Assert.That(yield!.Rate, Is.EqualTo(.0676).Within(Precision.Medium));
        }

        [Test]
        public void ParYield()
        {
            var bond = new Bond(2, new NominalRate(.06, TimePeriod.Annually), new SimplePeriodic<double>(TimePeriod.SemiAnnually));

            var zeroRateCurve = new ZeroRateCurve<double>(
            KeyValuePair.Create(.5d, .05),
            KeyValuePair.Create(1d, .058),
            KeyValuePair.Create(1.5d, .064),
            KeyValuePair.Create(2d, .068));

            var yield = bond.ParYield(zeroRateCurve);

            Assert.That(yield.Rate, Is.EqualTo(.0687).Within(Precision.Low));
        }
    }
}
