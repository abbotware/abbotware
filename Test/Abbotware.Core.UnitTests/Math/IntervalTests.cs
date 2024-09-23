namespace Abbotware.UnitTests.Core
{
    using global::Abbotware.Core.Math;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;
    using NewAssert = NUnit.Framework.Assert;

    [Category("Core")]
    [Category("Core.Math")]
    [TestFixture]
    public class IntervalTests
    {
        [Test]
        public void CreateTests()
        {
            {
                var i = new Interval<int>(5, 100);

                Assert.AreEqual(5, i.Lower);
                Assert.AreEqual(100, i.Upper);
                Assert.IsTrue(i.IsInclusive);
                Assert.IsFalse(i.IsExlusive);
                Assert.IsTrue(i.IncludeLower);
                Assert.IsTrue(i.IncludeUpper);
            }

            {
                // forgive minor 'mistake'
                var i = new Interval<int>(100, 5);

                Assert.AreEqual(5, i.Lower);
                Assert.AreEqual(100, i.Upper);
            }

            {
                var i = new Interval<int>(5, 100, false,  false);

                Assert.AreEqual(5, i.Lower);
                Assert.AreEqual(100, i.Upper);
                Assert.IsFalse(i.IsInclusive);
                Assert.IsTrue(i.IsExlusive);
                Assert.IsFalse(i.IncludeLower);
                Assert.IsFalse(i.IncludeUpper);
            }
        }

        [Test]
        public void LowerGreaterThanUpper()
        {
            // upper > lower -> exception
            var i = new Interval<int>(100, 5, false, false);

            NewAssert.That(i.Lower, Is.Not.EqualTo(i.Upper));
            NewAssert.That(i.Lower, Is.EqualTo(5));
            NewAssert.That(i.Upper, Is.EqualTo(100));
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
            var i = new Interval<int>(1_000_000, 2_999_999, false,  true);

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