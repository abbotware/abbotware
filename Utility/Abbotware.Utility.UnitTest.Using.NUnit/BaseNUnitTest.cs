// -----------------------------------------------------------------------
// <copyright file="BaseNUnitTest.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Utility.UnitTest.Using.NUnit
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Logging.Plugins;
    using global::NUnit.Framework;

    /// <summary>
    ///     Base class for creating NUnit unit tests
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BaseNUnitTest : BaseUnitTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNUnitTest"/> class.
        /// </summary>
        [Obsolete("work around while rest of code is moved to github")]
        public BaseNUnitTest()
            : base(NullLogger.Instance)
        {
        }

        /// <inheritdoc/>
        protected override void AssertInconclusive(string message)
        {
            Assert.Inconclusive(message);
        }

        /// <inheritdoc/>
        protected override void AssertFail(string message)
        {
            Assert.Fail(message);
        }

        /// <inheritdoc/>
        protected override void AssertEqual(object left, object right)
        {
            Assert.AreEqual(left, right);
        }
    }
}
