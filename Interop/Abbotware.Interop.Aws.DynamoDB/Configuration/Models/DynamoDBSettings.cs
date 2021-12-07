// -----------------------------------------------------------------------
// <copyright file="DynamoDBSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.DynamoDB.Configuration.Models
{
    /// <summary>
    ///     Poco class for DynamoDB configuration parameters
    /// </summary>
    public class DynamoDBSettings : IDynamoDBSettings
    {
        /// <summary>
        /// Default config section name
        /// </summary>
        public const string DefaultSection = "DynamoDB";

        /// <inheritdoc/>
        public string Region { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Username { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Password { get; set; } = string.Empty;
    }
}