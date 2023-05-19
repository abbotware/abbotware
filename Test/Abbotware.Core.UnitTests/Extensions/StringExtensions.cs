// <copyright file="StringExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Extensions;
    using NUnit.Framework;

    /// <summary>
    ///     Arguments class unit tests
    /// </summary>
    [TestFixture]
    [Category("Core")]
    [Category("Core.Extensions")]
    public class StringExtensions
    {
        [Test]
        public void Null()
        {
            string? s = null;
            Assert.IsTrue(s.IsBlank());
            Assert.IsFalse(s.IsNotBlank());
        }

        [Test]
        public void StringEmpty()
        {
            string s = string.Empty;
            Assert.IsTrue(s.IsBlank());
            Assert.IsFalse(s.IsNotBlank());
        }

        [Test]
        public void WhiteSpace()
        {
            string s = "       ";
            Assert.IsTrue(s.IsBlank());
            Assert.IsFalse(s.IsNotBlank());
        }

        [Test]
        public void Value()
        {
            string s = "asdfasdf";
            Assert.IsFalse(s.IsBlank());
            Assert.IsTrue(s.IsNotBlank());
        }
    }
}