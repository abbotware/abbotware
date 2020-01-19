// -----------------------------------------------------------------------
// <copyright file="ObjectHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Objects.Internal
{
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;

    /// <summary>
    /// Helper methods
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Logs the configuration values if the config class has a LogOptions property that is set to true
        /// </summary>
        /// <typeparam name="TConfiguration">configuration type</typeparam>
        /// <param name="configuration">configuration options</param>
        /// <param name="logger">logger</param>
        public static void LogConfiguration<TConfiguration>(TConfiguration configuration, ILogger logger)
            where TConfiguration : class
        {
            configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));
            logger = Arguments.EnsureNotNull(logger, nameof(logger));

            var logConfig = ReflectionHelper.GetPropertyValueAsStruct<bool>(configuration, nameof(BaseOptions.LogOptions));

            if (!logConfig.HasValue)
            {
                return;
            }

            if (!logConfig.Value)
            {
                return;
            }

            var cfg = configuration.Dump(1);

            logger.Debug(cfg);
        }
    }
}
