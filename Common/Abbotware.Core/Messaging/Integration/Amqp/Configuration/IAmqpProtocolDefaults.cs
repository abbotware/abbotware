// -----------------------------------------------------------------------
// <copyright file="IAmqpProtocolDefaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Configuration
{
    /// <summary>
    ///     read only interface containing amqp message protocol default values
    /// </summary>
    public interface IAmqpProtocolDefaults
    {
        /// <summary>
        ///     Gets a value indicating whether Mandatory mode is the default for the protocol
        /// </summary>
        bool Mandatory { get; }

        /// <summary>
        ///     Gets the default exchange name for the protocol
        /// </summary>
        string Exchange { get; }

        /// <summary>
        ///     Gets the default exchange name for the protocol
        /// </summary>
        string Topic { get; }
    }
}