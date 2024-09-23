//-----------------------------------------------------------------------
// <copyright file="InteropYubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.CsvHelper
{
    using System;
    using System.Linq;
    using Abbotware.Core.Helpers;
    using Abbotware.Interop.LumenWorks;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class InteropLumenworksTests : BaseNUnitTest
    {
        public enum TestEnumType
        {
            Enum2,
            Enum1,
        }

        [Test]
        public void SimpleData()
        {
            var row1 = ParserHelper.CsvFile<DataRow>(DirectoryHelper.FindFilePath("data.csv", "Sample"), this.Logger).First();

            Assert.That(row1.DateTime, Is.EqualTo(new DateTime(2011, 11, 11)));
            Assert.That(row1.Enum, Is.EqualTo(TestEnumType.Enum1));
            Assert.That(row1.Enum2, Is.EqualTo(null));
            Assert.That(row1.Int, Is.EqualTo(123));
            Assert.That(row1.String, Is.EqualTo("data"));

            var row2 = ParserHelper.CsvFile<DataRow>(DirectoryHelper.FindFilePath("data.csv", "Sample"), this.Logger).Last();
            Assert.That(row2.Enum2, Is.EqualTo(TestEnumType.Enum2));
        }

        public class DataRow
        {
            public string String { get; set; }

            public int Int { get; set; }

            public DateTime DateTime { get; set; }

            public TestEnumType Enum { get; set; }

            public TestEnumType? Enum2 { get; set; }
        }
    }
}