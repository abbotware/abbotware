// -----------------------------------------------------------------------
// <copyright file="ChannelMode.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    /// <summary>
    ///     Determines what mode a RabbitMQ channel operates in
    /// </summary>
    public enum ChannelMode
    {
        /// <summary>
        ///     no mode specified (default)
        /// </summary>
        None = 0,

        /// <summary>
        ///     Enable confirmations via Acknowledgements
        /// </summary>
        ConfirmationMode,

        /// <summary>
        ///     Enable Transaction Mode
        /// </summary>
        TransactionMode,
    }
}