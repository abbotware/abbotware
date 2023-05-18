namespace Abbotware.UnitTests.Core.Quant
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Math;
    using Abbotware.Quant.Rates;
    using NUnit.Framework;

    public class CurveTests
    {
        [Test]
        public void Create()
        {
            var c = new DiscreteCurve<double, double>(Array.Empty<KeyValuePair<double, double>>());
            Assert.That(c, Is.Not.Null);
        }

        [Test]
        public void ExactMatches()
        {
            var c = new DiscreteCurve<double, InterestRate>(
                KeyValuePair.Create(.5d, InterestRate.Continuous(.05)),
                KeyValuePair.Create(1d, InterestRate.Continuous(.058)),
                KeyValuePair.Create(1.5d, InterestRate.Continuous(.064)),
                KeyValuePair.Create(2d, InterestRate.Continuous(.068)));

            Assert.That(c.GetPoint(.5d).AnnualPercentageRate, Is.EqualTo(.05));
            Assert.That(c.GetPoint(1d).AnnualPercentageRate, Is.EqualTo(.058));
            Assert.That(c.GetPoint(1.5d).AnnualPercentageRate, Is.EqualTo(.064));
            Assert.That(c.GetPoint(2d).AnnualPercentageRate, Is.EqualTo(.068));
        }

        [Test]
        public void InteropolatedMatches()
        {
            var c = new DiscreteCurve<double, InterestRate>(
                KeyValuePair.Create(.5d, InterestRate.Continuous(.05)),
                KeyValuePair.Create(1d, InterestRate.Continuous(.058)),
                KeyValuePair.Create(1.5d, InterestRate.Continuous(.064)),
                KeyValuePair.Create(2d, InterestRate.Continuous(.068)));

            Assert.That(c.Nearest(0d).AnnualPercentageRate, Is.EqualTo(.05));
            Assert.That(c.Nearest(.9d).AnnualPercentageRate, Is.EqualTo(.058));
            Assert.That(c.Nearest(1.4d).AnnualPercentageRate, Is.EqualTo(.064));
            Assert.That(c.Nearest(1.5d).AnnualPercentageRate, Is.EqualTo(.064));
            Assert.That(c.Nearest(1.6d).AnnualPercentageRate, Is.EqualTo(.068));
            Assert.That(c.Nearest(1.9d).AnnualPercentageRate, Is.EqualTo(.068));
            Assert.That(c.Nearest(2.0).AnnualPercentageRate, Is.EqualTo(.068));
        }

        [Test]
        [TestCase(0, ExpectedResult = .03)]
        [TestCase(0.5, ExpectedResult = .03)]
        [TestCase(1, ExpectedResult = .03)]
        [TestCase(1.5, ExpectedResult = .035)]
        [TestCase(2, ExpectedResult = .035)]
        [TestCase(2.5, ExpectedResult = .04)]
        [TestCase(3, ExpectedResult = .04)]
        [TestCase(3.5, ExpectedResult = .04)]
        [TestCase(4, ExpectedResult = .04)]
        [TestCase(4.5, ExpectedResult = .04)]
        [TestCase(5, ExpectedResult = .04)]
        [TestCase(5.5, ExpectedResult = .0425)]
        [TestCase(6, ExpectedResult = .0425)]
        [TestCase(6.5, ExpectedResult = .0425)]
        [TestCase(7, ExpectedResult = .0425)]
        [TestCase(7.5, ExpectedResult = .0425)]
        [TestCase(8, ExpectedResult = .0425)]
        [TestCase(8.5, ExpectedResult = .0425)]
        [TestCase(9, ExpectedResult = .0425)]
        [TestCase(9.5, ExpectedResult = .0425)]
        [TestCase(10, ExpectedResult = .0425)]
        public double ExhaustiveMatches(double t)
        {
            var c = new DiscreteCurve<double, InterestRate>(
                KeyValuePair.Create(1d, InterestRate.Continuous(.03)),
                KeyValuePair.Create(2d, InterestRate.Continuous(.035)),
                KeyValuePair.Create(5d, InterestRate.Continuous(.04)),
                KeyValuePair.Create(10d, InterestRate.Continuous(.0425)));

            return c.Nearest(t).AnnualPercentageRate;
        }
    }
}
