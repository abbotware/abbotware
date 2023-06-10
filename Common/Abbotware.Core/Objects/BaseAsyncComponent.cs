// -----------------------------------------------------------------------
// <copyright file="BaseAsyncComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    ///     Abstract base class for writing component-like objects
    /// </summary>
    public abstract partial class BaseAsyncComponent : BaseComponent, IAsyncComponent
    {
        private readonly object initializeAsyncLock = new();

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
            : base(logger)
        {
        }

        /// <summary>
        /// Gets the Cancellation token linked to object dispose
        /// </summary>
        protected CancellationTokenSource DisposeRequested { get; } = new CancellationTokenSource();

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

            return this.InitializeIfRequiredAsync(this.DisposeRequested.Token)
                .GetAwaiter().GetResult();
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
                if (this.initializeTask?.Exception != null)
                {
                    throw new AbbotwareException("Initialization Error", this.initializeTask.Exception);
                }

                return Task.FromResult(false);
            }

            lock (this.initializeAsyncLock)
            {
                if (!this.OnRequiresInitialization())
                {
                    if (this.initializeTask?.Exception != null)
                    {
                        throw new AbbotwareException("Initialization Error", this.initializeTask.Exception);
                    }

                    return Task.FromResult(false);
                }

                // check if another thread beat us to creating the init task
                if (this.initializeTask != null)
                {
                    // attach a new task<bool> (returning false) to continue when the init task is complete
                    return this.initializeTask.ContinueWith(
                        (x) => false,
                        ct,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.Default);
                }

                this.Logger.Debug($"Scheduling Async Initialization:{this.GetType().Name}");

                // asign the task, but don't await it (the caller can)
                this.SetInitializeTask(ct);

                // this is the only time the init task<bool> ever returns true
                // that means the caller was the one that triggered the init
                return this.initializeTask!.ContinueWith(
                    (x) =>
                    {
                        // set the initi flag to true for the above double checked lock optimizations
                        this.IsInitialized = true;
                        return true;
                    },
                    ct,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.Default);
            }
        }

        /// <summary>
        /// Merges the incomming cancellation token to the dispose requested token
        /// </summary>
        /// <param name="external">external token</param>
        /// <returns>merged cancellation token</returns>
        protected CancellationTokenSource MergeWithDisposeToken(CancellationToken external)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(this.DisposeRequested.Token, external);
        }

        /// <summary>
        /// Disabling Function - should not be called
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected override sealed void OnInitialize()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        protected sealed override void OnDisposeUnmanagedResources()
        {
            // do nothing
        }

        partial void SetInitializeTask(CancellationToken ct);
    }
}