// -----------------------------------------------------------------------
// <copyright file="IApplicationInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;

    /// <summary>
    ///     Interface for useful application information
    /// </summary>
    public interface IApplicationInformation
    {
        /// <summary>
        ///     Gets the current executing assembly version
        /// </summary>
        Version SoftwareVersion { get; }

        /// <summary>
        ///     Gets the current executing assembly configuration attribute
        /// </summary>
        string AssemblyConfiguration { get; }

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