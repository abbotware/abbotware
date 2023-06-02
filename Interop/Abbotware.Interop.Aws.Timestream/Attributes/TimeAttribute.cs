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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class TimeAttribute : Attribute
    {
        /// <summary>
        /// Gets the time unit type
        /// </summary>
        public TimeUnit TimeUnit { get; } = TimeUnit.MILLISECONDS;
    }
}
