// -----------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Helpers;

using System;
#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Abbotware.Core.Diagnostics;

/// <summary>
///     Helper methods related to enums
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// Gets all the values of the enum
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <returns>all values of the enum</returns>
    public static IEnumerable<TEnum> GetValues<TEnum>()
        where TEnum : struct, Enum
#if NET7_0_OR_GREATER
        => Enum.GetValues<TEnum>();
#else
       => (TEnum[])Enum.GetValues(typeof(TEnum));
#endif

    /// <summary>
    /// Parses a string into an enum value
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="value">string version of enum</param>
    /// <returns>enum value</returns>
    public static TEnum ParseExact<TEnum>(string value)
        where TEnum : struct, Enum
    {
        if (!Enum.TryParse<TEnum>(value, false, out var parsed))
        {
            throw new ArgumentException($"{value} is not valid for enum:{typeof(TEnum).Name}");
        }

        return parsed;
    }

    /// <summary>
    /// Gets the EnumMemberAttribute value of the enum
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="value">value of enum</param>
    /// <returns>enum as string, or EnumMemberAttribute value</returns>
    public static string? GetEnumMemberValue<TEnum>(TEnum? value)
        where TEnum : struct, Enum
    {
        if (value is null)
        {
            return null;
        }

        return GetEnumMemberValue(value.Value);
    }

    /// <summary>
    /// Gets the EnumMemberAttribute value of the enum
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="value">value of enum</param>
    /// <returns>enum as string, or EnumMemberAttribute value</returns>
    public static string GetEnumMemberValue<TEnum>(TEnum value)
        where TEnum : struct, Enum
    {
        var attribute = GetAttribute<TEnum, EnumMemberAttribute>(value);

        return attribute?.Value ?? value.ToString();
    }

    /// <summary>
    /// Gets a value indicating whether or not the enum is marked obsolete
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="value">value of enum</param>
    /// <returns>true if obsolete</returns>
    public static bool IsEnumObsolete<TEnum>(TEnum value)
        where TEnum : struct, Enum
    {
        var attribute = GetAttribute<TEnum, ObsoleteAttribute>(value);

        return attribute != null;
    }

    /// <summary>
    /// Gets the display name value of the enum
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="value">value of enum</param>
    /// <returns>enum as string, or display name value</returns>
    public static string GetDescription<TEnum>(TEnum value)
        where TEnum : struct, Enum
    {
        var attribute = GetAttribute<TEnum, DescriptionAttribute>(value);
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// Gets a specific attribute from the enum member
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <typeparam name="TAttribute">Attribute type</typeparam>
    /// <param name="value">Enum value</param>
    /// <returns>Attribute instance if found, otherwise null</returns>
    public static TAttribute? GetAttribute<TEnum, TAttribute>(TEnum value)
        where TEnum : struct, Enum
        where TAttribute : Attribute
    {
        var (_, memberInfo) = GetMemberInfo<TEnum>(value);
        return ReflectionHelper.SingleOrDefaultAttribute<TAttribute>(memberInfo);
    }

    /// <summary>
    /// Gets a dictionary of enum values to their EnumMemberAttribute values
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <returns>Dictionary of enum values to EnumMemberAttribute values</returns>
    public static IReadOnlyDictionary<TEnum, string> GetEnumToEnumMemberDictionary<TEnum>()
        where TEnum : struct, Enum
        => GetValues<TEnum>().ToDictionary(
            enumValue => enumValue,
            enumValue => GetEnumMemberValue(enumValue))
#if NET8_0_OR_GREATER
            .ToFrozenDictionary();
#else
            ;
#endif

    /// <summary>
    /// Gets a dictionary of EnumMemberAttribute values to their enum values
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <returns>Dictionary of EnumMemberAttribute values to enum values</returns>
    public static IReadOnlyDictionary<string, TEnum> GetEnumMemberToEnumDictionary<TEnum>()
        where TEnum : struct, Enum
        => GetValues<TEnum>().ToDictionary(
            enumValue => GetEnumMemberValue(enumValue),
            enumValue => enumValue)
#if NET8_0_OR_GREATER
            .ToFrozenDictionary();
#else
            ;
#endif

    private static (string Value, MemberInfo MemberInfo) GetMemberInfo<TEnum>(TEnum? value)
        where TEnum : struct, Enum
    {
        var valueString = value.ToString()!;

        var t = value!.GetType();

        var memberInfo = t.GetMember(valueString).
            FirstOrDefault();

        if (memberInfo == null)
        {
            throw new InvalidOperationException($"{value} not part of enum");
        }

        return (valueString, memberInfo);
    }
}
