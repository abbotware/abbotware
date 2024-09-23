// <copyright file="AbbotwareExceptionTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Exceptions;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [Category("Core")]
    [TestFixture]
    public class AbbotwareExceptionTests
    {
        [Test]
        public void Exceptions()
        {
            var a1 = new AbbotwareException();
            Assert.IsNotNull(a1);

            var a2 = new AbbotwareException("test");
            Assert.IsNotNull(a2);

            var a3 = AbbotwareException.Create(new Exception(), "test");
            Assert.IsNotNull(a3);

            var a4 = AbbotwareException.Create("test {0}", "test");
            Assert.AreEqual("test test", a4.Message);

            var a5 = AbbotwareException.Create(new Exception(), "test {0}", "test");
            Assert.AreEqual("test test", a5.Message);
        }
    }
}