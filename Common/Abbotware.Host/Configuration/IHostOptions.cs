// -----------------------------------------------------------------------
// <copyright file="IHostOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host.Configuration
{
    using Abbotware.Core.Configuration;

    /// <summary>
    /// interface for host options
    /// </summary>
    public interface IHostOptions : IContainerOptions
    {
         /// <summary>
        ///     Gets a value indicating whether SSL verification is disabled
        /// </summary>
        bool DisableSslVerification { get; }

        /// <summary>
        ///     Gets a value indicating whether first change exceptions should be logged
        /// </summary>
        bool LogFirstChanceExceptions { get; }
    }
}