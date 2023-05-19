// -----------------------------------------------------------------------
// <copyright file="BaseLoggable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using Abbotware.Core.Logging;

    /// <summary>
    /// base class for classes that contain a logger
    /// </summary>
    public abstract class BaseLoggable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseLoggable" /> class.
        /// </summary>
        /// <param name="logger">Injected logger for the class</param>
        protected BaseLoggable(ILogger logger)
        {
            this.Logger = Arguments.EnsureNotNull(logger, nameof(logger));

            this.Logger = logger;
        }

        /// <summary>
        ///     Gets the logger for the class
        /// </summary>
        protected virtual ILogger Logger { get; }
    }
}