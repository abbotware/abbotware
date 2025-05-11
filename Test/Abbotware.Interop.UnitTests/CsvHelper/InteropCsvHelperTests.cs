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
using System.Runtime.Serialization;
using Abbotware.Core.Extensions;
using Abbotware.Interop.CsvHelper;
using Abbotware.Interop.CsvHelper.Plugins;
using Abbotware.Utility.UnitTest.Using.NUnit;
using global::CsvHelper.Configuration;
using global::CsvHelper.Configuration.Attributes;
using NUnit.Framework;

[TestFixture]
public class InteropCsvHelperTests : BaseNUnitTest
{
    public enum TestEnumeration
    {
        Enum2,
        Enum1,
    }

    public enum TestEnumeration2
    {
        [EnumMember(Value = "ENUM2")]
        Enum2,
        [EnumMember(Value = "ENUM1")]
        Enum1,
    }

    [Test]
    public void SimpleData()
    {
        var fi = RootFolder.FileInfoForExisting("sample", "data.csv");
        var row1 = Csv.Parse<DataRow>(fi, new CsvConfiguration(CultureInfo.InvariantCulture)).First();

        Assert.That(row1.DateTime, Is.EqualTo(new DateTime(2011, 11, 11)));
        Assert.That(row1.Enum, Is.EqualTo(TestEnumeration.Enum1));
        Assert.That(row1.EnumMember, Is.EqualTo(TestEnumeration2.Enum1));
        Assert.That(row1.NullableEnumMember, Is.Null);
        Assert.That(row1.Int, Is.EqualTo(123));
        Assert.That(row1.String, Is.EqualTo("data"));

        var row2 = Csv.Parse<DataRow>(fi, new CsvConfiguration(CultureInfo.InvariantCulture)).Last();
        Assert.That(row2.DateTime, Is.EqualTo(new DateTime(2011, 11, 11)));
        Assert.That(row2.Enum, Is.EqualTo(TestEnumeration.Enum1));
        Assert.That(row2.EnumMember, Is.EqualTo(TestEnumeration2.Enum2));
        Assert.That(row2.NullableEnumMember, Is.EqualTo(TestEnumeration2.Enum2));
        Assert.That(row2.Int, Is.EqualTo(123));
        Assert.That(row2.String, Is.EqualTo("data"));
    }

    [Test]
    public void SampleCode()
    {
        var fi = RootFolder.FileInfoForExisting("sample", "file.csv");
        var records = Csv.Parse<Foo>(fi, new CsvConfiguration(CultureInfo.InvariantCulture));

        Assert.That(records, Is.Not.Null);
    }

    public class DataRow
    {
        public string String { get; set; }

        public int Int { get; set; }

        public DateTime DateTime { get; set; }

        public TestEnumeration Enum { get; set; }

        //// public TestEnumeration NullableEnum { get; set; }

        [TypeConverter(typeof(EnumMemberConverter<TestEnumeration2>))]
        public TestEnumeration2 EnumMember { get; set; }

        [TypeConverter(typeof(NullableEnumMemberConverter<TestEnumeration2>))]
        public TestEnumeration2? NullableEnumMember { get; set; }
    }

    public class Foo
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}