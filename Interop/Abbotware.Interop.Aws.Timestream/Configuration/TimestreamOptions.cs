// -----------------------------------------------------------------------
// <copyright file="TimestreamOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Timestream.Configuration
{
    /// <summary>
    ///     POCO class for Teamstream configuration parameters
    /// </summary>
    public record class TimestreamOptions
    {
        /// <summary>
        /// Default config section name
        /// </summary>
        public const string DefaultSection = "Timestream";

        /// <summary>
        /// Gets the Database
        /// </summary>
        public string Database { get; init; } = string.Empty;

        /// <summary>
        /// Gets the Table
        /// </summary>
        public string Table { get; init; } = string.Empty;
    }
}