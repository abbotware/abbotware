// -----------------------------------------------------------------------
// <copyright file="IRabbitConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.ExtensionPoints
{
    using Abbotware.Core.Objects;
    using Abbotware.Interop.RabbitMQ.Conifguration;

    /// <summary>
    /// interface for rabbitmq connection factory
    /// </summary>
    public interface IRabbitConnectionFactory : IConnectionFactory<IRabbitConnection, IRabbitConnectionConfiguration>
    {
    }
}