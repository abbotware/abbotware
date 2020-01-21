// -----------------------------------------------------------------------
// <copyright file="ApplicationInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Helper class for getting the current application information
    /// </summary>
    public class ApplicationInformation : IApplicationInformation
    {
        /// <inheritdoc />
        public Version SoftwareVersion
        {
            get
            {
                var version = Assembly.GetEntryAssembly()
                    .GetName()
                    .Version;

                if (version == new Version(0, 0, 0, 0))
                {
                    version = Assembly.GetCallingAssembly()
                        .GetName()
                        .Version;
                }

                return version;
            }
        }

        /// <inheritdoc />
        public string AssemblyConfiguration
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();

                var attributes = assembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);

                if (attributes.Length == 0)
                {
                    return string.Empty;
                }

                var ac = (AssemblyConfigurationAttribute)attributes[0];

                return ac.Configuration;
            }
        }

        /// <inheritdoc />
        public Version RuntimeVersion
        {
            get
            {
                var v = RuntimeInformation.FrameworkDescription.Split(' ').Last();

                return new Version(v);
            }
        }

        /// <inheritdoc />
        public string RuntimeName
        {
            get
            {
                var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
                var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                int netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");

                if (netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2)
                {
                    return assemblyPath[netCoreAppIndex + 1];
                }

                return string.Empty;
            }
        }
    }
}