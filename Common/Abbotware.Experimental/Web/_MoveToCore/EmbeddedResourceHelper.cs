// -----------------------------------------------------------------------
// <copyright file="EmbeddedResourceHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Sid Sacek</author>

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Helper Methods that deal with embedded resources
    /// </summary>
    public static class EmbeddedResourceHelper
    {
        /// <summary>
        /// internal dictionary of module names mapped to assemblies
        /// </summary>
        private static readonly Dictionary<string, Assembly> Modules = new();

        /// <summary>
        /// Retrieves a module containing resources, first looking for it in the modules cache,
        /// then loading it if unsuccessful, and adding it to the modules cache for future use
        /// </summary>
        /// <param name="moduleName">The name of the module to retrieve</param>
        /// <returns>The retrieved module containing resources</returns>
        public static Assembly GetResourceModule(string moduleName)
        {
            moduleName = Arguments.EnsureNotNull(moduleName, nameof(moduleName));

            var uppercase = moduleName.ToUpperInvariant();

            lock (EmbeddedResourceHelper.Modules)
            {
                if (!EmbeddedResourceHelper.Modules.TryGetValue(uppercase, out Assembly? module))
                {
                    module = Assembly.Load(moduleName);
                    EmbeddedResourceHelper.Modules[uppercase] = module;
                }

                return module;
            }
        }

        /// <summary>
        /// Retrieves a module containing resources, looking for it only in the modules cache
        /// </summary>
        /// <param name="moduleName">The name of the module to retrieve</param>
        /// <returns>The retrieved module containing resources</returns>
        public static Assembly GetCachedResourceModule(string moduleName)
        {
            moduleName = Arguments.EnsureNotNull(moduleName, nameof(moduleName));

            var uppercase = moduleName.ToUpperInvariant();

            lock (EmbeddedResourceHelper.Modules)
            {
                if (!EmbeddedResourceHelper.Modules.TryGetValue(uppercase, out Assembly? module))
                {
                    throw new KeyNotFoundException("uppercase");
                }

                return module;
            }
        }

        /// <summary>
        /// Retrieves a binary file from the assembly's embedded resources in the form of a byte array
        /// </summary>
        /// <param name="assemblyName">The name of the assembly module</param>
        /// <param name="resourcePath">The full path of the embedded resource (excluding the module part)</param>
        /// <returns>The binary file's contents as a byte array</returns>
        public static byte[] GetBinaryFile(string assemblyName, string resourcePath)
        {
            Arguments.NotNullOrWhitespace(assemblyName, nameof(assemblyName));
            Arguments.NotNullOrWhitespace(resourcePath, nameof(resourcePath));

            using var stream = EmbeddedResourceHelper.GetResourceStream(assemblyName, resourcePath);
            using var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);
            byte[] result = memoryStream.ToArray();
            return result;
        }

        /// <summary>
        /// Retrieves a binary file from the specified assembly's embedded resources in the form of a byte array
        /// </summary>
        /// <param name="assembly">The assembly module from which the binary resource will be retrieved</param>
        /// <param name="assemblyName">The name of the assembly module</param>
        /// <param name="resourcePath">The full path of the embedded resource (excluding the module part)</param>
        /// <returns>The binary file's contents as a byte array</returns>
        public static byte[] GetBinaryFile(Assembly assembly, string assemblyName, string resourcePath)
        {
            Arguments.NotNull(assembly, nameof(assembly));
            Arguments.NotNullOrWhitespace(assemblyName, nameof(assemblyName));
            Arguments.NotNullOrWhitespace(resourcePath, nameof(resourcePath));

            using var stream = EmbeddedResourceHelper.GetResourceStream(assembly, assemblyName, resourcePath);
            using var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);
            byte[] result = memoryStream.ToArray();
            return result;
        }

        /// <summary>
        /// Retrieves a text file from an assembly's embedded resources in the form of a string
        /// </summary>
        /// <param name="assemblyName">The name of the assembly module</param>
        /// <param name="resourcePath">The full path of the embedded resource (excluding the module part)</param>
        /// <returns>The text file's contents as a string</returns>
        public static string GetTextFile(string assemblyName, string resourcePath)
        {
            Arguments.NotNullOrWhitespace(assemblyName, nameof(assemblyName));
            Arguments.NotNullOrWhitespace(resourcePath, nameof(resourcePath));

            using var stream = EmbeddedResourceHelper.GetResourceStream(assemblyName, resourcePath);
            using StreamReader reader = new(stream);

            string result = reader.ReadToEnd();
            return result;
        }

        /// <summary>
        /// Retrieves a text file from the specified assembly's embedded resources in the form of a string
        /// </summary>
        /// <param name="assembly">The assembly module from which the text resource will be retrieved</param>
        /// <param name="assemblyName">The name of the assembly module</param>
        /// <param name="resourcePath">The full path of the embedded resource (excluding the module part)</param>
        /// <returns>The text file's contents as a string</returns>
        public static string GetTextFile(Assembly assembly, string assemblyName, string resourcePath)
        {
            Arguments.NotNull(assembly, nameof(assembly));
            Arguments.NotNullOrWhitespace(assemblyName, nameof(assemblyName));
            Arguments.NotNullOrWhitespace(resourcePath, nameof(resourcePath));

            using var stream = EmbeddedResourceHelper.GetResourceStream(assembly, assemblyName, resourcePath);
            using StreamReader reader = new(stream);

            string result = reader.ReadToEnd();
            return result;
        }

        /// <summary>
        /// Returns a stream to an assembly's embedded resource
        /// </summary>
        /// <param name="assemblyName">The name of the assembly module</param>
        /// <param name="resourcePath">The full path of the embedded resource (excluding the module part)</param>
        /// <returns>byte stream</returns>
        public static Stream GetResourceStream(string assemblyName, string resourcePath)
        {
            Arguments.NotNullOrWhitespace(assemblyName, nameof(assemblyName));
            Arguments.NotNullOrWhitespace(resourcePath, nameof(resourcePath));

            var assembly = EmbeddedResourceHelper.GetResourceModule(assemblyName);
            var stream = GetResourceStream(assembly, assemblyName, resourcePath);
            return stream;
        }

        /// <summary>
        /// Returns a stream to the specified assembly's embedded resource
        /// </summary>
        /// <param name="assembly">The assembly module from which the resource will be located</param>
        /// <param name="assemblyName">The name of the assembly module</param>
        /// <param name="resourcePath">The full path of the embedded resource (excluding the module part)</param>
        /// <returns>byte stream</returns>
        public static Stream GetResourceStream(Assembly assembly, string assemblyName, string resourcePath)
        {
            assembly = Arguments.EnsureNotNull(assembly, nameof(assembly));
            assemblyName = Arguments.EnsureNotNullOrWhitespace(assemblyName, nameof(assemblyName));
            resourcePath = Arguments.EnsureNotNullOrWhitespace(resourcePath, nameof(resourcePath));

            var resource = EmbeddedResourceHelper.NormalizeFullPath(FormattableString.Invariant($"{assemblyName}/{resourcePath}"));

            Stream? stream = assembly.GetManifestResourceStream(resource);

            if (stream == null)
            {
                throw new FileNotFoundException("Cannot find the following embedded file: " + resource);
            }

            return stream;
        }

        /// <summary>
        /// Normalize name
        /// </summary>
        /// <param name="name">name to normalize</param>
        /// <returns>normalized string</returns>
        private static string NormalizeName(string name)
        {
            name = name.Trim(' ', '.', '/', '\\');
            name = name.Replace('-', '_');

            if (name.Contains('.', StringComparison.OrdinalIgnoreCase))
            {
                // if a segment starts with a digit, then inject an underscore character before it
                var segments = name.Split('.').Select(s => char.IsDigit(s, 0) ? ("_" + s) : s).ToArray();
                name = string.Join(".", segments);
            }

            return name;
        }

        /// <summary>
        /// Normalize path
        /// </summary>
        /// <param name="path">path to normalize</param>
        /// <returns>normalized string</returns>
        private static string NormalizeFullPath(string path)
        {
            path = path.Trim(' ', '.', '/', '\\');
            path = path.Replace('\\', '/');

            var folders = path.Split('/');
            for (int i = 0; i < folders.Length - 1; ++i)
            {
                folders[i] = NormalizeName(folders[i]);
            }

            var result = string.Join(".", folders);
            return result;
        }
    }
}
