// -----------------------------------------------------------------------
// <copyright file="BaseComponent{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using Abbotware.Core.Logging;

    /// <summary>
    /// Base class for a component that has configuration
    /// </summary>
    /// <typeparam name="TConfiguration">configuration type</typeparam>
    public abstract class BaseComponent<TConfiguration> : BaseComponent, IComponent<TConfiguration>
        where TConfiguration : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseComponent{TConfiguration}"/> class.
        /// </summary>
        /// <param name="configuration">component configuration</param>
        /// <param name="logger">injected logger</param>
        protected BaseComponent(TConfiguration configuration, ILogger logger)
            : base(logger)
        {
            this.Configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));
        }

        /// <summary>
        /// Gets the configuration for the component
        /// </summary>
        public TConfiguration Configuration { get; }
    }
}