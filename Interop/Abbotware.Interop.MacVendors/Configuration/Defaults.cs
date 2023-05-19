// -----------------------------------------------------------------------
// <copyright file="Defaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.MacVendors.Configuration
{
    using System;

    /// <summary>
    /// Configuration defaults
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Gets the base api endpoint
        /// </summary>
        public static Uri Endpoint { get; } = new Uri("https://api.macvendors.com/");
    }
}
