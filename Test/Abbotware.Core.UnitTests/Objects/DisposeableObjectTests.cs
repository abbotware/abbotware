// <copyright file="DisposeableObjectTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Objects")]
    public class DisposeableObjectTests
    {
        /// <summary>
        ///     A test for ThrowIfDisposed
        /// </summary>
        [Test]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ThrowIfDisposedTest()
        {
            using var d = new DObject();
            d.Foo();

            d.Dispose();

            d.Foo();
        }

        internal sealed class DObject : BaseComponent
        {
            public void Foo()
            {
                this.ThrowIfDisposed();
            }
        }
    }
}