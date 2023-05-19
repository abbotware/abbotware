// -----------------------------------------------------------------------
// <copyright file="IDynamoDBSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.DynamoDB.Configuration
{
    /// <summary>
    ///     Read only interface for Dynamo DB configuration parameters
    /// </summary>
    public interface IDynamoDBSettings
    {
        /// <summary>
        /// Gets the password
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets the region
        /// </summary>
        string Region { get; }

        /// <summary>
        /// Gets the username
        /// </summary>
        string Username { get; }
    }
}