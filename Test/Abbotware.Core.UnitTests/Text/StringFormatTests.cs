namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Text;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [Category("Core")]
    [Category("Core.Text")]
    [TestFixture]
    public class StringFormatTests
    {
        [Test]
        public void Bytes_Usage()
        {
            Assert.AreEqual("100 B", StringFormat.Bytes(100));
            Assert.AreEqual("1.01 KB", StringFormat.Bytes(1006));
            Assert.AreEqual("1.5 KB", StringFormat.Bytes(1500));
            Assert.AreEqual("1.01 MB", StringFormat.Bytes(1_006_000));
            Assert.AreEqual("1.5 MB", StringFormat.Bytes(1_500_000));
            Assert.AreEqual("1 KB", StringFormat.Bytes(1000));
            Assert.AreEqual("34 GB", StringFormat.Bytes(34_000_100_000));
            Assert.AreEqual("1.7 TB", StringFormat.Bytes(1_700_000_000_000));
        }
    }
}