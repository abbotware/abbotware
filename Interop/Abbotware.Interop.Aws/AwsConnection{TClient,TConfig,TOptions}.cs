// -----------------------------------------------------------------------
// <copyright file="AwsConnection{TClient,TConfig,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws
{
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Amazon.Runtime;

    /// <summary>
    /// Base AWS Connection
    /// </summary>
    /// <typeparam name="TClient">AWS Client</typeparam>
    /// <typeparam name="TConfig">AWS Config</typeparam>
    /// <typeparam name="TOptions">Options</typeparam>
    public abstract class AwsConnection<TClient, TConfig, TOptions> : BaseComponent<TOptions>, IConnection
        where TClient : AmazonServiceClient
        where TOptions : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsConnection{TClient, TConfig, TOptions}"/> class.
        /// </summary>
        /// <param name="client">injected Amazon client </param>
        /// <param name="options">injected configuration</param>
        /// <param name="logger">injected logger</param>
        protected AwsConnection(TClient client, TOptions options, ILogger logger)
            : base(options, logger)
        {
            client = Arguments.EnsureNotNull(client, nameof(client));

            this.Client = client;

            this.Client.BeforeRequestEvent += this.OnRequestEventHandler;
            this.Client.AfterResponseEvent += this.OnAfterResponseEvent;
            this.Client.ExceptionEvent += this.OnExceptionEvent;
        }

        /// <inheritdoc />
        public bool IsOpen => true;

        /// <summary>
        /// Gets the AWS Client
        /// </summary>
        protected TClient Client { get; }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.Client.BeforeRequestEvent -= this.OnRequestEventHandler;
            this.Client.AfterResponseEvent -= this.OnAfterResponseEvent;
            this.Client.ExceptionEvent -= this.OnExceptionEvent;

            this.Client.Dispose();
        }

        private void OnExceptionEvent(object sender, ExceptionEventArgs e)
        {
            switch (e)
            {
                case WebServiceExceptionEventArgs wsee:
                    this.Logger.Error(wsee.Exception, "OnExceptionEvent");
                    break;
                default:
                    this.Logger.Error(e?.ToString() ?? "unknown exception");
                    break;
            }
        }

        private void OnAfterResponseEvent(object sender, ResponseEventArgs e)
        {
        }

        private void OnRequestEventHandler(object sender, RequestEventArgs e)
        {
        }
    }
}
