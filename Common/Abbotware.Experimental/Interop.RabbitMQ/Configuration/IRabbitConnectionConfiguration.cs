// -----------------------------------------------------------------------
// <copyright file="IRabbitConnectionConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.Conifguration
{
    using System;
    using System.Net;
    using Abbotware.Core.Objects;
    using global::RabbitMQ.Client;

    /// <summary>
    /// Read only interface for RabbitMQ Configuration
    /// </summary>
    public interface IRabbitConnectionConfiguration : IConnectionOptions
    {
        /// <summary>
        /// Gets the amqp tcp endpoint
        /// </summary>
        AmqpTcpEndpoint AmqpTcpEndpoint { get; }

        /// <summary>
        /// Gets the connection timeout
        /// </summary>
        TimeSpan ConnectionTimeout { get; }

        /// <summary>
        /// Gets the heart beat timespan
        /// </summary>
        TimeSpan Heartbeat { get; }
    }
}