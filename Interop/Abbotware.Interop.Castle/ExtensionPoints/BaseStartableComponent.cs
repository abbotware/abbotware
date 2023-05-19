// -----------------------------------------------------------------------
// <copyright file="BaseStartableComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints
{
    using System;
    using System.Threading;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using global::Castle.Core;

    /// <summary>
    ///     base class for a startable component
    /// </summary>
    public abstract class BaseStartableComponent : BaseComponent, IStartable
    {
        /// <summary>
        ///     wait handle used to signify shutdown
        /// </summary>
        private readonly CancellationTokenSource cts = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseStartableComponent" /> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        protected BaseStartableComponent(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Gets the cancellation token for shutdown notification
        /// </summary>
        /// <returns>cancellation token</returns>
        protected CancellationToken CancellationToken => this.cts.Token;

        /// <inheritdoc />
        public void Start()
        {
            try
            {
                this.Logger.Info("Start Requested");

                this.OnStart();

                this.Logger.Info("Exiting Start");
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex, "Error Stopping:{0}", this.GetType());
                throw;
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            try
            {
                this.Logger.Info("Stop Requested");

                this.cts.Cancel();

                this.OnStop();

                this.Logger.Info("Exiting Stop");
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex, "Error Stopping:{0}", this.GetType());

                throw;
            }
        }

        /// <summary>
        ///     Hook to implement custom start logic
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        ///     Hook to implement custom stop logic
        /// </summary>
        protected virtual void OnStop()
        {
        }

        /// <inheritdoc/>
        protected sealed override void OnInitialize()
        {
            this.Start();
        }
    }
}