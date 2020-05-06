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
    using Abbotware.Core.Logging;
    using log4net;

    /// <summary>
    ///     Helper class for Log4net
    /// </summary>
    public static class Log4netHelper
    {
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
        ///     Sets a log4net global context property for %property{Component}
        /// </summary>
        /// <param name="componentName">Component Name</param>
        public static void SetComponent(string componentName)
        {
            GlobalContext.Properties[nameof(LoggingProperty.Component)] = componentName;
        }

        /// <summary>
        /// Sets common global properies
        /// </summary>
        /// <param name="componentName">Component Name</param>
        public static void SetCommonProperties(string componentName)
        {
            SetComponent(componentName);
            SetCommandLine();
            SetVersion();
            SetMachineName();
        }

        /// <summary>
        ///     Sets a log4net global context property for %property{AssemblyVersion}
        /// </summary>
        public static void SetVersion()
        {
            GlobalContext.Properties[nameof(LoggingProperty.AssemblyVersion)] = AssemblyHelper.GetExecutingAssemblyVersion();
        }

        /// <summary>
        ///     Sets a log4net global context property for %property{CommandLine}
        /// </summary>
        public static void SetCommandLine()
        {
            GlobalContext.Properties[nameof(LoggingProperty.CommandLine)] = Environment.CommandLine;
        }

        /// <summary>
        ///     Sets a log4net global context property for %property{MachineName}
        /// </summary>
        public static void SetMachineName()
        {
            GlobalContext.Properties[nameof(LoggingProperty.MachineName)] = Environment.MachineName;
        }

         /// <summary>
        /// begins a new scope adding the key values to the context
        /// </summary>
        /// <param name="context">contextual string</param>
        /// <remarks>For a primer on NDC see comment 'Want an example?' on this post:
        /// https://stackoverflow.com/questions/334367/when-to-use-nested-diagnostic-context-ndc
        /// </remarks>
        /// <returns>disposable for using block</returns>
        public static IDisposable BeginScope(string context)
        {
            return ThreadContext.Stacks["NDC"].Push(context);
        }
    }
}