namespace Abbotware.UnitTests.Quant.Hull
{
    using System.Collections.Generic;
    using Abbotware.Quant.Assets;
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Rates.Plugins;
    using Abbotware.UnitTests.Quant;
    using NUnit.Framework;

    public class Chapters
    {
        [Test]
        public void Chapters_04_05_Zero_Rates()
        {
            var equationResult = TimeValue.Continuous.FutureValue(100m, .05, 5);

            Assert.That(equationResult, Is.EqualTo(128.40).Within(Precision.VeryLow));
        }

        [Test]
        public void Chapters_04_06_01_Bond_Pricing()
        {
            // Lecture 02 Slide 21
            // Table 4.2 - page 84
            var zeroRateCurve = new ZeroRateCurve<double>(
           KeyValuePair.Create(.5d, .05),
           KeyValuePair.Create(1d, .058),
           KeyValuePair.Create(1.5d, .064),
           KeyValuePair.Create(2d, .068));

            var bond = new Bond(2, Coupon.Simple(.06, TimePeriod.SemiAnnually));

            var price = bond.Price(zeroRateCurve);

            Assert.That(price, Is.EqualTo(98.39).Within(Precision.VeryLow));
        }

        [Test]
        public void Chapters_04_06_02_Bond_Yield()
        {
            var bond = new Bond(2, Coupon.Simple(.06, TimePeriod.SemiAnnually));

            var price = bond.Yield(98.39m);

            Assert.That(price.Rate, Is.EqualTo(.0676).Within(Precision.Low));
        }

        [Test]
        public void Chapters_04_06_03_Par_Yield()
        {
            // Lecture 02 Slide 23
            var bond = new Bond(2, Coupon.Simple(.06, TimePeriod.SemiAnnually));

            var zeroRateCurve = new ZeroRateCurve<double>(
            KeyValuePair.Create(.5d, .05),
            KeyValuePair.Create(1d, .058),
            KeyValuePair.Create(1.5d, .064),
            KeyValuePair.Create(2d, .068));

            var yield = bond.ParYield(zeroRateCurve);

            Assert.That(yield.Rate, Is.EqualTo(.0687).Within(Precision.Low));
        }

        [Test]
        public void Chapters_04_07_01_Treasury_Rates()
        {
            var r1 = TimeValue.Continuous.Rate(99.6m, 100m, .25);
            var r1a = InterestRate.ContinousToPeriodic(r1, 4);
            Assert.That(r1, Is.EqualTo(.01603).Within(Precision.Medium));
            Assert.That(r1a, Is.EqualTo(.016064).Within(Precision.Medium));

            var r2 = TimeValue.Continuous.Rate(99m, 100m, .5);
            var r2a = InterestRate.ContinousToPeriodic(r2, 2);
            Assert.That(r2, Is.EqualTo(.02010).Within(Precision.Medium));
            Assert.That(r2a, Is.EqualTo(.020202).Within(Precision.Medium));

            var r3 = TimeValue.Continuous.Rate(97.8m, 100m, 1);
            var r3a = InterestRate.ContinousToPeriodic(r3, 1);
            Assert.That(r3, Is.EqualTo(.02225).Within(Precision.Medium));
            Assert.That(r3a, Is.EqualTo(.022495).Within(Precision.Medium));

            var b1 = new ZeroCouponBond(.25);
            Assert.That(b1.Yield(99.6m).Rate, Is.EqualTo(r1));
            Assert.That(b1.Yield(99.6m).AsYearlyPeriodic(4).Rate, Is.EqualTo(r1a));

            var b2 = new ZeroCouponBond(.5);
            Assert.That(b2.Yield(99m).Rate, Is.EqualTo(r2));
            Assert.That(b2.Yield(99m).AsYearlyPeriodic(2).Rate, Is.EqualTo(r2a));

            var b3 = new ZeroCouponBond(1);
            Assert.That(b3.Yield(97.8m).Rate, Is.EqualTo(r3));
            Assert.That(b3.Yield(97.8m).AsYearlyPeriodic(1).Rate, Is.EqualTo(r3a));

            var b4 = new Bond(1.5, Coupon.Simple(.04, TimePeriod.SemiAnnually));

            var b5 = new Bond(2, Coupon.Simple(.05, TimePeriod.SemiAnnually));

        }
    }
}