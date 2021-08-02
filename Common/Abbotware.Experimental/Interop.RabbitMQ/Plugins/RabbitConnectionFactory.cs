// -----------------------------------------------------------------------
// <copyright file="RabbitConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System.Net.Sockets;
    using Abbotware.Core;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.RabbitMQ.Conifguration;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using global::RabbitMQ.Client;

    /// <summary>
    /// Redis Connection Factory via StackExchange
    /// </summary>
    public class RabbitConnectionFactory : BaseComponent, IRabbitConnectionFactory
    {
        /// <summary>
        ///     RabbitMQ ConnectionFactory to create the initial connection
        /// </summary>
        private readonly ConnectionFactory factory = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitConnectionFactory"/> class.
        /// </summary>
        /// <param name="defaultConfiguration">injected default configuration</param>
        public RabbitConnectionFactory(IRabbitConnectionConfiguration defaultConfiguration)
        {
            this.DefaultOptions = defaultConfiguration;
        }

        /// <inheritdoc/>
        public IRabbitConnectionConfiguration DefaultOptions { get; }

        /// <inheritdoc/>
        public IRabbitConnection Create()
        {
            return this.Create(this.DefaultOptions);
        }

        /// <inheritdoc/>
        public IRabbitConnection Create(IRabbitConnectionConfiguration configuration)
        {
            Arguments.NotNull(configuration, nameof(configuration));

            return this.OnCreate(configuration);
        }

        /// <inheritdoc/>
        public void Destroy(IRabbitConnection resource)
        {
            Arguments.NotNull(resource, nameof(resource));

            resource.Dispose();
        }

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        /// <summary>
        /// encasulates the create connection logic
        /// </summary>
        /// <param name="options">configuration options</param>
        /// <returns>redis connection</returns>
        private RabbitConnection OnCreate(IRabbitConnectionConfiguration options)
        {
            options = Arguments.EnsureNotNull(options, nameof(options));

            ////this.factory.SocketFactory = (x) =>
            ////{
            ////    using var socket = new AutoFactory<Socket>(() => new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));

            ////    return new TcpClient(AddressFamily.InterNetwork);
            ////};

            // Client Options
            {
                ////this.factory.Protocol = Protocols.DefaultProtocol;

                ////this.connectionFactory.ClientProperties;
            }

            // Security Options
            {
                ////this.connectionFactory.AuthMechanismFactory() ???
                this.factory.AuthMechanisms = ConnectionFactory.DefaultAuthMechanisms;

                ////this.connectionFactory.Ssl = new SslOption();
            }

            // Timeout Options
            {
                this.factory.RequestedConnectionTimeout = options.ConnectionTimeout;

                this.factory.SocketReadTimeout = options.ConnectionTimeout;

                this.factory.SocketWriteTimeout = options.ConnectionTimeout;

                ////this.connectionFactory.HandshakeContinuationTimeout; = 00:00:10

                ////this.connectionFactory.ContinuationTimeout; = 00:00:20
            }

            // Connection Recovery Options
            {
                ////this.connectionFactory.TopologyRecoveryEnabled = true;

                ////this.connectionFactory.AutomaticRecoveryEnabled = false

                ////this.connectionFactory.NetworkRecoveryInterval;   00:00:05

                ////this.connectionFactory.HostnameSelector = RabbitMQ.Client.Impl.RandomHostnameSelector;
            }

            // Connection EndPoint Related
            {
                ////this.connectionFactory.Endpoint = new AmqpTcpEndpoint("amqp://localhost:5672");

                ////this.connectionFactory.uri // no getter
                ////this.connectionFactory.Uri // no getter

                this.factory.VirtualHost = ConnectionFactory.DefaultVHost;

                this.factory.HostName = options.AmqpTcpEndpoint.HostName ?? "localhost";

                this.factory.Port = options.AmqpTcpEndpoint.Port;

                this.factory.UserName = options.Credential.UserName ?? ConnectionFactory.DefaultUser;

                this.factory.Password = options.Credential.Password ?? ConnectionFactory.DefaultPass;
            }

            this.factory.RequestedChannelMax = ConnectionFactory.DefaultChannelMax;

            this.factory.RequestedFrameMax = ConnectionFactory.DefaultFrameMax;

            this.factory.RequestedHeartbeat = options.Heartbeat;

            // Threading Related
            {
                this.factory.UseBackgroundThreadsForIO = false;

                ////this.connectionFactory.TaskScheduler = System.Threading.Tasks.ThreadPoolTaskScheduler;
            }

            var connection = this.factory.CreateConnection();

            ////this.connectionFactory.CreateConnection(hostnames:);

            return new RabbitConnection(connection, this.Logger);
        }
    }
}