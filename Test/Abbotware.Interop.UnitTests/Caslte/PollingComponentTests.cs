//-----------------------------------------------------------------------
// <copyright file="InteropCastleTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Castle
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Host;
    using Abbotware.Host.Configuration;
    using Abbotware.Interop.Castle.ExtensionPoints;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;
    using global::NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Castle")]
    public class PollingComponentTests : BaseNUnitTest
    {
        [Test]
        public async Task PollingComponent()
        {
            using var a = new AwaitCounter(this.Logger);

            Assert.AreEqual(a.Counter, 0);

            a.Start();

            await Task.Delay(1000);

            a.Stop();

            Assert.GreaterOrEqual(a.Counter, 8);
        }

        private class MsecCounter : StartablePollingComponent
        {
            private readonly TestApp app;

            public MsecCounter(TestApp app, ILogger logger)
                : base(TimeSpan.FromMilliseconds(100), logger)
            {
                this.app = app;
            }

            protected override void OnNext(long x)
            {
                this.app.Counter++;
            }
        }

        private class AwaitCounter : PollingComponent
        {
            public AwaitCounter(ILogger logger)
         : base(TimeSpan.FromMilliseconds(50), logger)
            {
            }

            public int Counter { get; set; }

            protected override async Task OnIterationAsync()
            {
                this.Counter++;

                await Task.Delay(1);
            }
        }

        private class TestApp : AbbotwareHostService
        {
            public TestApp(IHostOptions configuration)
                : base(configuration)
            {
            }

            public int Counter { get; set; }

            protected override void OnInstall(IWindsorContainer container)
            {
                container.Register(Component.For<TestApp>().Instance(this).Named("test"));
                container.Register(Component.For<MsecCounter>().ImplementedBy<MsecCounter>());

                base.OnInstall(container);
            }
        }
    }
}