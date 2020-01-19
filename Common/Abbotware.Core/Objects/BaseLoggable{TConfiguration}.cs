// -----------------------------------------------------------------------
// <copyright file="BaseLoggable{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects.Internal;

    /// <summary>
    /// base class for classes that contain a logger and configuration
    /// </summary>
    /// <typeparam name="TConfiguration">configuration type</typeparam>
    public abstract class BaseLoggable<TConfiguration> : BaseLoggable
        where TConfiguration : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseLoggable{TConfiguration}" /> class.
        /// </summary>
        /// <param name="configuration">configuration options</param>
        /// <param name="logger">Injected logger for the class</param>
        protected BaseLoggable(TConfiguration configuration, ILogger logger)
            : base(logger)
        {
            this.Configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));

            ObjectHelper.LogConfiguration(configuration, logger);
        }

        /// <summary>
        /// Gets the configuration for the component
        /// </summary>
        public TConfiguration Configuration { get; }
    }
}