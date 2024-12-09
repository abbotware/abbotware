// -----------------------------------------------------------------------
// <copyright file="NullableEnumDescriptionConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.CsvHelper.Plugins;

using System;

/// <summary>
/// Converts an enum based on the [Description] attribute - supports null
/// </summary>
/// <typeparam name="TEnum">enum type</typeparam>
public class NullableEnumDescriptionConverter<TEnum> : EnumDescriptionConverter<TEnum>
    where TEnum : Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullableEnumDescriptionConverter{T}"/> class.
    /// </summary>
    public NullableEnumDescriptionConverter()
    {
        this.Nullable = true;
    }
}
