// -----------------------------------------------------------------------
// <copyright file="YesNoBooleanConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.CsvHelper.Plugins;

using Abbotware.Core.Extensions;
using global::CsvHelper;
using global::CsvHelper.TypeConversion;

/// <summary>
/// Convert 'Y' or 'N' to boolean values
/// </summary>
public class YesNoBooleanConverter : DefaultTypeConverter
{
    /// <inheritdoc/>
    public override object ConvertFromString(string? text, IReaderRow row, global::CsvHelper.Configuration.MemberMapData memberMapData)
    {
        if (text.IsBlank())
        {
            return false;
        }

        return text.Trim() switch
        {
            "Y" or "y" => true,
            _ => false,
        };
    }

    /// <inheritdoc/>
    public override string? ConvertToString(object? value, IWriterRow row, global::CsvHelper.Configuration.MemberMapData memberMapData)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "Y" : "N";
        }

        return base.ConvertToString(value, row, memberMapData);
    }
}