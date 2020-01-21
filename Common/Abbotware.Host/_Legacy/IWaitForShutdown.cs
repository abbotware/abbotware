// -----------------------------------------------------------------------
// <copyright file="IWaitForShutdown.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Interface that can be used to wait for application shutdown
    /// </summary>
    public interface IWaitForShutdown
    {
        /// <summary>
        ///     waits indefinetly for the application to Shutdown
        /// </summary>
        /// <returns>async task</returns>
        Task ShutdownComplete();
    }
}