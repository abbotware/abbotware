// -----------------------------------------------------------------------
// <copyright file="MessageProcessor{TM,TP,TC,TConnection,TConfig}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Plugins
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Messaging.ExtensionPoints;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Castle.ExtensionPoints;

    /// <summary>
    /// Wrapper class the encapsulates all message and consumer related objects into a single pattern
    /// </summary>
    /// <typeparam name="TM">Message type</typeparam>
    /// <typeparam name="TP">Protocol type</typeparam>
    /// <typeparam name="TC">Message Handling Consumer</typeparam>
    /// <typeparam name="TConnection">MQ Connection Type</typeparam>
    /// <typeparam name="TConfig">MQ Connection configuration</typeparam>
    public class MessageProcessor<TM, TP, TC, TConnection, TConfig> : BaseStartableComponent
        where TP : IMessageProtocol<TM>, new()
        where TC : MqConsumer<TM>
        where TConnection : IBasicConnection
    {
        /// <summary>
        /// injected connection factory
        /// </summary>
        private readonly IConnectionFactory<TConnection, TConfig> connectionFactory;

        /// <summary>
        /// injected consumer factory
        /// </summary>
        private readonly IMessageConsumerFactory consumerFactory;

        /// <summary>
        ///  injected connection configuration
        /// </summary>
        private readonly TConfig connectionConfiguration;

        /// <summary>
        ///    current connection
        /// </summary>
        private TConnection currentConnection;

        /// <summary>
        ///    current consumer
        /// </summary>
        private MqConsumer<TM> currentConsumer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProcessor{TMessage, TProtocol, TConsumer, TMQConnection, TMQConfiguration}"/> class.
        /// </summary>
        /// <param name="consumerFactory">injected consumer factory</param>
        /// <param name="connectionConfiguration">injected connection configuration</param>
        /// <param name="connectionFactory">injected connection factory</param>
        /// <param name="logger">injected logger</param>
        public MessageProcessor(IMessageConsumerFactory consumerFactory, TConfig connectionConfiguration, IConnectionFactory<TConnection, TConfig> connectionFactory, ILogger logger)
            : base(logger)
        {
            this.connectionConfiguration = connectionConfiguration;
            this.connectionFactory = connectionFactory;
            this.consumerFactory = consumerFactory;
        }

        /// <inheritdoc />
        protected override void OnStart()
        {
            this.currentConnection = this.connectionFactory.Create(this.connectionConfiguration);

            var c = this.currentConnection.CreateConsumer();

            var a = this.currentConnection.CreateRetriever();

            this.currentConsumer = this.consumerFactory.CreateConsumer<TC, TM>(a, new TP(), c);

            this.currentConsumer.Initialize();
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
            this.CleanUpCommon();

            base.OnStart();
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.CleanUpCommon();

            base.OnDisposeManagedResources();
        }

        private void CleanUpCommon()
        {
            this.currentConsumer?.Dispose();
            this.currentConnection?.Dispose();
        }
    }
}