// <copyright file="MemoryTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Threading;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.NUnit;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Objects")]
    public class TimeSpanComponentTests : BaseNUnitTest
    {
        [Test]
        public void TimeSpanComponent_Usage()
        {
            using var t = new TestComponent(TimeSpan.FromMilliseconds(500), this.Logger);

            Assert.AreEqual(0, t.Count);

            Assert.IsFalse(t.IsInitialized);
            Assert.IsFalse(t.IsDisposed);

            t.Initialize();

            Assert.AreEqual(1, t.Count);
            Assert.IsTrue(t.IsInitialized);

            t.Initialize();
            Assert.AreEqual(1, t.Count);

            Thread.Sleep(1000);

            Assert.AreEqual(1, t.Count);

            t.Initialize();
            Assert.AreEqual(2, t.Count);
            Assert.IsTrue(t.IsInitialized);

            t.Initialize();
            Assert.AreEqual(2, t.Count);

            Thread.Sleep(2000);

            Assert.AreEqual(2, t.Count);

            t.Initialize();
            Assert.AreEqual(3, t.Count);
            t.Initialize();

            t.Dispose();
            Assert.IsTrue(t.IsDisposed);
        }

        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void TimeSpanComponent_Dispose()
        {
            using var t = new TestComponent(TimeSpan.FromMilliseconds(500), this.Logger);

            t.Initialize();
            t.Dispose();
            Assert.IsTrue(t.IsDisposed);

            t.Initialize();
        }

        internal sealed class TestComponent : TimeSpanComponent
        {
            public TestComponent(TimeSpan expirationTimeSpan, ILogger logger)
                : base(expirationTimeSpan, logger)
            {
            }

            public int Count { get; set; }

            protected override void OnInitialize()
            {
                ++this.Count;

                base.OnInitialize();
            }
        }
    }
}