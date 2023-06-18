// -----------------------------------------------------------------------
// <copyright file="ITimestreamQueryProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol
{
    using Abbotware.Core.Serialization;
    using Amazon.TimestreamQuery.Model;

    /// <summary>
    /// interface for a protocol that decodes Timestream Query response messages
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface ITimestreamQueryProtocol<TMessage> : IDecode<TMessage, Row>, IBulkDecode<TMessage, QueryResponse>
        where TMessage : notnull
    {
    }
}
