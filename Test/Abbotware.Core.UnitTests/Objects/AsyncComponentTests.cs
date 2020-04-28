// <copyright file="DisposeableObjectTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Objects")]
    public class AsyncComponentTests
    {
        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ThrowIfDisposedTest()
        {
            using var dobject = new AObject(TimeSpan.FromMilliseconds(500));
            dobject.Foo();

            dobject.Dispose();

            dobject.Foo();
        }

        [Test]
        public async Task VerifyInitialized()
        {
            using var a = new AObject(TimeSpan.FromMilliseconds(500));

            Assert.IsFalse(a.IsInitialized);

            var init = await a.InitializeAsync(default);

            Assert.IsTrue(init);
            Assert.IsTrue(a.IsInitialized);
        }

        [Test]
        public async Task VerifyDoubleInitialized()
        {
            using var a = new AObject(TimeSpan.FromMilliseconds(500));

            Assert.IsFalse(a.IsInitialized);

            var init1 = a.InitializeAsync(default);
            var init2 = a.InitializeAsync(default);

            Assert.IsFalse(init1.IsCompleted);
            Assert.IsFalse(init2.IsCompleted);
            Assert.IsFalse(a.IsInitialized);

            await init2;

            Assert.IsTrue(init1.Result); // this was first, so it triggered init
            Assert.IsFalse(init2.Result);
            Assert.IsTrue(a.IsInitialized);
        }

        [Test]
        public async Task VerifyInitAfterAlreadyInitialized()
        {
            using var a = new AObject(TimeSpan.FromSeconds(1));

            Assert.IsFalse(a.IsInitialized);

            var init1 = a.InitializeAsync(default);

            Assert.IsFalse(a.IsInitialized);
            Assert.IsFalse(init1.IsCompleted);

            await init1;

            Assert.IsTrue(init1.Result); // this was first, so it triggered init
            Assert.IsTrue(a.IsInitialized);

            var init2 = a.InitializeAsync(default);
            Assert.IsTrue(init2.IsCompleted);
            Assert.IsFalse(init2.Result);
            Assert.IsTrue(a.IsInitialized);
        }

        [Test]
        public void VerifySyncMethod()
        {
            using var a = new AObject(TimeSpan.FromMilliseconds(100));

            Assert.IsFalse(a.IsInitialized);
            a.Foo();
            Assert.IsTrue(a.IsInitialized);
            a.Foo();
        }

        [Test]
        public async Task VerifyAsyncMethod()
        {
            using var a = new AObject(TimeSpan.FromMilliseconds(100));

            Assert.IsFalse(a.IsInitialized);
            await a.FooAsync(default);
            Assert.IsTrue(a.IsInitialized);
        }

        [Test]
        public void VerifyInitalizedWithNoAwait()
        {
            using var a = new AObject(TimeSpan.FromMilliseconds(100));

            Assert.IsFalse(a.IsInitialized);

            a.InitializeAsync(default);

            Thread.Sleep(500);

            Assert.IsTrue(a.IsInitialized);
        }

        [Test]
        public void StressTest_VerifyInitalizedCalledOnce([Values(0, 1, 3, 4, 5)] int initDelay)
        {
            for (int i = 0; i < 2; ++i)
            {
                using var a = new AObject(TimeSpan.FromSeconds(initDelay));
                var max = Environment.ProcessorCount + 2;
                using var b = new Barrier(max);

                Assert.IsFalse(a.IsInitialized);

                for (int j = 0; j < max; ++j)
                {
                    Task.Run(() =>
                    {
                        b.SignalAndWait();
                        a.Initialize();
                    });
                }

                a.InitializeAsync(default).Wait();

                Assert.IsTrue(a.IsInitialized);
                Assert.AreEqual(1, a.InitCalls, "if this fails, then there is a race condition and initialize is called more than once");
            }
        }

        internal class AObject : BaseAsyncComponent
        {
            public int InitCalls;

            private readonly TimeSpan wait;

            public AObject(TimeSpan wait)
            {
                this.wait = wait;
            }

            public void Foo()
            {
                this.InitializeIfRequired();
            }

            public Task FooAsync(CancellationToken ct)
            {
                return this.InitializeIfRequiredAsync(ct);
            }

            protected override Task OnInitializeAsync(CancellationToken ct)
            {
                Interlocked.Increment(ref this.InitCalls);

                return Task.Delay(this.wait, ct);
            }
        }
    }
}