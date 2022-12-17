namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Security;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Securtiy")]
    [TestFixture]
    public class MD5HelperTests
    {
        [Test]
        public void Basic()
        {
            //// https://www.md5hashgenerator.com/

            var h3 = MD5Helper.GenerateHash(" ");
            Assert.AreEqual("7215EE9C7D9DC229D2921A40E899EC5F", h3);

            var h4 = MD5Helper.GenerateHash("dfewefas");
            Assert.AreEqual("C003D06BBD47531979CA94807EDDC1C9", h4);
        }

        [Test]
        public void EmptyString()
        {
            //// https://www.md5hashgenerator.com/

            var h1 = MD5Helper.GenerateHash(string.Empty);

            Assert.AreEqual("D41D8CD98F00B204E9800998ECF8427E", h1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null()
        {
            var h1 = MD5Helper.GenerateHash(null!);
        }
    }
}