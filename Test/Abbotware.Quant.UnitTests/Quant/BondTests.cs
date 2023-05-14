namespace Abbotware.UnitTests.Core.Quant
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Quant.Derivatives;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;
    using NUnit.Framework;

    public class BondTests
    {
        [Test]
        public void Price()
        {
            // Table 4.2 - page 84
            var zeroRateCurve = new ZeroRateCurve(
           KeyValuePair.Create(.5d, InterestRate.Continuous(.05)),
           KeyValuePair.Create(1d, InterestRate.Continuous(.058)),
           KeyValuePair.Create(1.5d, InterestRate.Continuous(.064)),
           KeyValuePair.Create(2d, InterestRate.Continuous(.068)));

            var bond = new BondModel(2, InterestRate.Continuous(.06), AccrualPeriods.SemiAnnually);

            var price = bond.Price(zeroRateCurve);

            Assert.That(price, Is.EqualTo(98.39).Within(.01));
        }

        [Test]
        public void PriceFromYield()
        {
            var bond = new BondModel(2, InterestRate.Continuous(.06), AccrualPeriods.SemiAnnually);

            var price = bond.PriceFromYield(InterestRate.Continuous(.0676));

            Assert.That(price, Is.EqualTo(98.39).Within(.01));
        }

        [Test]
        public void YieldFromPrice()
        {
            var bond = new BondModel(2, InterestRate.Continuous(.06), AccrualPeriods.SemiAnnually);

            var yield = bond.YieldFromPrice(98.39M);

            Assert.That(yield!.Rate, Is.EqualTo(.0676).Within(.00001));
        }

        [Test]
        public void ParYield()
        {
            var bond = new BondModel(2, InterestRate.Continuous(.06), AccrualPeriods.SemiAnnually);

            var zeroRateCurve = new ZeroRateCurve(
            KeyValuePair.Create(.5d, InterestRate.Continuous(.05)),
            KeyValuePair.Create(1d, InterestRate.Continuous(.058)),
            KeyValuePair.Create(1.5d, InterestRate.Continuous(.064)),
            KeyValuePair.Create(2d, InterestRate.Continuous(.068)));

            var yield = bond.ParYield(zeroRateCurve);

            Assert.That(yield!.Rate, Is.EqualTo(.0687).Within(.00001));
        }
    }
}
