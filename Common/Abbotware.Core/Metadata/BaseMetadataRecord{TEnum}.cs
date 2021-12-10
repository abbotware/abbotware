// -----------------------------------------------------------------------
// <copyright file="BaseMetadataRecord{TEnum}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Metadata
{
    using System;

    /// <summary>
    /// Record class for ReadOnly Metadata
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="Id">enum id type</param>
    public abstract record class BaseMetadataRecord<TEnum>(TEnum Id)
        where TEnum : Enum
    {
    }
}