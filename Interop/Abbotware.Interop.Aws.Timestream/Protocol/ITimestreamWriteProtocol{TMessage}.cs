// -----------------------------------------------------------------------
// <copyright file="ITimestreamWriteProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using System.Collections.Generic;
    using Abbotware.Core.Serialization;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol.Internal;
    using Amazon.TimestreamWrite.Model;

    /// <summary>
    /// interface for a protocol that encodes Timestream Write request messages
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface ITimestreamWriteProtocol<TMessage> : IEncode<TMessage, WriteRecordsRequest>, IBulkEncode<TMessage, WriteRecordsRequest>
        where TMessage : notnull
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
        WriteRecordsRequest Encode(TMessage message, IRecordUpdater<TMessage> timestamp);

        /// <summary>
        /// Encodes a message
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <param name="options">timestream options</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(TMessage message, TimestreamOptions options, IRecordUpdater<TMessage> timestamp);

        /// <summary>
        ///  Encodes a batch of messages
        /// </summary>
        /// <param name="messages">messages to encode</param>
        /// <param name="options">timestream options</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(IEnumerable<TMessage> messages, TimestreamOptions options);

        /// <summary>
        ///  Encodes a batch of messages
        /// </summary>
        /// <param name="messages">messages to encode</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(IEnumerable<TMessage> messages, IRecordUpdater<TMessage> timestamp);

        /// <summary>
        /// Encodes a batch of messages
        /// </summary>
        /// <param name="messages">messages to encode</param>
        /// <param name="options">timestream options</param>
        /// <param name="timestamp">timestamp lookup function</param>
        /// <returns>encoded write request</returns>
        WriteRecordsRequest Encode(IEnumerable<TMessage> messages, TimestreamOptions options, IRecordUpdater<TMessage> timestamp);
    }
}
