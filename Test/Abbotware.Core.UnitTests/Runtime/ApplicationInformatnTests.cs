namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Runtime;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Runtime")]
    public class ApplicationInformatnTests : BaseNUnitTest
    {
        [Test]
        public void RuntimeName()
        {
            this.SkipTestOnLinux();

            var e = new ApplicationInformation();

#if NETCOREAPP2_1
            Assert.AreEqual("2.1.13", e.RuntimeName);
            Assert.AreEqual(new Version("4.6.28008.1"), e.RuntimeVersion);
#elif NETCOREAPP2_2
            if (Environment.GetEnvironmentVariable("NetCoreSdk") == "2.2.401")
            {
                Assert.AreEqual("2.2.6", e.RuntimeName);
                Assert.AreEqual(new Version("4.6.27817.3"), e.RuntimeVersion);
            }
            else
            {
                Assert.AreEqual("2.2.7", e.RuntimeName);
                Assert.AreEqual(new Version("4.6.28008.2"), e.RuntimeVersion);
            }
#elif NETCOREAPP3_1
            Assert.AreEqual("3.1.21", e.RuntimeName);
            Assert.AreEqual(new Version("3.1.21"), e.RuntimeVersion);
#elif NET6_0
            Assert.AreEqual("6.0.12", e.RuntimeName);
            Assert.AreEqual(new Version("6.0.12"), e.RuntimeVersion);
#elif NET7_0
            Assert.AreEqual("7.0.2", e.RuntimeName);
            Assert.AreEqual(new Version("7.0.2"), e.RuntimeVersion);
#else
            Assert.Fail("Unexpected");
#endif
        }
    }
}