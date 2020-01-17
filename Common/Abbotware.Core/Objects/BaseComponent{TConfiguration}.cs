// -----------------------------------------------------------------------
// <copyright file="BaseComponent{TConfiguration}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Extensions;
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

            this.LogConfiguration();
        }

        /// <summary>
        /// Gets the configuration for the component
        /// </summary>
        public TConfiguration Configuration { get; }

        private void LogConfiguration()
        {
            var logConfig = ReflectionHelper.GetPropertyValueAsStruct<bool>(this.Configuration, nameof(BaseOptions.LogOptions));

            if (!logConfig.HasValue)
            {
                return;
            }

            if (!logConfig.Value)
            {
                return;
            }

            var cfg = this.Configuration.Dump(1);

            this.Logger.Debug(cfg);
        }
    }
}