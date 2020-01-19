// -----------------------------------------------------------------------
// <copyright file="Property.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using System;

    /// <summary>
    /// Property
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Gets or sets the Field Name
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the Time stamp of value
        /// </summary>
        public DateTimeOffset? Timestamp { get; set; }
    }
}