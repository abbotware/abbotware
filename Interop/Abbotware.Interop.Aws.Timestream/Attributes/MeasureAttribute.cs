﻿// -----------------------------------------------------------------------
// <copyright file="MeasureAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Attributes
{
    using System;

    /// <summary>
    /// Attribute to identify a property that is used for measure values
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MeasureAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string? Name { get; set; }
    }
}
