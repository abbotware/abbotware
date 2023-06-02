// -----------------------------------------------------------------------
// <copyright file="MeasureNameAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Attributes
{
    using System;

    /// <summary>
    /// Attribute to identify a property that is used for measure values
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class MeasureNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasureNameAttribute"/> class.
        /// </summary>
        /// <param name="name">measure name</param>
        public MeasureNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the Measure name
        /// </summary>
        public string Name { get; }
    }
}
