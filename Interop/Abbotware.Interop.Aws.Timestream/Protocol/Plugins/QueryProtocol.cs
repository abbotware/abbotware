// -----------------------------------------------------------------------
// <copyright file="QueryProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream.Protocol.Plugins
{
    using System;
    using System.Collections.Generic;
    using Amazon.TimestreamQuery.Model;

    /// <summary>
    ///  Basic Query Protocol
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class QueryProtocol<TMessage> : ITimestreamQueryProtocol<TMessage>
        where TMessage : notnull
    {
        /// <inheritdoc/>
        public TMessage Decode(Row storage)
        {
            if (typeof(TMessage) == typeof(Row))
            {
                return (TMessage)(object)storage;
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<TMessage> Decode(QueryResponse storage)
        {
            foreach (var r in storage.Rows)
            {
                yield return this.Decode(r);
            }
        }
    }
}
