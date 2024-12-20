namespace Abbotware.UnitTests.Core
{
    using System;
    using System.IO;
    using Abbotware.Core.Compression.Plugins;
    using Abbotware.Core.Extensions;
    using Abbotware.Interop.NUnit;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Compression")]
    [TestFixture]
    public class CompressionTests : BaseNUnitTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompressNullThrow()
        {
            var gz = new GZipCompression();

            _ = gz.Compress(null!);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecompressNullThrow()
        {
            var gz = new GZipCompression();

            _ = gz.Decompress(null!);
        }

        [Test]
        public void Compress_EmptyString()
        {
            var gz = new GZipCompression();

            var b = gz.CompressString(string.Empty);
            var s = gz.DecompressString(b);

            Assert.That(s, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Compress_String()
        {
            var gz = new GZipCompression();

            var input = "alaksjdflkajsd;lfkja;slkdfj;alksdjf;lkasjd;lfkasdlkfj;alkdsjf;lkasjd;lfkjas;ldkfja;lskdjf;laksjdf;lkajsd;lkjfa;slkdfj;laskdjf;laskdjf;laksdjf";

            var b = gz.CompressString(input);

            Assert.That(b.Length, Is.LessThan(input.Length));

            var s = gz.DecompressString(b);

            Assert.That(s, Is.EqualTo(input));
        }

        [Test]
        [Category("windows")]
        public void Compress_Sample_Xml()
        {
            this.SkipTestOnLinux();

            var gz = new GZipCompression();

            var input = File.ReadAllText(Path.Combine("Sample", "Data", "Average_Daily_Traffic_Counts.xml"));

            var b = gz.CompressString(input);

            Assert.That(b.Length, Is.LessThan(input.Length));
            Assert.That(b.Length, Is.EqualTo(104647));

            var s = gz.DecompressString(b);

            Assert.That(s, Is.EqualTo(input));
        }
    }
}
