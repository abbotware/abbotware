// <copyright file="SetOncePropertyTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class SetOncePropertyTests
    {
        [Test]
        public void SetOnceProperty_SetOnce()
        {
            var property1 = new SetOnceProperty<string>("test");
            Assert.IsFalse(property1.HasValue);
            property1.Value = "test";
            Assert.AreEqual("test", property1.Value);
            Assert.IsTrue(property1.HasValue);
        }

        [Test]
        public void SetOnceProperty_ConstructAndSet()
        {
            var property1 = new SetOnceProperty<string>("test", "test");
            Assert.IsTrue(property1.HasValue);
            Assert.AreEqual("test", property1.Value);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetOnceProperty_ConstructAndSetTwice()
        {
            _ = new SetOnceProperty<string>("test", "test")
            {
                Value = "test",
            };
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetOnceProperty_GetWithoutSet()
        {
            var property1 = new SetOnceProperty<string>("test");
            Assert.IsFalse(property1.HasValue);
            Assert.AreEqual("test", property1.Value);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetOnceProperty_SetTwice()
        {
            var property1 = new SetOnceProperty<string>("test");
            Assert.IsFalse(property1.HasValue);
            property1.Value = "test";
            Assert.IsTrue(property1.HasValue);
            property1.Value = "test";
        }
    }
}