// -----------------------------------------------------------------------
// <copyright file="EnumDescriptionConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.CsvHelper.Plugins;

using System;
using System.ComponentModel;
using System.Reflection;
using global::CsvHelper;
using global::CsvHelper.Configuration;
using global::CsvHelper.TypeConversion;

/// <summary>
/// Converts an enum based on the [Description] attribute
/// </summary>
/// <typeparam name="TEnum">enum type</typeparam>
public class EnumDescriptionConverter<TEnum> : DefaultTypeConverter
    where TEnum : Enum
{
    /// <summary>
    /// Gets a value indicating whether or not support null
    /// </summary>
    protected bool Nullable { get; init; }

    /// <inheritdoc/>
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
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

        var fields = typeof(TEnum).GetFields();

        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null && attribute.Description.Equals(text, StringComparison.InvariantCultureIgnoreCase))
            {
                return Enum.Parse(typeof(TEnum), field.Name);
            }
        }

        throw new ArgumentException($"Cannot convert '{text}' to {typeof(TEnum).Name}");
    }

    /// <inheritdoc/>
    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is TEnum enumValue)
        {
            var field = typeof(TEnum).GetField(enumValue.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? enumValue.ToString();
        }

        return base.ConvertToString(value, row, memberMapData);
    }
}
