namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Runtime;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Runtime")]
    public class ApplicationInformationTests : BaseNUnitTest
    {
        [Test]
        public void RuntimeName()
        {
            this.SkipTestOnLinux();
            var e = new ApplicationInformation();

#if NETCOREAPP2_1
            Assert.That(e.RuntimeName, Is.EqualTo("2.1.13"));
            Assert.That(e.RuntimeVersion, Is.EqualTo(new Version("4.6.28008.1")));
#elif NETCOREAPP3_1
            Assert.That(e.RuntimeName, Is.EqualTo("3.1.21"));
            Assert.That(e.RuntimeVersion, Is.EqualTo(new Version("3.1.21")));
#elif NET6_0
            Assert.That(e.RuntimeName, Is.EqualTo("6.0.12"));
            Assert.That(e.RuntimeVersion, Is.EqualTo(new Version("6.0.12")));
#elif NET7_0
            Assert.That(e.RuntimeName, Is.EqualTo("7.0.12"));
            Assert.That(e.RuntimeVersion, Is.EqualTo(new Version("7.0.12")));
#elif NET8_0
            Assert.That(e.RuntimeName, Is.EqualTo("8.0.8"));
            Assert.That(e.RuntimeVersion, Is.EqualTo(new Version("8.0.8")));
#elif NET9_0
            Assert.That(e.RuntimeName, Is.EqualTo("9.0.8"));
            Assert.That(e.RuntimeVersion, Is.EqualTo(new Version("9.0.8")));
#else
            Assert.Fail("Unexpected");
#endif
        }
    }
}