//-----------------------------------------------------------------------
// <copyright file="TimeoutAttributeTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Interop.UnitTests.NUnit
{
    using System;
    using System.Threading;
    using Abbotware.Interop.NUnit;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;
    using TimeoutAttribute = Abbotware.Interop.NUnit.TimeoutAttribute;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.NUnit")]
    public class TimeoutAttributeTests : BaseNUnitTest
    {
        [Test]
        [Timeout(2)]
        [ExpectedException(typeof(TimeoutException))]
        public void Timeout_Thrown()
        {
            Thread.Sleep(4000);
        }

        [Test]
        [Timeout(2)]
        public void NoTimeout()
        {
        }

        [Test]
        [Timeout(10)]
        [ExpectedException(typeof(ArgumentException))]
        public void ExpectedException_AfterTimeout()
        {
            throw new ArgumentException("test");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Timeout(10)]
        public void ExpectedException_BeforeTimeout()
        {
            throw new ArgumentException("test");
        }
    }
}