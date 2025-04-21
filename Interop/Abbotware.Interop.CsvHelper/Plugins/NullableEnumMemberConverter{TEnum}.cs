// -----------------------------------------------------------------------
// <copyright file="NullableEnumMemberConverter{TEnum}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.CsvHelper.Plugins;

using System;

/// <summary>
/// Converts an enum based on the [EnumMember] attribute - supports null
/// </summary>
/// <typeparam name="TEnum">enum type</typeparam>
public class NullableEnumMemberConverter<TEnum> : EnumMemberConverter<TEnum>
    where TEnum : struct, Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullableEnumMemberConverter{T}"/> class.
    /// </summary>
    public NullableEnumMemberConverter()
        => this.Nullable = true;
}
