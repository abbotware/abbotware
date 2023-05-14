namespace Abbotware.UnitTests.Core.Quant
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Math;
    using Abbotware.Quant.InterestRates;
    using NUnit.Framework;

    public class CurveTests
    {
        [Test]
        public void Create()
        {
            var c = new Curve<double, double>(Array.Empty<KeyValuePair<double, double>>());
            Assert.That(c, Is.Not.Null);
        }

        [Test]
        public void ExactMatches()
        {
            var c = new Curve<double, InterestRate>(
                KeyValuePair.Create(.5d, InterestRate.Continuous(.05)),
                KeyValuePair.Create(1d, InterestRate.Continuous(.058)),
                KeyValuePair.Create(1.5d, InterestRate.Continuous(.064)),
                KeyValuePair.Create(2d, InterestRate.Continuous(.068)));

            Assert.That(c.Lookup(.5d).AnnualPercentageRate, Is.EqualTo(.05));
            Assert.That(c.Lookup(1d).AnnualPercentageRate, Is.EqualTo(.058));
            Assert.That(c.Lookup(1.5d).AnnualPercentageRate, Is.EqualTo(.064));
            Assert.That(c.Lookup(2d).AnnualPercentageRate, Is.EqualTo(.068));
        }

        [Test]
        public void InteropolatedMatches()
        {
            var c = new Curve<double, InterestRate>(
                KeyValuePair.Create(.5d, InterestRate.Continuous(.05)),
                KeyValuePair.Create(1d, InterestRate.Continuous(.058)),
                KeyValuePair.Create(1.5d, InterestRate.Continuous(.064)),
                KeyValuePair.Create(2d, InterestRate.Continuous(.068)));

            Assert.That(c.Lookup(0d).AnnualPercentageRate, Is.EqualTo(.05));
            Assert.That(c.Lookup(.9d).AnnualPercentageRate, Is.EqualTo(.05));
            Assert.That(c.Lookup(1.4d).AnnualPercentageRate, Is.EqualTo(.058));
            Assert.That(c.Lookup(1.9d).AnnualPercentageRate, Is.EqualTo(.064));
            Assert.That(c.Lookup(2.1).AnnualPercentageRate, Is.EqualTo(.068));
        }
    }
}
