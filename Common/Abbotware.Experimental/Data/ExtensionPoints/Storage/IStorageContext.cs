// -----------------------------------------------------------------------
// <copyright file="IStorageContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Storage
{
    /// <summary>
    /// interface for Storage Context
    /// </summary>
    public interface IStorageContext
    {
        /// <summary>
        /// Gets the instance Id for the storage context
        /// </summary>
        int InstanceId { get; }
    }
}