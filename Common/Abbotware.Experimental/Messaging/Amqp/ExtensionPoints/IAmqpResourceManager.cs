// -----------------------------------------------------------------------
// <copyright file="IAmqpResourceManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    /// <summary>
    ///     Interface that can manage the creation of AMQP resources
    /// </summary>
    public interface IAmqpResourceManager : IAmqpExchangeManager, IAmqpQueueManager
    {
    }
}