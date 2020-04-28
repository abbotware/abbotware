namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Helpers;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Helpers")]
    public class TimeSpanHelperTests
    {
        [Test]
        public void TimeSpanHelper_TestUsage()
        {
            Assert.AreEqual("15s", TimeSpanHelper.ToString(new TimeSpan(0, 0, 0, 15)));
            Assert.AreEqual("3 Days", TimeSpanHelper.ToString(new TimeSpan(3, 0, 0, 0)));
            Assert.AreEqual("3.2 Days", TimeSpanHelper.ToString(new TimeSpan(3, 5, 4, 15)));
            Assert.AreEqual("5 Hours", TimeSpanHelper.ToString(new TimeSpan(0, 5, 4, 15)));
            Assert.AreEqual("5.7 Hours", TimeSpanHelper.ToString(new TimeSpan(0, 5, 40, 15)));
            Assert.AreEqual("4 Min", TimeSpanHelper.ToString(new TimeSpan(0, 0, 4, 15)));
            Assert.AreEqual("n/a", TimeSpanHelper.ToString(null));
        }
    }
}