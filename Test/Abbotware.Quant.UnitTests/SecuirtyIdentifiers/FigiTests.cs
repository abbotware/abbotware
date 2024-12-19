namespace Abbotware.UnitTests.Quant.SecuirtyIdentifiers;

using System;
using Abbotware.Quant.SecurityIdentifiers;
using NUnit.Framework;

[TestFixture]
public class FigiTests
{
    [TestCase("BBG000BLNNH6", ExpectedResult = true)] // Apple Inc.
    [TestCase("BBG000B9XRY4", ExpectedResult = true)] // Microsoft Corporation
    [TestCase("BBG001SCTQY4", ExpectedResult = true)] // Meta Platforms Inc. (Facebook)
    [TestCase("BBG00178PGX3", ExpectedResult = true)] // Berkshire Hathaway Class B
    [TestCase("BBG000BLNNH6", ExpectedResult = true)] // Valid FIGI

    // Invalid FIGIs
    [TestCase("BBG000BLNNHX", ExpectedResult = false)] // Invalid check digit (not numeric)
    [TestCase("BBG000BLNNH5", ExpectedResult = false)] // Invalid checksum
    [TestCase("BBG000BLNNH7", ExpectedResult = false)] // Invalid checksum
    [TestCase("BBG000B9XRY5", ExpectedResult = false)] // Invalid checksum
    [TestCase("BBG000F2CWY4", ExpectedResult = false)] // Invalid checksum
    [TestCase("BBG000BLNNH", ExpectedResult = false)] // Too short
    [TestCase("BBG000BLNNH67", ExpectedResult = false)] // Too long
    [TestCase("BBG000B9XRA4", ExpectedResult = false)] // Contains vowel 'A'
    [TestCase("BBO000BLNNH6", ExpectedResult = false)] // Contains vowel 'O'

    // Structural rule violations
    [TestCase("BSG000BLNNH6", ExpectedResult = false)] // Invalid prefix 'BS'
    [TestCase("BMG000BLNNH6", ExpectedResult = false)] // Invalid prefix 'BM'
    [TestCase("GGG000BLNNH6", ExpectedResult = false)] // Invalid prefix 'GG'
    [TestCase("VGG000BLNNH6", ExpectedResult = false)] // Invalid prefix 'VG'
    [TestCase("BBT100BLNNH6", ExpectedResult = false)] // Invalid third character
    public bool Figi_IsValid_Tests(string figi)
        => Figi.IsValid(figi.AsSpan());

    [Test]
    public void Figi_IsValid_HandlesNullOrEmpty()
    {
        Assert.That(() => Figi.IsValid([]), Is.False);
        Assert.That(() => Figi.IsValid(string.Empty.AsSpan()), Is.False);
    }
}
