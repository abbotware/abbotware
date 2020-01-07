// -----------------------------------------------------------------------
// <copyright file="Log4netHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Log4net
{
    using System;
    using Abbotware.Core.Helpers;
    using log4net;

    /// <summary>
    ///     Helper class for Log4net
    /// </summary>
    public static class Log4netHelper
    {
        /// <summary>
        ///     name of the GlobalComponentproperty
        /// </summary>
        public const string GlobalComponent = "GlobalComponent";

        /// <summary>
        ///     name of the AssemblyVersion property
        /// </summary>
        public const string AssemblyVersion = "AssemblyVersion";

        /// <summary>
        ///     name of the CommandLine property
        /// </summary>
        public const string CommandLine = "CommandLine";

        /// <summary>
        ///     Sets a log4net global context property for %property{Property}
        /// </summary>
        /// <param name="property">property Name</param>
        /// <param name="value">property value</param>
        public static void SetProperty(string property, string value)
        {
            GlobalContext.Properties[property] = value;
        }

        /// <summary>
        ///     Sets a log4net global context property for %property{GlobalComponent}
        /// </summary>
        /// <param name="componentName">Component Name</param>
        public static void SetComponent(string componentName)
        {
            GlobalContext.Properties[Log4netHelper.GlobalComponent] = componentName;
        }

        /// <summary>
        ///     Sets a log4net global context property for %property{AssemblyVersion}
        /// </summary>
        public static void SetVersion()
        {
            GlobalContext.Properties[AssemblyVersion] = AssemblyHelper.GetExecutingAssemblyVersion();
        }

        /// <summary>
        ///     Sets a log4net global context property for %property{CommandLine}
        /// </summary>
        public static void SetCommandLine()
        {
            GlobalContext.Properties[CommandLine] = Environment.CommandLine;
        }
    }
}