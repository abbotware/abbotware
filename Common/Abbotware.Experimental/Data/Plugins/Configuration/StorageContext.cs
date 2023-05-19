// -----------------------------------------------------------------------
// <copyright file="StorageContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using Abbotware.Core.Data.ExtensionPoints.Storage;

    /// <summary>
    /// Class for storage context
    /// </summary>
    public class StorageContext : IStorageContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageContext"/> class.
        /// </summary>
        /// <param name="instanceId">instance id</param>
        public StorageContext(int instanceId)
        {
            this.InstanceId = instanceId;
        }

        /// <inheritdoc/>
        public int InstanceId { get; }
    }
}