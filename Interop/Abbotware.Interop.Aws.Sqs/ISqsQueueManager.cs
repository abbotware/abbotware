// -----------------------------------------------------------------------
// <copyright file="ISqsQueueManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs
{
    using System.Threading.Tasks;

    /// <summary>
    /// interface for the sqs queue mamanger
    /// </summary>
    public interface ISqsQueueManager
    {
        /// <summary>
        /// purges queue of all messages
        /// </summary>
        /// <param name="queue">queue name</param>
        /// <returns>ssync handle</returns>
        Task PurgeAsync(string queue);
    }
}