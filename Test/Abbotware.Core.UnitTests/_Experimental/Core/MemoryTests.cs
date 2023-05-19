// <copyright file="MemoryTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Runtime;
    using Abbotware.Interop.NUnit;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class MemoryTests : BaseNUnitTest
    {
        [Test]
        [ExpectedException(typeof(InsufficientMemoryException))]
        [Category("windows")]
        public void Test_MemoryFailPoint()
        {
            this.SkipTestOnLinux();

            // neat little class never new existed (near bottom)
            // http://msdn.microsoft.com/en-us/magazine/cc163716.aspx
            using var mem = new MemoryFailPoint(1024 * 1024 * 1024);
        }
    }
}