// -----------------------------------------------------------------------
// <copyright file="Figi.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.SecurityIdentifiers;

using System;

/// <summary>
/// FIGI Helper Functions
/// </summary>
public static class Figi
{
    /// <summary>
    /// Gets the length of a valid FIGI
    /// </summary>
    public const byte ValidLength = 12;

    /// <summary>
    /// static lookup table for characters
    /// </summary>
    private static readonly sbyte[] LookupTable = new sbyte[128];

    static Figi()
    {
        for (var i = 0; i < LookupTable.Length; i++)
        {
            LookupTable[i] = -1;
        }

        for (var c = '0'; c <= '9'; c++)
        {
            LookupTable[c] = (sbyte)(c - '0');
        }

        for (var c = 'A'; c <= 'Z'; c++)
        {
            // Vowels not allowed
            if (c is 'A' or 'E' or 'I' or 'O' or 'U')
            {
                continue;
            }

            LookupTable[c] = (sbyte)(c - 'A' + 10);
        }
    }

    /// <summary>
    /// Validates a given FIGI string based on its structure and checksum.
    /// </summary>
    /// <param name="figi">A ReadOnlySpan representing the FIGI to validate.</param>
    /// <returns>True if the FIGI is valid, otherwise false.</returns>
    public static bool IsValid(ReadOnlySpan<char> figi)
    {
        if (figi.Length != ValidLength)
        {
            return false;
        }

        var checkDigit = ComputeCheckDigit(figi[..(ValidLength - 1)]);

        if (!checkDigit.HasValue)
        {
            return false;
        }

        return checkDigit == LookupTable[figi[^1]];
    }

    /// <summary>
    /// Computes the check digit for a FIGI string.
    /// </summary>
    /// <param name="figi">FIGI prefix without the check digit.</param>
    /// <returns>The computed check digit as an integer.</returns>
    public static int? ComputeCheckDigit(ReadOnlySpan<char> figi)
    {
        if (figi.Length != ValidLength - 1)
        {
            return null;
        }

        sbyte sum = 0;

        for (var i = 0; i < figi.Length; i++)
        {
            var c = figi[i];

            if (c > 127 || LookupTable[c] == -1)
            {
                return null;
            }

            var value = LookupTable[c];

            // Alternate doubling for starting at the leftmost character
            if (i % 2 != 0)
            {
                value *= 2;
            }

            sum += CheckDigit.DigitSum(value);
        }

        return CheckDigit.Modulo10Complement(sum);
    }
}


