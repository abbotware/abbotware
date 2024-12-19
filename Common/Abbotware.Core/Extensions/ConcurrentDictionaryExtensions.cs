// -----------------------------------------------------------------------
// <copyright file="ConcurrentDictionaryExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET8_0_OR_GREATER
namespace Abbotware.Core.Extensions;

using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.CompilerServices;

/// <summary>
///     ConcurrentDictionary Extension Methods
/// </summary>
public static class ConcurrentDictionaryExtensions
{
    /// <summary>
    /// Adds an item to the CC Dictionary, otherwise throws
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TValue">value type</typeparam>
    /// <param name="extended">extended ConcurrentDictionary</param>
    /// <param name="key">key to add</param>
    /// <param name="value">value to add</param>
    /// <returns>added value</returns>
    /// <exception cref="InvalidOperationException">thrown if key already exists</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]

    public static TValue AddOrThrow<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> extended, TKey key, TValue value)
        where TKey : notnull
        => extended.AddOrUpdate(key, value, static (k, u) => throw new InvalidOperationException($"duplicate key:{k}"));

    /// <summary>
    /// Get or Create the sub dictionary
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TInnerValue">inner dictionary value type</typeparam>
    /// <param name="extended">extended ConcurrentDictionary</param>
    /// <param name="key">key to get or add</param>
    /// <returns>new or existing value</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ConcurrentDictionary<string, TInnerValue> GetOrAddInvariantDictionary<TKey, TInnerValue>(this ConcurrentDictionary<TKey, ConcurrentDictionary<string, TInnerValue>> extended, TKey key)
        where TKey : notnull
        => extended.GetOrAdd(key, new ConcurrentDictionary<string, TInnerValue>(StringComparer.InvariantCultureIgnoreCase));

    /// <summary>
    /// Pseudo Interlocked.Increment via a ConcurrentDictionary
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TNumeric">number type</typeparam>
    /// <param name="extended">extended ConcurrentDictionary</param>
    /// <param name="key">key to increment</param>
    /// <returns>updated value</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static TNumeric Increment<TKey, TNumeric>(this ConcurrentDictionary<TKey, TNumeric> extended, TKey key)
        where TKey : notnull
        where TNumeric : INumber<TNumeric>
        => extended.Add(key, TNumeric.One);

    /// <summary>
    /// Pseudo Interlocked.Add via a ConcurrentDictionary
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TNumeric">number type</typeparam>
    /// <param name="extended">extended ConcurrentDictionary</param>
    /// <param name="key">key to update</param>
    /// <param name="amount">amount to add to key</param>
    /// <returns>updated value</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static TNumeric Add<TKey, TNumeric>(this ConcurrentDictionary<TKey, TNumeric> extended, TKey key, TNumeric amount)
        where TKey : notnull
        where TNumeric : INumber<TNumeric>
        => extended.AddOrUpdate(key, static (k, arg) => arg, static (k, arg, prev) => prev + arg, amount);
}
#endif