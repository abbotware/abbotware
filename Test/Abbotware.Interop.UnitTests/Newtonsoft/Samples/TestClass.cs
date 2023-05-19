//-----------------------------------------------------------------------
// <copyright file="TestClass.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Newtonsoft
{
    using System;
    using System.Net;

    public class TestClass
    {
        public string Name { get; set; }

        public IPAddress IpAddressValue { get; set; }

        public IPAddress IpAddressNull { get; set; }

        public IPEndPoint IPEndPointValue { get; set; }

        public IPEndPoint IPEndPointNull { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public TimeSpan? TimeSpanNullableWithValue { get; set; }

        public TimeSpan? TimeSpanNullableWithNoValue { get; set; }
    }
}