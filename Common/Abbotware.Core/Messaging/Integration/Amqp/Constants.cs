// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp
{
    /// <summary>
    ///     Helper Class that contains important RabbitMQ constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     Special wildcard topic that means all topics
        /// </summary>
        public const string AllTopics = "#";

        /// <summary>
        ///     Name of the default exchange
        /// </summary>
        public static readonly string DefaultExchange = string.Empty;
    }
}