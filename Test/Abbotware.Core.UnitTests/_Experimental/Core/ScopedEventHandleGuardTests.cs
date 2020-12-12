// <copyright file="ScopedEventHandleGuardTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System.Threading;
    using Abbotware.Core.Logging.Plugins;
    using Abbotware.Core.Threading;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class ScopedEventHandleGuardTests
    {
        [Test]
        public void ScopedEventHandleGuard_TestUsage()
        {
            using var handle = new ManualResetEvent(false);

            using (var guard = new ScopedEventHandleGuard(handle, NullLogger.Instance))
            {
                Assert.IsFalse(handle.WaitOne(0));
            }

            Assert.IsTrue(handle.WaitOne(0));

            using var handle2 = new ManualResetEvent(true);

            using (var guard = new ScopedEventHandleGuard(handle2, NullLogger.Instance))
            {
                Assert.IsFalse(handle2.WaitOne(0));
            }

            Assert.IsTrue(handle2.WaitOne(0));
        }
    }
}