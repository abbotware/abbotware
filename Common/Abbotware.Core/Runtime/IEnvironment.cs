// -----------------------------------------------------------------------
// <copyright file="IEnvironment.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;

    /// <summary>
    ///     Interface for interacting with the Environment
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        ///     Gets a value indicating whether or not this Operating System is 64bit
        /// </summary>
        bool Is64BitOperatingSystem { get; }

        /// <summary>
        ///     Gets the username the process is running under
        /// </summary>
        string UserName { get; }

        /// <summary>
        ///     Gets the machine name
        /// </summary>
        string MachineName { get; }

        /// <summary>
        ///     Gets the processor count
        /// </summary>
        int ProcessorCount { get; }

        /// <summary>
        ///     Gets the processor frequency
        /// </summary>
        long ProcessorFrequency { get; }

        /// <summary>
        ///     Gets the current executing assembly version
        /// </summary>
        Version SoftwareVersion { get; }

        /// <summary>
        ///     Gets the current executing assembly configuration attribute
        /// </summary>
        string AssemblyConfiguration { get; }

        /// <summary>
        ///     Gets the total amount of memory
        /// </summary>
        long SystemMemory { get; }

        /// <summary>
        ///     Gets the current .Net Runtime version
        /// </summary>
        Version RuntimeVersion { get; }

        /// <summary>
        ///     Gets the current .Net Runtime name
        /// </summary>
        string RuntimeName { get; }
    }
}