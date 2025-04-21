namespace Abbotware.UnitTests.Quant.SecuirtyIdentifiers;

using System;
using Abbotware.Quant.SecurityIdentifiers;
using NUnit.Framework;

public class CusipTests
{
    [TestCase("037833100", ExpectedResult = true)] // Apple Inc.
    [TestCase("594918104", ExpectedResult = true)] // Microsoft Corporation
    [TestCase("02079K107", ExpectedResult = true)] // Alphabet Inc. (Google)
    [TestCase("68389X105", ExpectedResult = true)] // Oracle Corporation
    [TestCase("17275R102", ExpectedResult = true)] // Cisco Systems

    // Invalid CUSIPs
    [TestCase("123456789", ExpectedResult = false)] // Invalid checksum
    [TestCase("037833101", ExpectedResult = false)] // Invalid checksum
    [TestCase("594918105", ExpectedResult = false)] // Invalid checksum
    [TestCase("02079K108", ExpectedResult = false)] // Invalid checksum
    [TestCase("68389X106", ExpectedResult = false)] // Invalid checksum

    // Structural edge cases
    [TestCase("03783310", ExpectedResult = false)] // Too short
    [TestCase("0378331000", ExpectedResult = false)] // Too long
    [TestCase("#########", ExpectedResult = false)] // Invalid characters
    [TestCase("ABCDEFGHI", ExpectedResult = false)] // All alphabetic characters
    [TestCase("12345678@", ExpectedResult = false)] // Special character
    public bool Cusip_IsValid_Tests(string cusip)
        => Cusip.IsValid(cusip.AsSpan());

    [Test]
    public void Cusip_IsValid_HandlesNullOrEmpty()
    {
        Assert.That(() => Cusip.IsValid([]), Is.False);
        Assert.That(() => Cusip.IsValid(string.Empty.AsSpan()), Is.False);
    }
}