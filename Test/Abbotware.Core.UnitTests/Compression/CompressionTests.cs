﻿namespace Abbotware.UnitTests.Core
{
    using System;
    using System.IO;
    using Abbotware.Core.Compression;
    using Abbotware.Core.Compression.Plugins;
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

            gz.Compress(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecompressNullThrow()
        {
            var gz = new GZipCompression();

            gz.Decompress(null);
        }

        [Test]
        public void Compress_EmptyString()
        {
            var gz = new GZipCompression() as IStringCompression;

            var b = gz.Compress(string.Empty);
            var s = gz.Decompress(b);

            Assert.AreEqual(string.Empty, s);
        }

        [Test]
        public void Compress_String()
        {
            var gz = new GZipCompression() as IStringCompression;

            var input = "alaksjdflkajsd;lfkja;slkdfj;alksdjf;lkasjd;lfkasdlkfj;alkdsjf;lkasjd;lfkjas;ldkfja;lskdjf;laksjdf;lkajsd;lkjfa;slkdfj;laskdjf;laskdjf;laksdjf";

            var b = gz.Compress(input);

            Assert.IsTrue(b.Length < input.Length);

            var s = gz.Decompress(b);

            Assert.AreEqual(input, s);
        }

        [Test]
        [Category("windows")]
        public void Compress_Sample_Xml()
        {
            this.SkipTestOnLinux();

            var gz = new GZipCompression() as IStringCompression;

            var input = File.ReadAllText(Path.Combine("Sample", "Data", "Average_Daily_Traffic_Counts.xml"));

            var b = gz.Compress(input);

            Assert.IsTrue(b.Length < input.Length);
            Assert.AreEqual(106686, b.Length);

            var s = gz.Decompress(b);

            Assert.AreEqual(input, s);
        }
    }
}