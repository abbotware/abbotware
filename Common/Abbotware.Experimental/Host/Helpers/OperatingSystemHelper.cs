// -----------------------------------------------------------------------
// <copyright file="OperatingSystemHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host.Helpers
{
    using System;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Runtime;
    using Abbotware.Interop.Linux;
    using Abbotware.Interop.Windows;

    /// <summary>
    /// Operating System Helper functions
    /// </summary>
    public static class OperatingSystemHelper
    {
        /// <summary>
        /// Gets the System.Type for OS specific Implementation of IOperatingSystem
        /// </summary>
        /// <returns>System.Type for OS specific Implementation of IOperatingSystem</returns>
        public static Type OperatingSystemType
        {
            get
            {
                if (PlatformHelper.IsUnix)
                {
                    return typeof(LinuxOperatingSystem);
                }
                else
                {
                    return typeof(WindowsOperatingSystem);
                }
            }
        }

        /// <summary>
        /// Creates the OperatingSystem for based on the OS
        /// </summary>
        /// <returns>OS specific Implementation for IOperatingSystem</returns>
        public static IOperatingSystem CreateOperatingSystem()
        {
            if (PlatformHelper.IsUnix)
            {
                return new LinuxOperatingSystem();
            }
            else
            {
                return new WindowsOperatingSystem();
            }
        }

        /// <summary>
        /// Creates the Environment for based on the OS
        /// </summary>
        /// <returns>OS specific Implementation for IEnvironment</returns>
        public static IEnvironmentInformation CreateEnvironment()
        {
            if (PlatformHelper.IsUnix)
            {
                return new LinuxOperatingSystem();
            }
            else
            {
                return new WindowsOperatingSystem();
            }
        }
    }
}
