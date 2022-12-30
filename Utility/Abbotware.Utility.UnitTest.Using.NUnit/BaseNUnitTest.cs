// -----------------------------------------------------------------------
// <copyright file="BaseNUnitTest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Utility.UnitTest.Using.NUnit
{
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core.Logging;
    using Abbotware.Using.Castle;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using global::NUnit.Framework;

    /// <summary>
    ///     Base class for creating NUnit unit tests
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BaseNUnitTest : BaseUnitTest, IWindsorInstaller
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseNUnitTest"/> class.
        /// </summary>
        protected BaseNUnitTest()
            : this(CreateTestContainer())
        {
        }

        private BaseNUnitTest(IWindsorContainer container)
            : this(container, container.Resolve<ILogger>())
        {
        }

        private BaseNUnitTest(IWindsorContainer container, ILogger logger)
            : base(logger)
        {
            this.Container = container;

            this.Container.Install(this);
        }

        /// <summary>
        ///     Gets the IoC Container for this unit test
        /// </summary>
        protected IWindsorContainer Container
        {
            get;
        }

        /// <inheritdoc/>
        public override void AssertInconclusive(string message)
        {
            Assert.Inconclusive(message);
        }

        /// <inheritdoc/>
        public override void AssertFail(string message)
        {
            Assert.Fail(message);
        }

        /// <inheritdoc/>
        public override void AssertEqual(object? left, object? right)
        {
            Assert.AreEqual(left, right);
        }

        /// <inheritdoc />
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (container == null)
            {
                return;
            }

            this.OnInstall(container);
        }

        /// <summary>
        /// Creates the container
        /// </summary>
        /// <returns>initialized container</returns>
        protected static IWindsorContainer CreateTestContainer()
        {
            return IocContainer.Create("UnitTest", false)
                .AddLog4net("log4net.unittest.config");
        }

        /// <summary>
        ///     Hook to install custom installers for this unit test container class
        /// </summary>
        /// <param name="container">container to install with installers</param>
        protected virtual void OnInstall(IWindsorContainer container)
        {
        }
    }
}
