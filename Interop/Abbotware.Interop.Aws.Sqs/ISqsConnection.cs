// -----------------------------------------------------------------------
// <copyright file="ISqsConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs
{
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    /// Interface for an amazon connection
    /// </summary>
    public interface ISqsConnection : IBasicConnection
    {
        /// <summary>
        ///     creates a publisher
        /// </summary>
        /// <returns>publisher</returns>
        ISqsQueueManager CreateQueueManager();
    }
}