//-----------------------------------------------------------------------
// <copyright file="InteropCsvHelperTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.CsvHelper;

using System;
using System.Globalization;
using System.Linq;
using Abbotware.Core.Extensions;
using Abbotware.Interop.CsvHelper;
using Abbotware.Utility.UnitTest.Using.NUnit;
using global::CsvHelper.Configuration;
using NUnit.Framework;

[TestFixture]
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
        var fi = RootFolder.FileInfo("sample", "data.csv");
        var row = Csv.Parse<DataRow>(fi, new CsvConfiguration(CultureInfo.InvariantCulture)).First();

        Assert.That(row.DateTime, Is.EqualTo(new DateTime(2011, 11, 11)));
        Assert.That(row.Enum, Is.EqualTo(TestEnumeration.Enum1));
        Assert.That(row.Int, Is.EqualTo(123));
        Assert.That(row.String, Is.EqualTo("data"));
    }

    [Test]
    public void SampleCode()
    {
        var fi = RootFolder.FileInfo("sample", "file.csv");
        var records = Csv.Parse<Foo>(fi, new CsvConfiguration(CultureInfo.InvariantCulture));

        Assert.That(records, Is.Not.Null);
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