// <copyright file="DateTimeOffsetExtensionsTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Extensions;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Extensions")]
    public class DateTimeOffsetExtensionsTests
    {
        [Test]
        public void Iso8601_Use_System_Timezone()
        {
            var localZone = TimeZoneInfo.Local;

            if (localZone.BaseUtcOffset != TimeSpan.FromHours(-5))
            {
                Assert.Ignore("Expects local timezone to be -5");
            }

            var s1 = DateTimeOffset.Parse("12/13/02 14:30:23.4343232211132").ToIso8601();

            Assert.AreEqual("2002-12-13T14:30:23-05:00", s1);
        }

        [Test]
        public void Iso8601_Use_Zulu_Timezone()
        {
            var s2 = DateTimeOffset.Parse("12/13/02 14:30:23.4343232211132Z").ToIso8601();

            Assert.AreEqual("2002-12-13T14:30:23+00:00", s2);
        }

        [Test]
        public void Iso8601WithPrecision_Use_System_Timezone()
        {
            var localZone = TimeZoneInfo.Local;

            if (localZone.BaseUtcOffset != TimeSpan.FromHours(-5))
            {
                Assert.Ignore("Expects local timezone to be -5");
            }

            var s1 = DateTimeOffset.Parse("12/13/02 14:30:23.4343232211132").ToIso8601WithPrecision();

            Assert.AreEqual("2002-12-13T14:30:23.4343232-05:00", s1);
        }

        [Test]
        public void Iso8601WithPrecision_Use_Zulu_Timezone()
        {
            var s2 = DateTimeOffset.Parse("12/13/02 14:30:23.4343232211132Z").ToIso8601WithPrecision();

            Assert.AreEqual("2002-12-13T14:30:23.4343232+00:00", s2);
        }
    }
}