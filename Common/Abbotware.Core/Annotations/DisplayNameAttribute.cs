// -----------------------------------------------------------------------
// <copyright file="DisplayNameAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Annotations
{
    using System;

    /// <summary>
    /// DisplayName attribute for Enum items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class DisplayNameAttribute : Attribute, IAttributeValue<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class.
        /// </summary>
        /// <param name="value">display name value</param>
        public DisplayNameAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the Value of DisplayName attribute.
        /// </summary>
        public string Value { get; }
    }
}