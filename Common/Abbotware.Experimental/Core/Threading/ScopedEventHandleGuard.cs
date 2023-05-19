// -----------------------------------------------------------------------
// <copyright file="ScopedEventHandleGuard.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Class that emulates RAII functionality around the System.Threading.EventWaitHandle
    /// </summary>
    public sealed class ScopedEventHandleGuard : BaseComponent
    {
        /// <summary>
        ///     reference to the mutex object the scoped guard is wrapping
        /// </summary>
        private readonly EventWaitHandle eventHandle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScopedEventHandleGuard" /> class.
        /// </summary>
        /// <param name="eventHandle">mutex object the scoped guard is wrapping</param>
        /// <param name="logger">injected logger</param>
        public ScopedEventHandleGuard(EventWaitHandle eventHandle, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(eventHandle, nameof(eventHandle));
            Arguments.NotNull(logger, nameof(logger));

            this.eventHandle = eventHandle;

            var result = this.eventHandle.Reset();

            if (!result)
            {
                throw new InvalidOperationException("Reset on event handle failed!");
            }
        }

        /// <inheritdoc />
        protected override void OnDisposeUnmanagedResources()
        {
            var result = this.eventHandle.Set();

            if (!result)
            {
                this.Logger.Warn("Set failed");
            }
        }
    }
}