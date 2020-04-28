// <copyright file="TemporaryFileStreamTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.IO;
    using Abbotware.Core.IO;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.IO")]
    public class TemporaryFileStreamTests
    {
        [Test]
        public void TemporaryFileS_NoName()
        {
            var fname = string.Empty;
            using (var k = new TemporaryFileStream())
            {
                fname = k.Name;
                Assert.IsTrue(File.Exists(fname));
            }

            Assert.IsFalse(File.Exists(fname));
        }

        [Test]
        public void TemporaryFileS_WithName()
        {
            var filePath = Path.Combine(Path.GetTempPath(), "dummy.txt");

            var fname = string.Empty;

            using (var k = new TemporaryFileStream(new Uri(filePath)))
            {
                fname = k.Name;
                Assert.IsTrue(File.Exists(fname));
            }

            Assert.IsFalse(File.Exists(fname));
        }
    }
}