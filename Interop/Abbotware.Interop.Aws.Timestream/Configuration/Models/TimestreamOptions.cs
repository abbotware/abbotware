// -----------------------------------------------------------------------
// <copyright file="TimestreamOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Timestream.Configuration.Models
{
    /// <summary>
    ///     POCO class for Teamstream configuration parameters
    /// </summary>
    public class TimestreamOptions : ITimestreamOptions
    {
        /// <summary>
        /// Default config section name
        /// </summary>
        public const string DefaultSection = "Timestream";

        /// <inheritdoc/>
        public string Region { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Database { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Table { get; set; } = string.Empty;
    }
}