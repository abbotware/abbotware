// -----------------------------------------------------------------------
// <copyright file="IInMemoryOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Data.Configuration
{
    /// <summary>
    /// Interface for an in memory database configuration
    /// </summary>
    public interface IInMemoryOptions
    {
        /// <summary>
        /// Gets the id for the in memory database instance
        /// </summary>
        string DatabaseId { get; }
    }
}