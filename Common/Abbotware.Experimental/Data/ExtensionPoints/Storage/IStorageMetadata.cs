// -----------------------------------------------------------------------
// <copyright file="IStorageMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Storage
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Interface for Storage Metadata
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Used to enforce type inheritance")]
    public interface IStorageMetadata : IDataStageMetadata
    {
    }
}