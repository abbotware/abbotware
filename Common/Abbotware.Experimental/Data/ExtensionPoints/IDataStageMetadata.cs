// -----------------------------------------------------------------------
// <copyright file="IDataStageMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints
{
    using System;

    /// <summary>
    ///     Interface for common metadata for data channel stages
    /// </summary>
    public interface IDataStageMetadata
    {
        /// <summary>
        ///     Gets the Start time of the stage
        /// </summary>
        DateTimeOffset? StartTime { get; }

        /// <summary>
        ///     Gets the End time of the stage
        /// </summary>
        DateTimeOffset? EndTime { get; }

        /// <summary>
        ///     Gets the Exception (if there was one) during the stage
        /// </summary>
        Exception? Exception { get; }
    }
}