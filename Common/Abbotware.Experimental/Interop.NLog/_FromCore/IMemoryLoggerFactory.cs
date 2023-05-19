// -----------------------------------------------------------------------
// <copyright file="IMemoryLoggerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for an in-memory logger
    /// </summary>
    public interface IMemoryLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Gets the memory logged messages
        /// </summary>
        IEnumerable<string> Messages { get; }
    }
}