// -----------------------------------------------------------------------
// <copyright file="BaseConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Configuration
{
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     base class for manager configuration objects
    /// </summary>
    public abstract class BaseConfiguration : IConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseConfiguration" /> class.
        /// </summary>
        protected BaseConfiguration()
        {
            this.LogConfigurationValues = true;
        }

        /// <inheritdoc />
        public bool LogConfigurationValues { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var temp = this.Dump(1);

            return temp;
        }
    }
}