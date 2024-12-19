// <copyright file="StringExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Extensions;
    using NUnit.Framework;

    /// <summary>
    ///     Arguments class unit tests
    /// </summary>
    [Category("Core")]
    [Category("Core.Extensions")]
    public class DictonaryExtensions
    {
        [Test]
        public void RemoveOrThrow()
        {
            var d = new Dictionary<string, Uri>
            {
                { "test", new("http://asdf") },
            };

            Assert.That(d.ContainsKey("test"), Is.True);

            _ = d.RemoveOrThrow("test");

            Assert.That(d.ContainsKey("test"), Is.False);
        }

        [Test]
        public void RemoveOrThrow_Throws()
        {
            var d = new Dictionary<string, Uri>();
            _ = Assert.Throws<KeyNotFoundException>(() => d.RemoveOrThrow("test"));
        }
    }
}