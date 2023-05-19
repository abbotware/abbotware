// -----------------------------------------------------------------------
// <copyright file="ILambdaHostOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda.Configuration
{
    using System;
    using Abbotware.Host.Configuration;

    /// <summary>
    /// interface for lambda host options
    /// </summary>
    public interface ILambdaHostOptions : IHostOptions
    {
        /// <summary>
        /// Gets a value indicating whether or not to run as a regular console app
        /// </summary>
        bool RunAsConsole { get;  }

        /// <summary>
        /// Gets a value indicating whether or not to skip the lambda and warm up aws
        /// </summary>
        bool SpoolAws { get;  }

        /// <summary>
        /// Gets the time slice the lambda can run for
        /// </summary>
        public TimeSpan TimeSlice { get; }
    }
}