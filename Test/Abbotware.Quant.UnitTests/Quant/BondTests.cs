namespace Abbotware.UnitTests.Quant
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Abbotware.Quant.Assets;
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Periodic;
    using Abbotware.Quant.Rates.Plugins;
    using Abbotware.UnitTests.Quant.Hull;
    using NUnit.Framework;

    public class BondTests
    {
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

            var bond = new Bond(maturity, Coupon.Simple(couponRate, TimePeriod.SemiAnnually));

            var price = bond.Price(zeroRateCurve);

            Assert.That(price, Is.EqualTo(xls).Within(DoublePrecision.High));
        }

        [Test]
        public void ZeroCouponBond_Price()
        {
            var zcb = new ZeroCouponBond(10) { Notional = 1000 };

            var ytm = zcb.Yield(742.47m);

            Assert.That(ytm.Rate, Is.EqualTo(.03).Within(DoublePrecision.VeryLow));
        }

        [Test]
        public void ZeroCouponBond_CouponPayment()
        {
            var zcb = new ZeroCouponBond(10) { Notional = 1000 };

            Assert.That(zcb.CouponAmount, Is.EqualTo(0));
        }

        [Test]
        public void PriceFromYield()
        {
            var bond = new Bond(2, Coupon.Simple(.06, TimePeriod.SemiAnnually));

            var price = bond.Price(new ConstantRiskFreeRate<double>(.0676));

            Assert.That(price, Is.EqualTo(98.39).Within(DoublePrecision.VeryLow));
        }

        [Test]
        public void YieldFromPrice()
        {
            new Chapters().Chapters_04_06_02_Bond_Yield();
        }

        [Test]
        public void YieldToMaturity2()
        {
            //// https://dqydj.com/bond-yield-to-maturity-calculator/

            var bond = new Bond(10, Coupon.Simple(.1, TimePeriod.SemiAnnually))
            { Notional = 1000 };

            var yield = bond.Yield(920);
            Assert.That(yield.AsYearlyPeriodic(2).Rate, Is.EqualTo(.11359).Within(DoublePrecision.Low));

            ////var yieldEstimate = bond.YieldEstimate(920);
            ////Assert.That(yieldEstimate.AsYearlyPeriodic(2).Rate, Is.EqualTo(.11250).Within(Precision.Low));

            var currentYield = bond.CurrentYield(920);
            Assert.That(currentYield.Rate, Is.EqualTo(.10870).Within(DoublePrecision.Low));
        }

        [Test]
        public void YieldToMaturity3()
        {
            //// https://investinganswers.com/calculators/yield/yield-maturity-ytm-calculator-2081

            var bond = new Bond(5, Coupon.Simple(.08, TimePeriod.SemiAnnually))
            { Notional = 1000 };

            var yield = bond.Yield(900);

            Assert.That(yield.AsYearlyPeriodic(2).Rate, Is.EqualTo(.1063).Within(DoublePrecision.Low));
        }

        [Test]
        public void Duration_Given_Price_YTM()
        {
            ////https://exploringfinance.com/calculate-bond-duration/
            var bond = new Bond(2, Coupon.Simple(.06, TimePeriod.SemiAnnually))
            { Notional = 1000 };

            ////var ytm = new Yield(.08, 2);
            var d = bond.MacaulayDuration(963.7m);

            Assert.That(d, Is.EqualTo(1.9124).Within(DoublePrecision.Low));

            Assert.Inconclusive();
        }

        [Test]
        public void Duration_Hull_4_7()
        {
            var bond = new Bond(3, Coupon.Simple(.1, TimePeriod.SemiAnnually))
            { Notional = 100 };

            var ytm = new Yield(.12, 3);
            var d = bond.MacaulayDuration(94.213m, ytm);

            Assert.That(d, Is.EqualTo(2.653).Within(DoublePrecision.Low));
        }

        [Test]
        public void Duration2()
        {
            ////https://exploringfinance.com/calculate-bond-duration/
            var bond = new Bond(2, Coupon.Simple(.06, TimePeriod.SemiAnnually))
            { Notional = 1000 };

            ////var ytm = new Yield<double>(new ContinuousRate(.08), new(0, 2));
            var d = bond.MacaulayDuration(963.7m);

            Assert.That(d, Is.EqualTo(1.9124).Within(DoublePrecision.Low));

            Assert.Inconclusive();
        }
    }
}
