// -----------------------------------------------------------------------
// <copyright file="ITimestreamOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Timestream.Configuration
{
    /// <summary>
    ///     Read only interface for Teamstream configuration parameters
    /// </summary>
    public interface ITimestreamOptions
    {
        /// <summary>
        /// Gets the region
        /// </summary>
        string Region { get;  }

        /// <summary>
        /// Gets the database name
        /// </summary>
        string Database { get; }

        /// <summary>
        /// Gets the table name
        /// </summary>
        string Table { get; }
    }
}