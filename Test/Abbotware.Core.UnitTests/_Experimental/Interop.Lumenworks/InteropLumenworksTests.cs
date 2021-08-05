//-----------------------------------------------------------------------
// <copyright file="InteropYubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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

            Assert.AreEqual(new DateTime(2011, 11, 11), row1.DateTime);
            Assert.AreEqual(TestEnumType.Enum1, row1.Enum);
            Assert.AreEqual(null, row1.Enum2);
            Assert.AreEqual(123, row1.Int);
            Assert.AreEqual("data", row1.String);

            var row2 = ParserHelper.CsvFile<DataRow>(DirectoryHelper.FindFilePath("data.csv", "Sample"), this.Logger).Last();
            Assert.AreEqual(TestEnumType.Enum2, row2.Enum2);
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