// <copyright file="DisposableExtensionsTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Extensions")]
    public class DisposableExtensionsTests
    {
        [Test]

        public async Task CreateTimeoutScope_NoTimeout()
        {
            TestObject? peekObject = null;

            using (var t = new TestObject())
            {
                peekObject = t;

                using (var s = t.DisposeAsyncOperationAfterTimeout(TimeSpan.FromMilliseconds(100)))
                {
                    Assert.IsFalse(t.IsDisposed);
                }

                // make sure the original object is not disposed if the timeout wrapper is disposed
                Assert.IsFalse(t.IsDisposed);

                await Task.Delay(TimeSpan.FromMilliseconds(250));

                // make sure the original object is not disposed even after the timeout should have occured
                Assert.IsFalse(t.IsDisposed);
            }

            Assert.IsTrue(peekObject.IsDisposed);
        }

        [Test]
        public async Task CreateTimeoutScope_WithTimeout()
        {
            TestObject? peekObject = null;
            {
                using var t = new TestObject();

                peekObject = t;
                {
                    using var s = t.DisposeAsyncOperationAfterTimeout(TimeSpan.FromMilliseconds(100));

                    Assert.IsFalse(t.IsDisposed);

                    await Task.Delay(TimeSpan.FromMilliseconds(250));

                    Assert.IsTrue(t.IsDisposed);
                }
            }

            Assert.IsTrue(peekObject.IsDisposed);
        }

        [Test]
        public void CreateTimeoutScope_DoubleDispose()
        {
            TestObject? peekObject = null;

            using (var t = new TestObject())
            {
                peekObject = t;

                using (var s = t.DisposeAsyncOperationAfterTimeout(TimeSpan.FromMilliseconds(100)))
                {
                    Assert.IsFalse(t.IsDisposed);

                    s.Dispose();

                    Assert.IsFalse(t.IsDisposed);
                }

                Assert.IsFalse(t.IsDisposed);
            }

            Assert.IsTrue(peekObject.IsDisposed);
        }

        public sealed class TestObject : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                this.IsDisposed = true;
            }
        }
    }
}