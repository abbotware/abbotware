// -----------------------------------------------------------------------
// <copyright file="BaseComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using Abbotware.Core.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    ///     Abstract base class for writing component-like objects
    /// </summary>
    public abstract class BaseComponent : BaseLoggable, IComponent
    {
#if DEBUG
        /// <summary>
        ///     call stack when this object was initialized.  Useful for debugging
        /// </summary>
        private readonly StackTrace debugCallStack = new();
#endif

        /// <summary>
        ///     Sync object for thread synchronization
        /// </summary>
        private readonly object initializeSyncLock = new();

        private volatile bool isInitialized;

        /// <summary>
        ///     current dispose state of the object
        /// </summary>
        private DisposeState disposeState = new(DisposeState.State.Initial);

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseComponent" /> class.
        /// </summary>
        protected BaseComponent()
            : this(NullLogger.Instance)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseComponent" /> class.
        /// </summary>
        /// <param name="logger">Injected logger for the class</param>
        protected BaseComponent(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="BaseComponent" /> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        ~BaseComponent()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether the object has been initialized
        /// </summary>
        public bool IsInitialized
        {
            get => this.isInitialized;
            private protected set => this.isInitialized = value;
        }

        /// <summary>
        /// Gets a value indicating whether the object has been disposed
        /// </summary>
        public bool IsDisposed => this.IsDisposedOrDisposing;

        /// <summary>
        ///     Gets a value indicating whether or not the object is disposed, or is currently being disposed
        /// </summary>
        protected bool IsDisposedOrDisposing
        {
            get
            {
                return this.disposeState.Value != DisposeState.State.Initial;
            }
        }

        /// <inheritdoc/>
        public bool Initialize()
        {
            return this.InitializeIfRequired();
        }

        /// <summary>
        ///     Disposes the Disposable Object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// public methods guard function which throws if the object has already been disposed
        /// </summary>
        /// <param name="method">compiler supplied member name</param>
        protected void ThrowIfDisposed([CallerMemberName] string? method = null)
        {
            if (this.IsDisposedOrDisposing)
            {
                throw new ObjectDisposedException($"Can not call method:{method} after object is disposed");
            }
        }

        /// <summary>
        /// public methods guard function which throws if the object has already been initialized
        /// </summary>
        /// <param name="method">compiler supplied member name</param>
        protected void ThrowIfInitialized([CallerMemberName] string? method = null)
        {
            if (this.IsInitialized)
            {
                throw new InvalidOperationException($"Can not call method:{method} after initialized");
            }
        }

        /// <summary>
        ///     Guard method to put in all public methods in the derived class
        /// </summary>
        /// <returns>true if initialization was run</returns>
        protected virtual bool InitializeIfRequired()
        {
            this.ThrowIfDisposed();

            if (!this.OnRequiresInitialization())
            {
                return false;
            }

            lock (this.initializeSyncLock)
            {
                if (!this.OnRequiresInitialization())
                {
                    return false;
                }

                try
                {
                    this.Logger.Debug($"Initializing:{this.GetType().Name}");

                    this.OnInitialize();
                }
                catch (Exception)
                {
                    // for debugging break points
                    throw;
                }

                this.IsInitialized = true;

                return true;
            }
        }

        /// <summary>
        /// helper method to use for inline properties that require initialization
        /// </summary>
        /// <remarks>
        ///  private Type property;
        ///  public Type Property => PropertyInitializeIfRequired(() => this.property);
        /// </remarks>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="func">callback</param>
        /// <returns>the </returns>
        protected T PropertyInitializeIfRequired<T>(Func<T> func)
        {
            Arguments.EnsureNotNull(func, nameof(func));

            this.InitializeIfRequired();

            return func();
        }

        /// <summary>
        ///     Hook to implement custom logic that disposes unmanaged resources
        /// </summary>
        protected virtual void OnDisposeUnmanagedResources()
        {
        }

        /// <summary>
        ///     Hook to implement custom logic that disposes managed resources
        /// </summary>
        protected virtual void OnDisposeManagedResources()
        {
        }

        /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        protected virtual void OnInitialize()
        {
        }

        /// <summary>
        /// callback hook for initialization check logic
        /// </summary>
        /// <returns>true if the component should initialize</returns>
        protected virtual bool OnRequiresInitialization()
        {
            return !this.IsInitialized;
        }

        /// <summary>
        ///     Dispose method
        /// </summary>
        /// <param name="disposing">flag to indicate whether a user or the Finalizer called disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            var current_state = this.disposeState.CompareAndSwap(DisposeState.State.Initial, DisposeState.State.Disposing);

            if (current_state != DisposeState.State.Initial)
            {
                // either it's disposed already, or in the process of being disposed by some other thread
                return;
            }

            try
            {
                // called by the finalizer
                if (!disposing)
                {
#if !DEBUG
                    var str = string.Format(CultureInfo.InvariantCulture, "this object:{0} was not disposed! Allocation Stack:(Not Available in Release build)", this.GetType());
#else
                    var str = string.Format(CultureInfo.InvariantCulture, "this object:{0} was allocated here:{1} and should be disposed instead of finalized", this.GetType(), this.debugCallStack);
#endif
                    Debug.WriteLine(str);

                    this.Logger?.Warn(str);
                }

                if (disposing)
                {
                    this.OnDisposeManagedResources();
                }

                this.OnDisposeUnmanagedResources();
            }
            finally
            {
                this.disposeState.Swap(DisposeState.State.Disposed);
            }
        }
    }
}