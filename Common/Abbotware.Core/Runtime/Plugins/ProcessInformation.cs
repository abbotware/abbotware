// -----------------------------------------------------------------------
// <copyright file="ProcessInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime.Plugins
{
    using System;
    using System.Diagnostics;
    using Abbotware.Core.Chrono;
    using Abbotware.Core.Objects;
    using Abbotware.Core.Runtime;

    /// <summary>
    /// Wrapper class for getting the current process stats
    /// </summary>
    public class ProcessInformation : BaseComponent, IProcessInformation
    {
        /// <summary>
        ///     handle to the process object
        /// </summary>
        private readonly Process process;

        /// <summary>
        ///     refresh time span for polling the process object
        /// </summary>
        private readonly MinimumTimeSpan refreshFrequency = new(TimeSpan.FromSeconds(1));

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInformation"/> class.
        /// </summary>
        public ProcessInformation()
        {
            this.process = Process.GetCurrentProcess();
        }

        /// <inheritdoc />
        public long VirtualMemorySize
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.VirtualMemorySize64;
            }
        }

        /// <inheritdoc />
        public long PrivateMemorySize
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.PrivateMemorySize64;
            }
        }

        /// <inheritdoc />
        public long PagedMemorySize
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.PagedMemorySize64;
            }
        }

        /// <inheritdoc />
        public long PagedSystemMemorySize
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.PagedSystemMemorySize64;
            }
        }

        /// <inheritdoc />
        public long NonpagedSystemMemorySize
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.NonpagedSystemMemorySize64;
            }
        }

        /// <inheritdoc />
        public TimeSpan Uptime
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return DateTime.Now - this.process.StartTime;
            }
        }

        /// <inheritdoc />
        public long WorkingSet
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.WorkingSet64;
            }
        }

        /// <inheritdoc />
        public int HandleCount
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.HandleCount;
            }
        }

        /// <inheritdoc />
        public int ThreadCount
        {
            get
            {
                this.RefreshProcessIfNeeded();
                return this.process.Threads.Count;
            }
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.process?.Dispose();

            base.OnDisposeManagedResources();
        }

        /// <summary>
        /// refreshes the process object if needed
        /// </summary>
        private void RefreshProcessIfNeeded()
        {
            if (this.refreshFrequency.IsExpired)
            {
                this.process.Refresh();
            }
        }
    }
}