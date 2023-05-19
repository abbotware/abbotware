// -----------------------------------------------------------------------
// <copyright file="IOperatingSystemInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;

    /// <summary>
    ///     Interface for useful operating system information
    /// </summary>
    public interface IOperatingSystemInformation
    {
        /// <summary>
        ///     Gets the time span the system has been on
        /// </summary>
        TimeSpan? SystemUptime { get; }
    }
}