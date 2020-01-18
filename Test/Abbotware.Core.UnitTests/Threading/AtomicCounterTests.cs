// <copyright file="AtomicCounterTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2016. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Threading.Counters;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [Category("Core")]
    [Category("Core.Threading")]
    [TestFixture]
    public class AtomicCounterTests : BaseNUnitTest
    {
        [Test]
        public void AtomicCounter__ParallelIncrement()
        {
            var k = 0;

            var global = new AtomicCounter();

            Parallel.For(0, 100, (i, loop) =>
            {
                Interlocked.Increment(ref k);
                var counter = new AtomicCounter();

                Assert.AreEqual(0, counter.Value);

                void Callback()
                {
                    counter.Increment();
                    global.Increment();
                }

                Parallel.Invoke(Callback, Callback, Callback, Callback, Callback, Callback, Callback, Callback, Callback, Callback, Callback, Callback, Callback);

                Assert.AreEqual(13, counter.Value);
            });

            Assert.AreEqual(100, k);
            Assert.AreEqual(1300, global.Value);
        }

        [Test]
        public void AtomicCounter_ParallelCounter()
        {
            var k = 0;

            Parallel.For(0, 100, (i, loop) =>
            {
                Interlocked.Increment(ref k);

                var counter = new AtomicCounter();

                Assert.AreEqual(0, counter.Value);

                void Inc()
                {
                    counter.Increment();
                }

                void Dec()
                {
                    counter.Decrement();
                }

                Parallel.Invoke(Inc, Inc, Dec, Dec, Dec, Inc, Dec, Inc, Dec, Inc, Inc, Inc, Dec, Dec, Inc);

                Assert.AreEqual(1, counter.Value);
            });

            Assert.AreEqual(100, k);
        }

        [Test]
        public void AtomicWaitComplete()
        {
            var counter = new AtomicWaitCounter(2);

            var fac = new TaskFactory();
            var t1 = fac.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
                counter.Increment();
            });
            var t2 = fac.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
                counter.Increment();
            });

            Assert.IsTrue(counter.Completion.Wait(TimeSpan.FromMilliseconds(300)));

            Thread.Sleep(100);

            Assert.IsTrue(t1.IsCompleted);
            Assert.IsTrue(t2.IsCompleted);
        }

        [Test]
        public void AtomicWaitTimeout()
        {
            var counter = new AtomicWaitCounter(2);

            var fac = new TaskFactory();

            using var t1 = fac.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
                counter.Increment();
            });

            Assert.IsFalse(counter.Completion.Wait(TimeSpan.FromMilliseconds(300)));
        }
    }
}