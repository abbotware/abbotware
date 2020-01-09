// -----------------------------------------------------------------------
// <copyright file="IConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Configuration
{
    /// <summary>
    ///     Manager configuration
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        ///     Gets a value indicating whether or not the configuration should be logged
        /// </summary>
        bool LogConfigurationValues { get; }
    }
}