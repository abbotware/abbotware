// -----------------------------------------------------------------------
// <copyright file="IProcessInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;

    /// <summary>
    ///     Interface for useful process information
    /// </summary>
    public interface IProcessInformation
    {
        /// <summary>
        ///     Gets the time span this process has been running
        /// </summary>
        TimeSpan Uptime { get; }

        /// <summary>
        ///     Gets the Process.WorkingSet64
        /// </summary>
        long WorkingSet { get; }

        /// <summary>
        ///     Gets the Process.VirtualMemorySize64
        /// </summary>
        long VirtualMemorySize { get; }

        /// <summary>
        ///     Gets the Process.PrivateMemorySize64
        /// </summary>
        long PrivateMemorySize { get; }

        /// <summary>
        ///     Gets the Process.PagedMemorySize64
        /// </summary>
        long PagedMemorySize { get; }

        /// <summary>
        ///     Gets the Process.PagedSystemMemorySize64
        /// </summary>
        long PagedSystemMemorySize { get; }

        /// <summary>
        ///     Gets the Process.NonpagedSystemMemorySize64
        /// </summary>
        long NonpagedSystemMemorySize { get; }

        /// <summary>
        ///     Gets the Process.HandleCount
        /// </summary>
        int HandleCount { get; }

        /// <summary>
        ///     Gets the Process.Threads.Count
        /// </summary>
        int ThreadCount { get; }
    }
}