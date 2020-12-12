// -----------------------------------------------------------------------
// <copyright file="InterlockedHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Threading
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    /// Interlocked helper functions
    /// </summary>
    public static class InterlockedHelper
    {
        /// <summary>
        /// Performs an atomic compare and swap
        /// </summary>
        /// <typeparam name="TEnum">enum type</typeparam>
        /// <param name="location">location</param>
        /// <param name="expected">expected value</param>
        /// <param name="next">next value</param>
        /// <returns>The original value @ location</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnum CompareExchange<TEnum>(ref int location, TEnum expected, TEnum next)
            where TEnum : Enum
        {
            var compareIntValue = (int)(object)expected;
            var newIntState = (int)(object)next;

            return (TEnum)(object)Interlocked.CompareExchange(ref location, newIntState, compareIntValue);
        }
    }
}
