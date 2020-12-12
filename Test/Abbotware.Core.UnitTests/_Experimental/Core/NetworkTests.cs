namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Net;
    using Abbotware.Core.Helpers;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class NetworkTests
    {
        [Test]
        public void MacAddress()
        {
            if (!string.Equals(Dns.GetHostName(), "Abbotware", StringComparison.InvariantCultureIgnoreCase))
            {
                Assert.Inconclusive();
                return;
            }

            Assert.AreEqual("7085C2535C7E", NetworkHelper.FirstMacAddress());
        }
    }
}