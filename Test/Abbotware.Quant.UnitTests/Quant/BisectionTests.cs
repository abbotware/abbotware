namespace Abbotware.UnitTests.Quant.Quant
{
    using Abbotware.Quant.Solvers;
    using NUnit.Framework;
    using Alternative = MathNet.Numerics.RootFinding.Bisection;

    public class BisectionTests
    {
        [Test]
        [TestCase(true, -100, 100)]
        [TestCase(true, 0, 100)]
        [TestCase(true, -100, 0)]
        public void Bisection_Linear(bool hasRoot, double lower, double upper)
        {
            var f = (double x) => x;

            var x1 = Bisection.Solve(f, new(lower, upper), 0, Precision.VeryHigh, true, SolverConstants.DefaultMaxIterations, new double[SolverConstants.DefaultMaxIterations]);
            var theirsFound = Alternative.TryFindRoot(f, lower, upper, Precision.VeryHigh, (int)SolverConstants.DefaultMaxIterations, out var x2);

            AssertRoot(hasRoot, x1, x2, theirsFound);
        }

        [Test]
        [TestCase(true, -100, 100, 33)]
        [TestCase(true, 0, 100, -15)]
        [TestCase(true, -100, 0, 15)]
        public void Bisection_Linear_Offset(bool hasRoot, double lower, double upper, double offset)
        {
            var f = (double x) => x + offset;
            var answer = -offset;

            var x1 = Bisection.Solve(f, new(lower, upper), 0, Precision.VeryHigh, true, SolverConstants.DefaultMaxIterations, new double[SolverConstants.DefaultMaxIterations]);
            var theirsFound = Alternative.TryFindRoot(f, lower, upper, Precision.VeryHigh, (int)SolverConstants.DefaultMaxIterations, out var x2);

            AssertRoot(hasRoot, x1, x2, theirsFound);
        }

        [Test]
        [TestCase(false, -100, 100)]
        [TestCase(true, 0, 100)]
        [TestCase(true, -100, 0)]
        public void Bisection_Squared(bool hasRoot, double lower, double upper)
        {
            var f = (double x) => x * x;

            var x1 = Bisection.Solve(f, new(lower, upper), 0, Precision.VeryHigh, true, SolverConstants.DefaultMaxIterations, new double[SolverConstants.DefaultMaxIterations]);
            var theirsFound = Alternative.TryFindRoot(f, lower, upper, Precision.VeryHigh, (int)SolverConstants.DefaultMaxIterations, out var x2);

            AssertRoot(hasRoot, x1, x2, theirsFound);
        }

        [Test]
        [TestCase(false, -1000, 1000, 33)]
        [TestCase(true, 0, 1000, -5)]
        [TestCase(false, -1000, 0, 5)]
        public void Bisection_Squared_OffSet(bool hasRoot, double lower, double upper, double offset)
        {
            var f = (double x) => (x * x) + offset;
            var answer = -offset;

            var x1 = Bisection.Solve(f, new(lower, upper), 0, Precision.VeryHigh, true, SolverConstants.DefaultMaxIterations, new double[SolverConstants.DefaultMaxIterations]);
            var theirsFound = Alternative.TryFindRoot(f, lower, upper, Precision.VeryHigh, (int)SolverConstants.DefaultMaxIterations, out var x2);

            AssertRoot(hasRoot, x1, x2, theirsFound);
        }

        private static void AssertRoot(bool hasRoot, double? ours, double theirs, bool theirsFound)
        {
            if (hasRoot)
            {
                Assert.That(theirsFound, Is.True, "We found a root, but they found no root?");
                Assert.That(ours, Is.EqualTo(theirs).Within(Precision.VeryHigh));
            }
            else
            {
                Assert.That(ours, Is.Null);
                Assert.That(theirsFound, Is.False, "We found no root, but they found a root");
            }
        }
    }
}