// -----------------------------------------------------------------------
// <copyright file="IRegistration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Microsoft
{
    using global::Microsoft.Extensions.Configuration;
    using global::Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency registration interface (ASP Net Core / .Net Core)
    /// </summary>
    public interface IRegistration
    {
        /// <summary>
        /// Configure / Register dependencies
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="services">services collection</param>
        void Register(IConfiguration configuration, IServiceCollection services);
    }
}