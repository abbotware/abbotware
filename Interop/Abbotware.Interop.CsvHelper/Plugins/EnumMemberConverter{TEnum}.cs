// -----------------------------------------------------------------------
// <copyright file="EnumMemberConverter{TEnum}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.CsvHelper.Plugins;

using System;
using System.Collections.Generic;
using Abbotware.Core.Extensions;
using Abbotware.Core.Helpers;
using global::CsvHelper;
using global::CsvHelper.Configuration;
using global::CsvHelper.TypeConversion;

/// <summary>
/// Converts an enum based on the [Description] attribute
/// </summary>
/// <typeparam name="TEnum">enum type</typeparam>
public class EnumMemberConverter<TEnum> : DefaultTypeConverter
    where TEnum : struct, Enum
{
    private readonly IReadOnlyDictionary<string, TEnum> stringToEnum;
    private readonly IReadOnlyDictionary<TEnum, string> enumToString;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumMemberConverter{TEnum}"/> class.
    /// </summary>
    public EnumMemberConverter()
    {
        this.stringToEnum = EnumHelper.GetEnumMemberToEnumDictionary<TEnum>();
        this.enumToString = EnumHelper.GetEnumToEnumMemberDictionary<TEnum>();
    }

    /// <summary>
    /// Gets a value indicating whether or not support null
    /// </summary>
    protected bool Nullable { get; init; }

    /// <inheritdoc/>
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text.IsBlank())
        {
            if (this.Nullable)
            {
                return null;
            }
            else
            {
                throw new ArgumentException($"Cannot convert '{text}' to {typeof(TEnum).Name}");
            }
        }

        if (!this.stringToEnum.TryGetValue(text, out var parsed))
        {
            throw new ArgumentException($"Cannot convert '{text}' to {typeof(TEnum).Name}");
        }

        return parsed;
    }

    /// <inheritdoc/>
    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (this.Nullable && value is null)
        {
            return null;
        }
        else if (!this.Nullable)
        {
            throw new ArgumentException($"Cannot convert {typeof(TEnum).Name} to null");
        }
        else
        {
            if (!this.enumToString.TryGetValue((TEnum)value!, out var asString))
            {
                throw new ArgumentException($"Cannot convert '{value}' to {typeof(TEnum).Name}");
            }

            return asString;
        }
    }
}
