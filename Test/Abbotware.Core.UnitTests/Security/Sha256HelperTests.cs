namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Security;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Securtiy")]
    [TestFixture]
    public class Sha256HelperTests
    {
        [Test]
        public void Basic()
        {
            //// https://passwordsgenerator.net/sha256-hash-generator/

            var h3 = Sha256Helper.GenerateHash(" ");
            Assert.AreEqual("36A9E7F1C95B82FFB99743E0C5C4CE95D83C9A430AAC59F84EF3CBFAB6145068", h3);

            var h4 = Sha256Helper.GenerateHash("sdfgsdfgs");
            Assert.AreEqual("D6C84C6D32D245DDA9CE2E9E3C776767DC877B4C2BDEAEC5CAB3E595D573740A", h4);

            // https://crypto.stackexchange.com/questions/64442/is-it-possible-to-convert-the-output-of-sha256-hash-to-binary-or-decimal-value
            var h5 = Sha256Helper.GenerateHash("hello  world!");
            Assert.AreEqual("60668F3A418FF2F78E6C53BC77910B7945ECD29D4B3D9D3934B89B0B5E84F797", h5);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null()
        {
            Sha256Helper.GenerateHash(null!);
        }

        [Test]
        public void EmptyString()
        {
            var h1 = Sha256Helper.GenerateHash(string.Empty);

            Assert.AreEqual("E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855", h1.ToUpperInvariant());
        }
    }
}
