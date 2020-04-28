namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Text;
    using NUnit.Framework;

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
            Assert.AreEqual("1.01 MB", StringFormat.Bytes(1006000));
            Assert.AreEqual("1.5 MB", StringFormat.Bytes(1500000));
            Assert.AreEqual("1 KB", StringFormat.Bytes(1000));
        }
    }
}