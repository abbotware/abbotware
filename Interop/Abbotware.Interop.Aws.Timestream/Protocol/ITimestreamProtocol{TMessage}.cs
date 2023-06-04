// -----------------------------------------------------------------------
// <copyright file="ITimestreamProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using Abbotware.Core.Serialization;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol.Options;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// interface for Timestream message encoding protocol
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface ITimestreamProtocol<TMessage> : IEncode<TMessage, WriteRecordsRequest>, IBulkEncode<TMessage, WriteRecordsRequest>
    {
        /// <summary>
        /// Encodes a message
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <param name="options">timestream options</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage message, TimestreamOptions options);

        /// <summary>
        /// Encodes a message
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage message, TimeValueOptions<TMessage> timestamp);

        /// <summary>
        /// Encodes a message
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <param name="options">timestream options</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage message, TimestreamOptions options, TimeValueOptions<TMessage> timestamp);

        /// <summary>
        ///  Encodes a batch of messages
        /// </summary>
        /// <param name="messages">messages to encode</param>
        /// <param name="options">timestream options</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage[] messages, TimestreamOptions options);

        /// <summary>
        ///  Encodes a batch of messages
        /// </summary>
        /// <param name="messages">messages to encode</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage[] messages, TimeValueOptions<TMessage> timestamp);

        /// <summary>
        /// Encodes a batch of messages
        /// </summary>
        /// <param name="messages">messages to encode</param>
        /// <param name="options">timestream options</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage[] messages, TimestreamOptions options, TimeValueOptions<TMessage> timestamp);
    }
}
