﻿// -----------------------------------------------------------------------
// <copyright file="BaseOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Configuration
{
    /// <summary>
    ///   base options class for component{TConfiguration} objects
    /// </summary>
    public abstract class BaseOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the options should be logged
        /// </summary>
        public bool LogOptions { get; set; }
    }
}