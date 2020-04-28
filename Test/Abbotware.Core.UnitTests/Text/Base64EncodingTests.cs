namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Text;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Text")]
    [TestFixture]
    public class Base64EncodingTests
    {
        private const string Message = "Hello World!";

        private const string EncodedMessage = "SGVsbG8gV29ybGQh";

        [Test]
        public void Verify_Decode()
        {
            var e = new Base64Encoding();
            var decoded = e.GetBytes(EncodedMessage);
            Assert.AreEqual(Message, decoded);
        }

        [Test]
        public void Verify_Roundtrip()
        {
            var e = new Base64Encoding();

            Assert.AreEqual(EncodedMessage, e.GetString(e.GetBytes(EncodedMessage)));
        }
    }
}