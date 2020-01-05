// -----------------------------------------------------------------------
// <copyright file="OperatingSystemHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Helpers
{
    using System;

    /// <summary>
    ///     Helper methods for operating system
    /// </summary>
    public static class OperatingSystemHelper
    {
        /// <summary>
        ///     Gets a value indicating whether the operating system is linux
        /// </summary>
        public static bool IsUnix
        {
            get
            {
                var p = (int)Environment.OSVersion.Platform;
                return p == (int)PlatformID.Unix || p == (int)PlatformID.MacOSX || p == 128;
            }
        }
    }
}