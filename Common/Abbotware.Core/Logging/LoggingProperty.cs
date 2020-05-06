// -----------------------------------------------------------------------
// <copyright file="LoggingProperty.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    /// <summary>
    /// Common logging properties
    /// </summary>
    public enum LoggingProperty
    {
        /// <summary>
        ///     Component loggingproperty
        /// </summary>
        Component,

        /// <summary>
        ///     Assembly Version logging property
        /// </summary>
        AssemblyVersion,

        /// <summary>
        ///     CommandLine logging property
        /// </summary>
        CommandLine,

        /// <summary>
        ///     Machine Name logging property
        /// </summary>
        MachineName,

        /// <summary>
        ///     Application Name logging property
        /// </summary>
        ApplicationName,

        /// <summary>
        ///     Source Application Name (if any) logging property
        /// </summary>
        SourceApplicationName,
    }
}
