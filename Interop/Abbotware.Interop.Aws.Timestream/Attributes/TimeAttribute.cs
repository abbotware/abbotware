// -----------------------------------------------------------------------
// <copyright file="TimeAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Attributes
{
    using System;
    using Amazon.TimestreamWrite;

    /// <summary>
    /// Attribute to identify a property that is used for measure values
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class TimeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the time unit type
        /// </summary>
        public TimeUnit TimeUnit { get; set; } = TimeUnit.MILLISECONDS;
    }
}
