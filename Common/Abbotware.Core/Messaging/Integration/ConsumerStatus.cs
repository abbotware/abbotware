// -----------------------------------------------------------------------
// <copyright file="ConsumerStatus.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    /// <summary>
    /// The states the Consumer class can be in
    /// </summary>
    public enum ConsumerStatus
    {
        /// <summary>
        /// Consumer has not started
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Consumer is running
        /// </summary>
        Running,

        /// <summary>
        /// Consumer is being cancelled
        /// </summary>
        CancelRequested,

        /// <summary>
        /// Consumer is cancelled
        /// </summary>
        Canceled,

        /// <summary>
        /// Consumer is shutdown
        /// </summary>
        Shutdown,
    }
}