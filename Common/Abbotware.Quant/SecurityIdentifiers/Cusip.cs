// -----------------------------------------------------------------------
// <copyright file="Cusip.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.SecurityIdentifiers;

using System;

/// <summary>
/// CUSIP Helper Functions
/// </summary>
public static class Cusip
{
    /// <summary>
    /// Gets the length of a valid CUSIP
    /// </summary>
    public const byte ValidLength = 9;

    private static readonly sbyte[] LookupTable = new sbyte[128];

    static Cusip()
    {
        // Initialize all entries to -1 (sentinel for invalid characters)
        for (var i = 0; i < LookupTable.Length; i++)
        {
            LookupTable[i] = -1;
        }

        // Set values for '0'-'9' (ASCII 48-57)
        for (var c = '0'; c <= '9'; c++)
        {
            LookupTable[c] = (sbyte)(c - '0');
        }

        // Set values for 'A'-'Z' (ASCII 65-90)
        for (var c = 'A'; c <= 'Z'; c++)
        {
            LookupTable[c] = (sbyte)(c - 'A' + 10);
        }

        // Set values for special characters '*' (42), '@' (64), '#' (35)
        LookupTable['*'] = 36;
        LookupTable['@'] = 37;
        LookupTable['#'] = 38;
    }

    /// <summary>
    /// Validates a given CUSIP string based on its structure and checksum.
    /// </summary>
    /// <param name="cusip">A ReadOnlySpan representing the CUSIP to validate.</param>
    /// <returns>True if the CUSIP is valid, otherwise false.</returns>
    public static bool IsValid(ReadOnlySpan<char> cusip)
    {
        if (cusip.Length != ValidLength)
        {
            return false;
        }

        var checkDigit = ComputeCheckDigit(cusip[..(ValidLength - 1)]);

        if (!checkDigit.HasValue)
        {
            return false;
        }

        return checkDigit == LookupTable[cusip[^1]];
    }

    /// <summary>
    /// Comptues the check digit for a CUSIP string
    /// </summary>
    /// <param name="cusip">CUSIP string</param>
    /// <returns>the 9th check digit</returns>
    public static int? ComputeCheckDigit(ReadOnlySpan<char> cusip)
    {
        if (cusip.Length != 8)
        {
            ////throw new ArgumentException("CUSIP prefix must be exactly 8 characters.");
            return null;
        }

        sbyte sum = 0;

        for (var i = 0; i < 8; i++)
        {
            var c = cusip[i];

            if (c > 127 || LookupTable[c] == -1)
            {
                ////throw new ArgumentException($"Invalid character '{c}'.");
                return null;
            }

            var value = LookupTable[c];

            if (i % 2 != 0)
            {
                value *= 2;
            }

            sum += CheckDigit.DigitSum(value);
        }

        return CheckDigit.Modulo10Complement(sum);
    }
}