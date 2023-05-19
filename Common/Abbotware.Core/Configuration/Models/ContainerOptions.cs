// -----------------------------------------------------------------------
// <copyright file="ContainerOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Configuration.Models
{
    /// <summary>
    /// class for Container options
    /// </summary>
    public class ContainerOptions : IContainerOptions
    {
        /// <inheritdoc/>
        public string Component { get; set; } = "Default";

        /// <inheritdoc/>
        public bool DisableStartable { get; set; }
    }
}
