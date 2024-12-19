// -----------------------------------------------------------------------
// <copyright file="CheckDigit.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.SecurityIdentifiers;

using System.Runtime.CompilerServices;

/// <summary>
/// Helper methods for Check Digit computation
/// </summary>
public static class CheckDigit
{
    /// <summary>
    /// Gets the Modulo 10 Complement
    /// </summary>
    /// <param name="value">value to complement</param>
    /// <returns>modulo 10 Complement</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int Modulo10Complement(sbyte value)
        => ModuloComplement(value, 10);

    /// <summary>
    /// Gets the Modulo 10 Complement
    /// </summary>
    /// <param name="value">value to complement</param>
    /// <param name="mod">modulus</param>
    /// <returns>modulo 10 Complement</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int ModuloComplement(sbyte value, int mod)
        => (mod - (value % mod)) % mod;

    /// <summary>
    /// Adds the Digits of a number
    /// </summary>
    /// <remarks>12 => 1 + 2 = 3</remarks>
    /// <param name="value">value to sum</param>
    /// <returns>sum of digits</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static sbyte DigitSum(int value)
        => value switch
        {
            < 10 => (sbyte)value,
            < 100 => (sbyte)((value / 10) + (value % 10)),
            _ => GeneralDigitSum(value),
        };

    private static sbyte GeneralDigitSum(int value)
    {
        var sum = 0;

        while (value > 0)
        {
            sum += value % 10;
            value /= 10;
        }

        return (sbyte)sum;
    }
}