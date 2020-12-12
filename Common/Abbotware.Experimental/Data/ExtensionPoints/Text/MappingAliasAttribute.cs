// -----------------------------------------------------------------------
// <copyright file="MappingAliasAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Text
{
    using System;

    /// <summary>
    /// Mapping alias for parsed columns
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class MappingAliasAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingAliasAttribute"/> class.
        /// </summary>
        /// <param name="alternateName">alternate name</param>
        public MappingAliasAttribute(string alternateName)
        {
            this.AlternateName = alternateName;
        }

        /// <summary>
        /// Gets the alternate name for the property
        /// </summary>
        public string AlternateName { get; }
    }
}