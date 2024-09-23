//-----------------------------------------------------------------------
// <copyright file="JsonTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.SystemTextJson
{
    using System;
    using System.Text.Json;
    using Abbotware.Interop.SystemTextJson;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class JsonTests : BaseNUnitTest
    {
        [Test]
        [Category("Interop")]
        [Category("Interop.SystemTextJson")]
        public void CustomConverterTests()
        {
            var a = new TestClass
            {
                TimeSpan = new TimeSpan(1, 2, 3, 4, 5),
                TimeSpanNullableWithValue = new TimeSpan(7, 6, 5, 4, 3),
            };

            var opts = new JsonSerializerOptions();
            opts.Converters.Add(new TimeSpanConverter());

            var text = JsonSerializer.Serialize(a, opts);

            var b = JsonSerializer.Deserialize<TestClass>(text, opts)!;

            Assert.That(a.TimeSpan, Is.EqualTo(b.TimeSpan));
            Assert.That(b.TimeSpanNullableWithNoValue, Is.Null);
            Assert.That(a.TimeSpanNullableWithValue, Is.EqualTo(b.TimeSpanNullableWithValue));
        }
    }
}