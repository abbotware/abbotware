// <copyright file="InstanceCounterTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Objects;
    using Abbotware.Core.Threading.Counters;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Threading")]
    public class InstanceCounterTests : BaseNUnitTest
    {
        [Test]
        public void InstanceCounter_TestUsage()
        {
            Assert.AreEqual(0, TypeCreatedCounter<AClass>.Count);
            Assert.AreEqual(0, ActiveInstanceCounter<AClass>.GlobalActiveCount);

            using (var i1 = new AClass(this.Logger))
            {
                Assert.AreEqual(1, TypeCreatedCounter<AClass>.Count);
                Assert.AreEqual(1, ActiveInstanceCounter<AClass>.GlobalActiveCount);

                Assert.AreEqual(i1.InstanceId, 0);

                using (var i2 = new AClass(this.Logger))
                using (var i3 = new AClass(this.Logger))
                {
                    Assert.AreEqual(1, i2.InstanceId);

                    Assert.AreEqual(3, TypeCreatedCounter<AClass>.Count);
                    Assert.AreEqual(3, ActiveInstanceCounter<AClass>.GlobalActiveCount);

                    using (var tracker = new ActiveInstanceCounter<AClass>(this.Logger))
                    {
                        Assert.AreEqual(4, ActiveInstanceCounter<AClass>.GlobalActiveCount);
                        Assert.AreEqual(4, TypeCreatedCounter<AClass>.Count);
                        Assert.AreEqual(4, tracker.ActiveCount);
                    }

                    Assert.AreEqual(3, ActiveInstanceCounter<AClass>.GlobalActiveCount);
                    Assert.AreEqual(4, TypeCreatedCounter<AClass>.Count);
                }

                Assert.AreEqual(4, TypeCreatedCounter<AClass>.Count);
                Assert.AreEqual(1, ActiveInstanceCounter<AClass>.GlobalActiveCount);
            }

            Assert.AreEqual(4, TypeCreatedCounter<AClass>.Count);
            Assert.AreEqual(0, ActiveInstanceCounter<AClass>.GlobalActiveCount);

            var c = new TypeCreatedCounter<AClass>();

            Assert.AreEqual(5, TypeCreatedCounter<AClass>.Count);
            Assert.AreEqual(TypeCreatedCounter<AClass>.Count, c.CreatedCount);
        }

        internal sealed class AClass : BaseComponent
        {
            private readonly ActiveInstanceCounter<AClass> counter;

            public AClass(ILogger logger)
                : base(logger)
            {
                this.counter = new ActiveInstanceCounter<AClass>(logger);
            }

            public long InstanceId => this.counter.InstanceId;

            protected override void OnDisposeManagedResources()
            {
                this.counter.Dispose();
                base.OnDisposeManagedResources();
            }
        }
    }
}