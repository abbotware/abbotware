namespace Abbotware.UnitTests.Quant
{
    using Abbotware.Quant.Pricers.Trees;
    using Abbotware.Quant.Pricers.Trees.Plugins;
    using NUnit.Framework;

    public class TreeCalculationTests
    {
        public const double VeryHighTolerance = .0000000000001;
        public const double HighTolerance = .000000001;
        public const double LowTolerance = .0001;

        [Test]
        public void Binomial_Lecture()
        {
            var bv = new TreeVariables(1d / 12d, .4, .10, 0);
            var b = new CoxRubenstienRossBinomial(bv);

            Assert.Multiple(() =>
            {
                // sanity checks:
                Assert.That(b.UpShift, Is.EqualTo(1 / b.DownShift).Within(HighTolerance));
                Assert.That(b.UpProbability, Is.EqualTo(1 - b.DownProbability).Within(HighTolerance));

                // actual data checks
                Assert.That(b.UpShift, Is.EqualTo(1.1224).Within(LowTolerance));
                Assert.That(b.DownShift, Is.EqualTo(.8909).Within(LowTolerance));
                Assert.That(b.A, Is.EqualTo(1.0084).Within(LowTolerance));
                Assert.That(b.UpProbability, Is.EqualTo(.5073).Within(LowTolerance));
            });
        }

        [Test]
        public void Binomial_HW3()
        {
            var bv = new TreeVariables(2d / 12d, .25, .02, 0);
            var b = new CoxRubenstienRossBinomial(bv);

            Assert.Multiple(() =>
            {
                // sanity checks:
                Assert.That(b.UpShift, Is.EqualTo(1 / b.DownShift).Within(HighTolerance));

                // actual data checks
                Assert.That(b.UpShift, Is.EqualTo(1.10745221205).Within(HighTolerance));
                Assert.That(b.DownShift, Is.EqualTo(0.90297350000).Within(HighTolerance));
                Assert.That(b.UpProbability, Is.EqualTo(0.49083542272).Within(HighTolerance));
                Assert.That(b.DownProbability, Is.EqualTo(0.50916457728).Within(HighTolerance));
            });
        }

        [Test]
        public void Trinomial_Lecture()
        {
            var bv = new TreeVariables(2d / 12d, .25, .02, 0);
            var b = new CoxRubenstienRossTrinomial(bv);

            Assert.Multiple(() =>
            {
                // sanity checks:
                Assert.That(b.UpShift, Is.EqualTo(1 / b.DownShift).Within(VeryHighTolerance));

                // actual data checks
                Assert.That(b.UpShift, Is.EqualTo(1.1933645794479495947956947190067).Within(VeryHighTolerance));
                Assert.That(b.MiddleShift, Is.Zero);
                Assert.That(b.DownShift, Is.EqualTo(0.83796688557875578872262310029487).Within(VeryHighTolerance));

                Assert.That(b.UpProbability, Is.EqualTo(0.1613633658077680000000000000).Within(VeryHighTolerance));
                Assert.That(b.MiddleProbability, Is.EqualTo(2m / 3m).Within(VeryHighTolerance));
                Assert.That(b.DownProbability, Is.EqualTo(-0.1613633658077680000000000000).Within(VeryHighTolerance));
            });
        }
    }
}
