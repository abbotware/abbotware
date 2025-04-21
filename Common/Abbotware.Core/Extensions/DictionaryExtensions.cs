// -----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions;

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

/// <summary>
///     IDictionary Extension Methods
/// </summary>
public static class DictionaryExtensions
{
#if NET8_0_OR_GREATER
    /// <summary>
    /// Removes an Item from the dictionary or Throws an exception
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TValue">value type</typeparam>
    /// <param name="extended">dictionary being extended</param>
    /// <param name="key">key to removed</param>
    /// <returns>removed value</returns>
    /// <exception cref="KeyNotFoundException">if key not found</exception>
    public static TValue RemoveOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> extended, TKey key)
    where TKey : notnull
    {
        if (!extended.Remove(key, out var found))
        {
            throw new KeyNotFoundException($"key {key} not found");
        }

        return found;
    }
#endif

    /// <summary>
    ///     formats an IDictionary to a string
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <param name="extended">dictionary to convert to a string</param>
    /// <returns>formatted string</returns>
    public static string StringFormat<TKey, TValue>(this IDictionary<TKey, TValue> extended)
        where TKey : notnull
    {
        Arguments.NotNull(extended, nameof(extended));

        return extended.StringFormat("{0}='{1}'");
    }

    /// <summary>
    ///     formats an IDictionary to a string
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <param name="extended">dictionary to convert to a string</param>
    /// <param name="format">format string</param>
    /// <returns>formatted string</returns>
    public static string StringFormat<TKey, TValue>(this IDictionary<TKey, TValue> extended, string format)
        where TKey : notnull
    {
        Arguments.NotNull(extended, nameof(extended));
        Arguments.NotNullOrWhitespace(format, nameof(format));

        if (!extended.Any())
        {
            return "Dictionary Is Empty";
        }

        var aggregate = extended
            .OrderBy(s => s.Key)
            .Aggregate(
                new StringBuilder(),
                (sb, kvp) => KeyValueToString(format, sb, kvp.Key, kvp.Value));

        var retVal = aggregate.ToString();

        if (retVal.IsBlank())
        {
            return "Dictionary Is Empty";
        }

        return retVal;
    }

    /// <summary>
    ///     formats an IDictionary to a string
    /// </summary>
    /// <param name="extended">dictionary to convert to a string</param>
    /// <returns>formatted string</returns>
    public static string StringFormat(this IDictionary extended)
    {
        Arguments.NotNull(extended, nameof(extended));

        return extended.StringFormat("{0}='{1}'");
    }

    /// <summary>
    ///     formats an IDictionary to a string
    /// </summary>
    /// <param name="extended">dictionary to convert to a string</param>
    /// <param name="format">format string</param>
    /// <returns>formatted string</returns>
    public static string StringFormat(this IDictionary extended, string format)
    {
        Arguments.NotNull(extended, nameof(extended));
        Arguments.NotNullOrWhitespace(format, nameof(format));

        var aggregate = extended
            .OfType<DictionaryEntry>()
            .OrderBy(s => s.Key)
            .Aggregate(
                new StringBuilder(),
                (sb, kvp) => KeyValueToString(format, sb, kvp.Key, kvp.Value));

        // Would never have done this if Code Contracts didnt point out the fact that I had a possible null reference in the above LINQ code!
        // I orignally put the ToString() on the above composed LINQ, but that would have been an error if the dictionary passed in was empty
        if (aggregate == null)
        {
            return "Dictionary Is Empty";
        }

        var retVal = aggregate.ToString();

        if (retVal.IsBlank())
        {
            return "Dictionary Is Empty";
        }

        return retVal;
    }

    private static StringBuilder KeyValueToString(string format, StringBuilder sb, object key, object? value) => value switch
    {
        byte[] b => sb.AppendFormat(CultureInfo.InvariantCulture, format, key, Encoding.ASCII.GetString(b)),
        IDictionary d => sb.AppendFormat(CultureInfo.InvariantCulture, format, key, d.StringFormat()),
        _ => sb.AppendFormat(CultureInfo.InvariantCulture, format, key, value),
    };
}