﻿// <copyright file="ConsoleArgumentsTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System.Collections.Generic;
    using Abbotware.Core;
    using NUnit.Framework;

    [TestFixture]
    [Category("Host")]
    public class ConsoleArgumentsTests
    {
        [Test]
        public void ConsoleArguments_Create()
        {
            var o = new ConsoleArguments(new List<string> { "test" });
            var a = o.Arguments;

            Assert.IsNotNull(a);
        }
    }
}