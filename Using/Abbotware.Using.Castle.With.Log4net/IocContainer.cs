// -----------------------------------------------------------------------
// <copyright file="IocContainer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.With.Log4net
{
    using Abbotware.Core.Helpers;
    using global::Castle.Windsor;

    /// <summary>
    /// Container factory
    /// </summary>
    public static class IocContainer
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
                if (OperatingSystemHelper.IsUnix)
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
        /// creates the container
        /// </summary>
        /// <returns>new container</returns>
        public static IWindsorContainer Create()
        {
            return Create(string.Empty, false, DefaultConfigFile);
        }

        /// <summary>
        /// creates the container
        /// </summary>
        /// <param name="component">name of component</param>
        /// <param name="enableStartable">flag to enable / disable the IStartable facility. (set to false for unit tests)</param>
        /// <param name="logConfigFile">log4net config file</param>
        /// <returns>new container</returns>
        public static IWindsorContainer Create(string component, bool enableStartable, string logConfigFile)
        {
            var c = global::Abbotware.Using.Castle.IocContainer.Create(component, enableStartable);

            c.AddLog4net(component, f => f.WithConfig(logConfigFile));

            return c;
        }
    }
}