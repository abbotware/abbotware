// -----------------------------------------------------------------------
// <copyright file="IOperatingSystem.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;

    /// <summary>
    ///     Interface for interacting with the Operating System
    /// </summary>
    public interface IOperatingSystem
    {
        /// <summary>
        ///     Gets the time span the system has been on
        /// </summary>
        TimeSpan? SystemUptime { get; }

        /// <summary>
        ///     Reboots the Operating System if the required permissions are held
        /// </summary>
        void Reboot();
    }
}