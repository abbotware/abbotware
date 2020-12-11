// -----------------------------------------------------------------------
// <copyright file="BaseConnectionFactory{TConnection,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects.Internal;

    /// <summary>
    /// base class for classes that contain a logger and configuration
    /// </summary>
    /// <typeparam name="TConnection">Connection Type</typeparam>
    /// <typeparam name="TOptions">Connection Options Type</typeparam>
    public abstract class BaseConnectionFactory<TConnection, TOptions> : BaseLoggable, IConnectionFactory<TConnection, TOptions>
        where TConnection : IConnection
        where TOptions : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseConnectionFactory{TConnection,TOptions}" /> class.
        /// </summary>
        /// <param name="defaultOptions">injected default connection options</param>
        /// <param name="logger">injected logger</param>
        protected BaseConnectionFactory(TOptions defaultOptions, ILogger logger)
            : base(logger)
        {
            defaultOptions = Arguments.EnsureNotNull(defaultOptions, nameof(defaultOptions));

            this.DefaultOptions = defaultOptions;

            ObjectHelper.LogConfiguration(defaultOptions, logger);
        }

        /// <inheritdoc/>
        public TOptions DefaultOptions { get; }

        /// <inheritdoc/>
        public abstract TConnection Create();

        /// <inheritdoc/>
        public abstract TConnection Create(TOptions configuration);

        /// <inheritdoc/>
        public abstract void Destroy(TConnection resource);
    }
}