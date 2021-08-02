// -----------------------------------------------------------------------
// <copyright file="RabbitConnectionConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Conifguration.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using Abbotware.Core.Configuration;
    using global::RabbitMQ.Client;

    /// <summary>
    ///     Configuration class for ConnectionManager
    /// </summary>
    public class RabbitConnectionConfiguration : BaseOptions, IRabbitConnectionConfiguration
    {
        /// <summary>
        ///     Default user/pass for RabbitMQ Server
        /// </summary>
        public static readonly NetworkCredential DefaultCredentials = new (ConnectionFactory.DefaultUser, ConnectionFactory.DefaultPass);

        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitConnectionConfiguration" /> class.
        /// </summary>
        /// <param name="endpoint">connection endpoint</param>
        public RabbitConnectionConfiguration(Uri endpoint)
            : this(new AmqpTcpEndpoint(endpoint))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitConnectionConfiguration" /> class.
        /// </summary>
        /// <param name="endpoint">connection endpoint</param>
        public RabbitConnectionConfiguration(AmqpTcpEndpoint endpoint)
            : this(DefaultCredentials, endpoint)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitConnectionConfiguration" /> class.
        /// </summary>
        /// <param name="credentials">connection user/pass</param>
        /// <param name="endpoint">connection endpoint</param>
        public RabbitConnectionConfiguration(NetworkCredential credentials, AmqpTcpEndpoint endpoint)
        {
            this.AmqpTcpEndpoint = endpoint;
            this.Credential = credentials;
        }

        /// <summary>
        ///     Gets the user credentials
        /// </summary>
        public NetworkCredential Credential { get; private set; }

        /// <summary>
        /// Gets the connection  endpoint
        /// </summary>
        public Uri Endpoint => new (this.AmqpTcpEndpoint.ToString());

        /// <summary>
        ///     Gets the AMQP endpoint
        /// </summary>
        public AmqpTcpEndpoint AmqpTcpEndpoint { get; private set; }

        /// <summary>
        ///     Gets the heartbeat time interval
        /// </summary>
        public TimeSpan ConnectionTimeout { get; } = ConnectionFactory.DefaultConnectionTimeout;

        /// <summary>
        ///     Gets the heartbeat time interval
        /// </summary>
        public TimeSpan Heartbeat { get; } = ConnectionFactory.DefaultHeartbeat;
    }
}