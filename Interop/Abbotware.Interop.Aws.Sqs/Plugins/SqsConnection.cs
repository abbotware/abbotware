// -----------------------------------------------------------------------
// <copyright file="SqsConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon.SQS;
    using global::Microsoft.Extensions.Logging;

    /// <summary>
    /// SQS Connection
    /// </summary>
    public class SqsConnection : BaseComponent, ISqsConnection
    {
        private readonly ILoggerFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqsConnection"/> class.
        /// </summary>
        /// <param name="client">injected Amazon client </param>
        /// <param name="configuration">injected configuration</param>
        /// <param name="factory">injected logger factory</param>
        public SqsConnection(AmazonSQSClient client, ISqsSettings configuration, ILoggerFactory factory)
            : base(factory.CreateLogger<SqsConnection>())
        {
            client = Arguments.EnsureNotNull(client, nameof(client));
            this.factory = Arguments.EnsureNotNull(factory, nameof(factory));

            this.Client = client;
            this.Configuration = configuration;

            this.Client.BeforeRequestEvent += this.OnRequestEventHandler;
            this.Client.AfterResponseEvent += this.OnAfterResponseEvent;
            this.Client.ExceptionEvent += this.OnExceptionEvent;
        }

        /// <inheritdoc />
        public bool IsOpen => true;

        /// <summary>
        /// Gets the AWS SQS Client
        /// </summary>
        protected AmazonSQSClient Client { get; }

        /// <summary>
        /// Gets the SQS configuration
        /// </summary>
        protected ISqsSettings Configuration { get; }

        /// <inheritdoc />
        public IBasicPublisher CreatePublisher()
        {
            return new SqsPublisher(this.Client, this.Configuration, this.factory.CreateLogger("SqsPublisher"));
        }

        /// <inheritdoc />
        public TPublisher CreatePublisher<TPublisher>()
            where TPublisher : class, IBasicPublisher
        {
            Arguments.TypesAreSame<TPublisher, SqsPublisher>();

            return (TPublisher)this.CreatePublisher();
        }

        /// <inheritdoc />
        public IBasicRetriever CreateRetriever()
        {
            return new SqsRetriever(this.Client, this.Configuration, this.factory.CreateLogger("SqsRetriever"));
        }

        /// <inheritdoc />
        public TRetriever CreateRetriever<TRetriever>()
            where TRetriever : class, IBasicRetriever
        {
            Arguments.TypesAreSame<TRetriever, SqsRetriever>();

            return (TRetriever)this.CreateRetriever();
        }

        /// <inheritdoc />
        public ISqsQueueManager CreateQueueManager()
        {
            return new SqsQueueManager(this.Client, this.Configuration, this.factory.CreateLogger("SqsQueueManager"));
        }

        /// <inheritdoc />
        public IBasicConsumer CreateConsumer()
        {
            return new SqsConsumer(this.CreateRetriever<SqsRetriever>(), this.factory.CreateLogger("SqsConsumer"));
        }

        /// <inheritdoc />
        public TConsumer CreateConsumer<TConsumer>()
            where TConsumer : class, IBasicConsumer
        {
            Arguments.TypesAreSame<TConsumer, SqsConsumer>();

            return (TConsumer)this.CreateConsumer();
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.Client.BeforeRequestEvent -= this.OnRequestEventHandler;
            this.Client.AfterResponseEvent -= this.OnAfterResponseEvent;
            this.Client.ExceptionEvent -= this.OnExceptionEvent;

            this.Client.Dispose();
        }

        private void OnExceptionEvent(object sender, global::Amazon.Runtime.ExceptionEventArgs e)
        {
            this.Logger.Error($"Sender:{sender} ExceptionEventArgs:{e}");
        }

        private void OnAfterResponseEvent(object sender, global::Amazon.Runtime.ResponseEventArgs e)
        {
        }

        private void OnRequestEventHandler(object sender, global::Amazon.Runtime.RequestEventArgs e)
        {
        }
    }
}
