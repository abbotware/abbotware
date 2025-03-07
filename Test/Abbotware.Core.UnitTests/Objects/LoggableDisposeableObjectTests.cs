﻿// <copyright file="LoggableDisposeableObjectTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Objects;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Objects")]
    public class LoggableDisposeableObjectTests
    {
#if NETSTANDARD2_0
        /// <summary>
        ///     A test for ArgumentNullException
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor()
        {
            using var k = new DObject(null);

            GC.KeepAlive(k);
        }
#endif

        /// <summary>
        ///     A test for ThrowIfDisposed
        /// </summary>
        [Test]
        public void LoggableDisposableObject_TestLog()
        {
            using var dobject = new DObject(NullLogger.Instance);
            dobject.Foo();
        }

        internal sealed class DObject : BaseComponent
        {
            public DObject(ILogger l)
                : base(l)
            {
            }

            public void Foo()
            {
                this.OnInitialize();
            }
        }
    }
}