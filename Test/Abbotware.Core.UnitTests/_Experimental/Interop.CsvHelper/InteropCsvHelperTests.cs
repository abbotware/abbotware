//-----------------------------------------------------------------------
// <copyright file="InteropYubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.CsvHelper
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Abbotware.Interop.CsvHelper;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::CsvHelper;
    using global::NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    [Ignore("3rd Party Library seems broken")]
    public class InteropCsvHelperTests : BaseNUnitTest
    {
        public enum TestEnumeration
        {
            Enum2,
            Enum1,
        }

        [Test]
        public void SimpleData()
        {
            var row = ParserHelper.CsvFile<DataRow>(Path.Combine("Samples", "data.csv"), this.Logger).Single();

            Assert.AreEqual(new DateTime(11, 11, 11), row.DateTime);
            Assert.AreEqual(TestEnumeration.Enum1, row.Enum);
            Assert.AreEqual(123, row.Int);
            Assert.AreEqual("data", row.String);
        }

        [Test]
        public void SampleCode()
        {
            using var reader = new StreamReader(Path.Combine("Samples", "file.csv"));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<Foo>();

            Assert.IsNotNull(records);
        }

        public class DataRow
        {
            public string String { get; set; }

            public int Int { get; set; }

            public DateTime DateTime { get; set; }

            public TestEnumeration Enum { get; set; }
        }

        public class Foo
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}