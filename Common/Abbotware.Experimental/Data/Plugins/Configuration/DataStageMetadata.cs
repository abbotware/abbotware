// -----------------------------------------------------------------------
// <copyright file="DataStageMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using System;
    using Abbotware.Core.Data.ExtensionPoints;

    /// <summary>
    ///     class for common fields for DataChannel events
    /// </summary>
    public class DataStageMetadata : IDataStageMetadata
    {
        /// <inheritdoc/>
        public DateTimeOffset? StartTime { get; set; } = DateTimeOffset.Now;

        /// <inheritdoc/>
        public DateTimeOffset? EndTime { get; set; }

        /// <inheritdoc/>
        public Exception? Exception { get; set; }
    }
}