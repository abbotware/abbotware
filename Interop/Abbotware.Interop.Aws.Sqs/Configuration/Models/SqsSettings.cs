// -----------------------------------------------------------------------
// <copyright file="SqsSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs.Configuration.Models
{
    /// <summary>
    ///     POCO class for SQS configuration parameters
    /// </summary>
    public class SqsSettings : ISqsSettings
    {
        /// <summary>
        /// Default config section name
        /// </summary>
        public const string DefaultSection = "SQS";

        /// <inheritdoc/>
        public string Region { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Username { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Password { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Queue { get; set; } = string.Empty;
    }
}