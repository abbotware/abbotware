// <copyright file="ConsoleArgumentsTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System.Collections.Generic;
    using Abbotware.Core.Runtime;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Runtime")]
    [TestFixture]
    public class ConsoleArgumentsTests
    {
        [Test]
        public void ConsoleArguments_Create()
        {
            var o = new ConsoleArguments(new List<string> { "test" });
            var a = o.Arguments;

            Assert.IsNotNull(a);
        }

        [Test]
        public void ConsoleArguments_ToString1()
        {
            var o = new ConsoleArguments(new List<string> { "test" });

            Assert.AreEqual("[test]", o.ToString());
        }

        [Test]
        public void ConsoleArguments_ToString2()
        {
            var o = new ConsoleArguments(new List<string> { string.Empty, "first", "second" });

            Assert.AreEqual("[first second]", o.ToString());
        }
    }
}