// -----------------------------------------------------------------------
// <copyright file="IRequestShutdown.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host
{
    /// <summary>
    ///     Interface that can be used to Shutdown the application
    /// </summary>
    public interface IRequestShutdown
    {
        /// <summary>
        ///     signal the application its time to Shutdown
        /// </summary>
        void Shutdown();
    }
}