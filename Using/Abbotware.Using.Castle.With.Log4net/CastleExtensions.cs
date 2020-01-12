// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Helpers;
    using Abbotware.Using.Castle.Internal;
    using global::Castle.Windsor;

    /// <summary>
    /// Extension methods for castle registraion
    /// </summary>
    public static class CastleExtensions
    {
        /// <summary>
        ///     name of the default config file
        /// </summary>
        public const string DefaultWindowsLogConfigFile = "log4net.windows.config";

        /// <summary>
        ///     name of the default config file
        /// </summary>
        public const string DefaultLinuxLogConfigFile = "log4net.linux.config";

        /// <summary>
        ///     Gets the default config file name
        /// </summary>
        /// <returns>file name for config file</returns>
        private static string DefaultConfigFile
        {
            get
            {
                if (PlatformHelper.IsUnix)
                {
                    return DefaultLinuxLogConfigFile;
                }
                else
                {
                    return DefaultWindowsLogConfigFile;
                }
            }
        }

        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <returns>contianer for fluent api</returns>
        public static IWindsorContainer AddLog4net(this IWindsorContainer container)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            return container.AddLog4net(DefaultConfigFile);
        }

        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="conifg">log4net config file</param>
        /// <returns>contianer for fluent api</returns>
        public static IWindsorContainer AddLog4net(this IWindsorContainer container, string conifg)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            return container.AddLog4net((o, f) => f.WithConfig(conifg).WithOptions(o));
        }

        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="onCreate">callback to configure</param>
        /// <returns>contianer for fluent api</returns>
        public static IWindsorContainer AddLog4net(this IWindsorContainer container, Action<IContainerOptions, AbbotwareLoggingFacility> onCreate)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var opts = container.Resolve<IContainerOptions>();

            return container.AddFacility<AbbotwareLoggingFacility>(x => onCreate(opts, x));
        }
    }
}
