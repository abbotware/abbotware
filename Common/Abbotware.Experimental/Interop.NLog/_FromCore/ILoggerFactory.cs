// -----------------------------------------------------------------------
// <copyright file="ILoggerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    /// <summary>
    /// Interface for creating loggers
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create a logger with the supplied name
        /// </summary>
        /// <param name="name">name of logger</param>
        /// <returns>logger</returns>
        ILogger Create(string name);
    }
}