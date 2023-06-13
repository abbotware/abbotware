// -----------------------------------------------------------------------
// <copyright file="IMessageBatchPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for publishing objects of a single type in a batch
    /// </summary>
    /// <typeparam name="TMessage">type of message</typeparam>
    public interface IMessageBatchPublisher<TMessage> : IMessagePublisher<TMessage>, IMessagePublisher<IEnumerable<TMessage>>
    {
    }
}