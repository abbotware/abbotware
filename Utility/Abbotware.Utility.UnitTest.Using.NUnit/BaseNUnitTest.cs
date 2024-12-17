// -----------------------------------------------------------------------
// <copyright file="BaseNUnitTest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Utility.UnitTest.Using.NUnit
{
    using System.Diagnostics.CodeAnalysis;
    using global::NUnit.Framework;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Base class for creating NUnit unit tests
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BaseNUnitTest : BaseUnitTest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseNUnitTest"/> class.
        /// </summary>
        protected BaseNUnitTest()
            : this(null, null)
        {
        }

        private BaseNUnitTest(ILogger logger, ILoggerFactory factory)
            : base(logger)
        {
            this.LoggerFactory = factory;
        }

        /// <summary>
        ///     Gets the IoC Container for this unit test
        /// </summary>
        protected ILoggerFactory LoggerFactory
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
            Assert.That(left, Is.EqualTo(right));
        }
    }
}
