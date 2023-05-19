//-----------------------------------------------------------------------
// <copyright file="TestClass.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.SystemTextJson
{
    using System;

    public class TestClass
    {
        public string Name { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public TimeSpan? TimeSpanNullableWithValue { get; set; }

        public TimeSpan? TimeSpanNullableWithNoValue { get; set; }
    }
}