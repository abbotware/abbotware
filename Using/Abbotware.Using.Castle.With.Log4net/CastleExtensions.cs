// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.With.Log4net
{
    using System;
    using Abbotware.Core;
    using Abbotware.Interop.Log4net;
    using Abbotware.Using.Castle.Internal;
    using global::Castle.Windsor;

    /// <summary>
    /// Extension methods for castle registraion
    /// </summary>
    public static class CastleExtensions
    {
        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="component">component name</param>
        public static void AddLog4net(this IWindsorContainer container, string component)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            CommonInit(component);

            container.AddFacility<AbbotwareLoggingFacility>();
        }

        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="component">component name</param>
        /// <param name="onCreate">callback to configure</param>
        public static void AddLog4net(this IWindsorContainer container, string component, Action<AbbotwareLoggingFacility> onCreate)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            CommonInit(component);

            container.AddFacility(onCreate);
        }

        private static void CommonInit(string component)
        {
            Log4netHelper.SetComponent(component);
            Log4netHelper.SetVersion();
            Log4netHelper.SetCommandLine();
        }
    }
}
