// -----------------------------------------------------------------------
// <copyright file="BaseStartableComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
        private readonly ManualResetEventSlim cancelNotification = new ManualResetEventSlim(false);

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseStartableComponent" /> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        protected BaseStartableComponent(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
        }

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

                this.cancelNotification.Set();

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
        ///     Blocks until stop has been requested, or returns true indicating it ok to continue running
        /// </summary>
        /// <param name="pollTimeSpan">timespan to wait for notification</param>
        /// <returns>false if component should shutdown, else it can still run</returns>
        protected bool ShouldContinue(TimeSpan pollTimeSpan)
        {
            return !this.cancelNotification.WaitHandle.WaitOne(pollTimeSpan);
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