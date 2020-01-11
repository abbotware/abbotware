// -----------------------------------------------------------------------
// <copyright file="BaseEnvironment.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    /// <summary>
    ///     base class for operating system specific environment information
    /// </summary>
    public abstract class BaseEnvironment : IEnvironment
    {
        /// <summary>
        /// Gets the Shutdown task
        /// </summary>
        public Task Shutdown => this.ShutdownSignal.Task;

        /// <inheritdoc />
        public bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;

        /// <inheritdoc />
        public string UserName => Environment.UserName;

        /// <inheritdoc />
        public string MachineName => Environment.MachineName;

        /// <inheritdoc />
        public int ProcessorCount => Environment.ProcessorCount;

        /// <inheritdoc />
        public long ProcessorFrequency => Stopwatch.Frequency;

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
        public abstract long SystemMemory { get; }

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

        /// <summary>
        /// Gets the shutdown signal
        /// </summary>
        protected TaskCompletionSource<bool> ShutdownSignal { get; } = new TaskCompletionSource<bool>();
    }
}