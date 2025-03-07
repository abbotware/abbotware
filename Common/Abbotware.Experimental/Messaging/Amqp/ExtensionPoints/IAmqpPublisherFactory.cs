﻿// -----------------------------------------------------------------------
// <copyright file="IAmqpPublisherFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    /// interface for message publisher factory
    /// </summary>
    public interface IAmqpPublisherFactory
    {
        /// <summary>
        /// Creates a message publisher object
        /// </summary>
        /// <typeparam name="TMessage">message class type</typeparam>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="publishManager">publish manager</param>
        /// <returns>configured publisher object</returns>
        IMessagePublisher<TMessage> CreatePublisher<TMessage>(string exchangeName, IAmqpPublisher publishManager);
    }
}