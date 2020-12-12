// -----------------------------------------------------------------------
// <copyright file="ExchangeType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    /// <summary>
    ///     The Type of the AMQP exchange
    /// </summary>
    public enum ExchangeType
    {
        /// <summary>
        ///     Topic Exchange (topic pattern matching via */#)
        /// </summary>
        Topic,

        /// <summary>
        ///     Header Exchange (ignores topic)
        /// </summary>
        Header,

        /// <summary>
        ///     Fanout Exchange (all messages are routed regardless of topic)
        /// </summary>
        Fanout,

        /// <summary>
        ///     Direct Exchange
        /// </summary>
        Direct,
    }
}