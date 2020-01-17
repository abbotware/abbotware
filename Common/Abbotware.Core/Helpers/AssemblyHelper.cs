// -----------------------------------------------------------------------
// <copyright file="AssemblyHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Static Assembly helper methods
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets the Executing Assembly Version
        /// </summary>
        /// <returns>version</returns>
        public static string GetExecutingAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Find the types {TType} in assembly specified by assemblyPath
        /// </summary>
        /// <typeparam name="TType">type to search</typeparam>
        /// <param name="assemblyPath">assembly path</param>
        /// <returns>collection of types found</returns>
        public static IEnumerable<Type> FindTypes<TType>(FileInfo assemblyPath)
        {
            assemblyPath = Arguments.EnsureNotNull(assemblyPath, nameof(assemblyPath));

            if (!assemblyPath.Exists)
            {
                throw new ArgumentException($"assembly: {assemblyPath} not found");
            }

            var assembly = Assembly.LoadFile(assemblyPath.FullName);

            return FindTypes<TType>(assembly);
        }

        /// <summary>
        /// Find the types in a given assembly.
        /// </summary>
        /// <typeparam name="TType">type to search</typeparam>
        /// <param name="assembly">assembly to search</param>
        /// <returns>set of types found</returns>
        public static IEnumerable<Type> FindTypes<TType>(Assembly assembly)
        {
            assembly = Arguments.EnsureNotNull(assembly, nameof(assembly));

            var registrationType = typeof(TType);

            var registrationTypes = assembly.GetTypes()
                .Where(p => registrationType.IsAssignableFrom(p))
                .ToList();

            return registrationTypes;
        }
    }
}