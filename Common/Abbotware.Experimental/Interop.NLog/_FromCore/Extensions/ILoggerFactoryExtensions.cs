// -----------------------------------------------------------------------
// <copyright file="ILoggerFactoryExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions
{
    using System;
    using Abbotware.Core.Logging;

    /// <summary>
    /// Extensions for the ILoggerFactory interface
    /// </summary>
    public static class ILoggerFactoryExtensions
    {
        /// <summary>
        /// Create a logger for a given type
        /// </summary>
        /// <typeparam name="T">type to set logger name</typeparam>
        /// <param name="factory">Extendee</param>
        /// <returns>logger</returns>
        public static ILogger Create<T>(this ILoggerFactory factory)
            where T : class
        {
            Arguments.NotNull(factory, nameof(factory));

            return factory.Create(typeof(T));
        }

        /// <summary>
        /// Create a logger for a given type
        /// </summary>
        /// <param name="factory">Extendee</param>
        /// <param name="type">type for logger name</param>
        /// <returns>logger</returns>
        public static ILogger Create(this ILoggerFactory factory, Type type)
        {
            Arguments.NotNull(factory, nameof(factory));
            Arguments.NotNull(type, nameof(type));

#pragma warning disable CA1062 // Validate arguments of public methods
            return factory.Create(type.Name);
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}
