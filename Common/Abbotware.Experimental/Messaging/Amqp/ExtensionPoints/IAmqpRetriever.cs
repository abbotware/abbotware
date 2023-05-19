// -----------------------------------------------------------------------
// <copyright file="IAmqpRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    ///     Interface that manages receiving messages via RabbitMQ Channel
    /// </summary>
    public interface IAmqpRetriever : IBasicRetriever, IAmqpAcknowledger
    {
    }
}