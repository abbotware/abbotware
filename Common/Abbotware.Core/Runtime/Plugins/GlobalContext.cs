// -----------------------------------------------------------------------
// <copyright file="GlobalContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Runtime.Plugins
{
    /// <summary>
    /// global runtime context of information
    /// </summary>
    public class GlobalContext : IGlobalContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalContext"/> class.
        /// </summary>
        /// <param name="environment">os specific environment information</param>
        /// <param name="operatingSystem">os specific information</param>
        /// <param name="process">process information</param>
        public GlobalContext(IEnvironmentInformation environment, IOperatingSystemInformation operatingSystem, IProcessInformation process)
        {
            this.Environment = environment;
            this.OperatingSystem = operatingSystem;
            this.Process = process;
        }

        /// <inheritdoc/>
        public IApplicationInformation Application { get; } = new ApplicationInformation();

        /// <inheritdoc/>
        public IEnvironmentInformation Environment { get; }

        /// <inheritdoc/>
        public IProcessInformation Process { get; }

        /// <inheritdoc/>
        public IOperatingSystemInformation OperatingSystem { get; }
    }
}
