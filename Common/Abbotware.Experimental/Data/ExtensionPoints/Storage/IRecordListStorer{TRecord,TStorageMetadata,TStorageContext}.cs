﻿// -----------------------------------------------------------------------
// <copyright file="IRecordListStorer{TRecord,TStorageMetadata,TStorageContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Storage
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Encapulates a way to store a data list
    /// </summary>
    /// <typeparam name="TRecord">type of data</typeparam>
    /// <typeparam name="TStorageMetadata">type of metadata</typeparam>
    /// <typeparam name="TStorageContext">type of context</typeparam>
    public interface IRecordListStorer<TRecord, TStorageMetadata, TStorageContext> : IRecordStorer<IEnumerable<TRecord>, TStorageMetadata, TStorageContext>
        where TStorageMetadata : IStorageMetadata
    {
    }
}