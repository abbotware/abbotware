namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Interop.NUnit;
    using global::Abbotware.Core.Math;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Math")]
    [TestFixture]
    public class IntervalTTests
    {
        [Test]
        public void CreateTests()
        {
            {
                var i = new Interval<int>(5, 100);

                Assert.AreEqual(5, i.LowerBound);
                Assert.AreEqual(100, i.UpperBound);
                Assert.IsTrue(i.IsInclusive);
                Assert.IsFalse(i.IsExlusive);
                Assert.IsTrue(i.IncludeLower);
                Assert.IsTrue(i.IncludeUpper);
            }

            {
                // forgive minor 'mistake'
                var i = new Interval<int>(100, 5);

                Assert.AreEqual(5, i.LowerBound);
                Assert.AreEqual(100, i.UpperBound);
            }

            {
                var i = new Interval<int>(5, false, 100, false);

                Assert.AreEqual(5, i.LowerBound);
                Assert.AreEqual(100, i.UpperBound);
                Assert.IsFalse(i.IsInclusive);
                Assert.IsTrue(i.IsExlusive);
                Assert.IsFalse(i.IncludeLower);
                Assert.IsFalse(i.IncludeUpper);
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void LowerGreaterThanUpper()
        {
            // upper > lower -> exception
            _ = new Interval<int>(100, false, 5, false);
        }

        [Test]
        public void SinglePoint()
        {
            var i = new Interval<int>(5, 5);

            Assert.IsFalse(i.Within(4));

            Assert.IsTrue(i.Within(5));

            Assert.IsFalse(i.Within(6));
        }

        [Test]
        public void AllPositiveNonZero()
        {
            var i = new Interval<int>(1, int.MaxValue);

            Assert.IsFalse(i.Within(0));
            Assert.IsTrue(i.Within(1));
            Assert.IsTrue(i.Within(int.MaxValue));
            Assert.IsFalse(i.Within(int.MinValue));
        }

        [Test]
        public void SpecificRange()
        {
            var i = new Interval<int>(1_000_000, false, 2_999_999, true);

            Assert.IsFalse(i.IsExlusive);
            Assert.IsFalse(i.IsInclusive);
            Assert.IsTrue(i.IncludeUpper);
            Assert.IsFalse(i.IncludeLower);

            Assert.IsFalse(i.Within(1_000_000));
            Assert.IsTrue(i.Within(1_000_001));
            Assert.IsTrue(i.Within(1_400_000));
            Assert.IsTrue(i.Within(2_999_999));
            Assert.IsFalse(i.Within(3_000_000));
        }
    }
}