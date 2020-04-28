// -----------------------------------------------------------------------
// <copyright file="BaseAsyncComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Logging.Plugins;

    /// <summary>
    ///     Abstract base class for writing component-like objects
    /// </summary>
    public abstract class BaseAsyncComponent : BaseComponent, IAsyncComponent
    {
        private readonly object initializeAsyncSyncLock = new object();

        private Task? initializeTask;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAsyncComponent" /> class.
        /// </summary>
        protected BaseAsyncComponent()
            : this(NullLogger.Instance)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAsyncComponent" /> class.
        /// </summary>
        /// <param name="logger">Injected logger for the class</param>
        protected BaseAsyncComponent(ILogger logger)
            : this(logger, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAsyncComponent" /> class.
        /// </summary>
        /// <param name="logger">Injected logger for the class</param>
        /// <param name="useDefaultLogStatement">use default log statement</param>
        protected BaseAsyncComponent(ILogger logger, bool useDefaultLogStatement)
            : base(logger, useDefaultLogStatement)
        {
        }

        /// <inheritdoc/>
        public Task<bool> InitializeAsync(CancellationToken ct)
        {
            return this.InitializeIfRequiredAsync(ct);
        }

        /// <summary>
        ///     Guard method to put in all public methods in the derived class
        /// </summary>
        /// <returns>true if initialization was run</returns>
        protected override bool InitializeIfRequired()
        {
            this.ThrowIfDisposed();

            if (!this.OnRequiresInitialization())
            {
                return false;
            }

            return this.InitializeIfRequiredAsync(default).GetAwaiter().GetResult();
        }

        /// <summary>
        ///     Guard method to put in all public methods in the derived class
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>true if initialization was run</returns>
        protected Task<bool> InitializeIfRequiredAsync(CancellationToken ct)
        {
            this.ThrowIfDisposed();

            if (!this.OnRequiresInitialization())
            {
                return Task.FromResult(false);
            }

            lock (this.initializeAsyncSyncLock)
            {
                if (!this.OnRequiresInitialization())
                {
                    return Task.FromResult(false);
                }

                // check if initialization is still 'running'
                if (this.initializeTask != null)
                {
                    return this.initializeTask.ContinueWith((x) => false, ct, TaskContinuationOptions.None, TaskScheduler.Default);
                }
            }

            this.Logger.Debug($"Scheduling Async Initialization:{this.GetType().Name}");

            // asign the task, but don't await it (the caller can)
            this.initializeTask = this.OnInitializeAsync(ct);

            // this is the only time the init task<bool> ever returns true
            // that means the caller was the one that triggered the init
            return this.initializeTask.ContinueWith(
                (x) =>
                {
                    this.IsInitialized = true;
                    return true;
                },
                ct,
                TaskContinuationOptions.None,
                TaskScheduler.Default);
        }

        /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task handle</returns>
        protected virtual Task OnInitializeAsync(CancellationToken ct)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Disabling Function - should not be called
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected override sealed void OnInitialize()
        {
            throw new NotSupportedException();
        }
    }
}