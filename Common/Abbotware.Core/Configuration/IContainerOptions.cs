// -----------------------------------------------------------------------
// <copyright file="IContainerOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Configuration
{
    /// <summary>
    /// interface for container options
    /// </summary>
    public interface IContainerOptions
    {
        /// <summary>
        /// Gets the name of the container
        /// </summary>
        string Component { get; }

        /// <summary>
        /// Gets a value indicating whether to disabled startable components (disable for unit tests)
        /// </summary>
        bool DisableStartable { get; }
    }
}