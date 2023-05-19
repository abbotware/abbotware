// -----------------------------------------------------------------------
// <copyright file="Defaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda.Configuration
{
    using System;

    /// <summary>
    /// Redis Connection Configuration
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Default config section name 'Lambda'
        /// </summary>
        public const string ConfigurationSection = "Lambda";

        /// <summary>
        /// Gets the default lambda time slice '180' seconds
        /// </summary>
        public static TimeSpan LambdaTimeSlice { get; } = TimeSpan.FromSeconds(180);
   }
}