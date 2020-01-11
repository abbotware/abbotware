namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Threading;
    using Abbotware.Core.Chrono;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Chrono")]
    public class MinimumTimeSpanTests
    {
        [Test]
        public void MinimumTimeSpan_Usage()
        {
            var mts = new MinimumTimeSpan(TimeSpan.FromSeconds(1));

            Assert.IsFalse(mts.IsExpired);
            Assert.IsFalse(mts.IsExpired);
            Assert.IsFalse(mts.IsExpired);
            Assert.IsFalse(mts.IsExpired);

            Thread.Sleep(TimeSpan.FromSeconds(1.5));

            Assert.IsTrue(mts.IsExpired);

            Assert.IsFalse(mts.IsExpired);
        }
    }
}