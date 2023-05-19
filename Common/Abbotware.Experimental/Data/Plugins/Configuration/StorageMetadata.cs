// -----------------------------------------------------------------------
// <copyright file="StorageMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using Abbotware.Core.Data.ExtensionPoints.Storage;

    /// <summary>
    ///     Class for Storage Metadata
    /// </summary>
    public class StorageMetadata : DataStageMetadata, IStorageMetadata
    {
    }
}