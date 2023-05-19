// -----------------------------------------------------------------------
// <copyright file="ISqsSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs.Configuration
{
    /// <summary>
    ///     Read only interface for SQS configuration parameters
    /// </summary>
    public interface ISqsSettings
    {
        /// <summary>
        /// Gets the password
        /// </summary>
        string Password { get;  }

        /// <summary>
        /// Gets the region
        /// </summary>
        string Region { get;  }

        /// <summary>
        /// Gets the username
        /// </summary>
        string Username { get;  }

        /// <summary>
        /// Gets the queue
        /// </summary>
        string Queue { get; }
    }
}