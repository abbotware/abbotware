// -----------------------------------------------------------------------
// <copyright file="ActiveInstanceCounter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     special class that can be used for counting instances of a specific type of an object. This will decrement when the
    ///     container class is disposed (and disposed is chained correctly)
    /// </summary>
    /// <remarks>
    ///     Use this class in composition with another class.
    /// </remarks>
    /// <typeparam name="TObjectType">type to use for counting</typeparam>
    public class ActiveInstanceCounter<TObjectType> : BaseComponent
    {
        /// <summary>
        ///     global static counter for tracking 'active' objects
        /// </summary>
        private static readonly AtomicCounter ActiveCounter = new();

        /// <summary>
        ///     global static counter
        /// </summary>
        private readonly TypeCreatedCounter<TObjectType> globalCounter = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActiveInstanceCounter{TObjectType}" /> class.
        /// </summary>
        /// <param name="logger">Injected logger for the class</param>
        public ActiveInstanceCounter(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));

            var count = ActiveCounter.Increment();

            this.Logger.Debug("Create - InstanceId: {0} Active:{0}", this.InstanceId, count);
        }

        /// <summary>
        ///     Gets the global count of tracked objects
        /// </summary>
        public static long GlobalActiveCount
        {
            get
            {
                return ActiveCounter.Value;
            }
        }

        /// <summary>
        ///     Gets the current id for this type
        /// </summary>
        public long InstanceId
        {
            get
            {
                return this.globalCounter.InstanceId;
            }
        }

        /// <summary>
        ///     Gets the global count of tracked objects via the static variable
        /// </summary>
        public long ActiveCount
        {
            get
            {
                return ActiveCounter.Value;
            }
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            ActiveCounter.Decrement();

            this.Logger.Debug("Dispose - InstanceId: {0} Active:{0}", this.InstanceId, this.ActiveCount);

            base.OnDisposeManagedResources();
        }
    }
}