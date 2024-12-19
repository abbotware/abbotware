namespace Abbotware.UnitTests.Quant.SecuirtyIdentifiers;

using System;
using Abbotware.Quant.SecurityIdentifiers;
using NUnit.Framework;

public class DigitSumTests
{
    [TestCase(0, ExpectedResult = 0)]
    [TestCase(5, ExpectedResult = 5)]
    [TestCase(10, ExpectedResult = 1)]
    [TestCase(12, ExpectedResult = 3)]
    [TestCase(99, ExpectedResult = 18)]
    [TestCase(42, ExpectedResult = 6)]
    [TestCase(142, ExpectedResult = 7)]
    [TestCase(33242, ExpectedResult = 14)]
    public sbyte Sum(int value) => CheckDigit.DigitSum(value);
}
