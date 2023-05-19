//-----------------------------------------------------------------------
// <copyright file="JsonTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Newtonsoft
{
    using System;
    using System.Net;
    using Abbotware.Interop.Newtonsoft;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class JsonTests : BaseNUnitTest
    {
        [Test]
        [Category("Interop")]
        [Category("Interop.Newtonsoft")]
        public void CustomConverterTests()
        {
            var a = new TestClass
            {
                IpAddressValue = IPAddress.Parse("1.2.3.4"),
                IPEndPointValue = IPEndPoint.Parse("1.2.3.4:56"),
                TimeSpan = new TimeSpan(1, 2, 3, 4, 5),
                TimeSpanNullableWithValue = new TimeSpan(7, 6, 5, 4, 3),
            };

            var text = JsonHelper.ToString(a);

            var b = JsonHelper.FromString<TestClass>(text)!;

            Assert.IsNull(b.IpAddressNull);
            Assert.AreEqual(a.IpAddressValue, b.IpAddressValue);
            Assert.AreEqual(a.IPEndPointValue, b.IPEndPointValue);
            Assert.IsNull(b.IPEndPointNull);
            Assert.AreEqual(a.TimeSpan, b.TimeSpan);
            Assert.IsNull(b.TimeSpanNullableWithNoValue);
            Assert.AreEqual(a.TimeSpanNullableWithValue, b.TimeSpanNullableWithValue);
        }
    }
}