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
        public TMessage Decode((Row Row, ColumnInfo ColumnInfo) storage)
        {
            if (typeof(TMessage) == typeof(Row))
            {
                return (TMessage)(object)storage;
            }

            return this.OnDecode(storage.Row, storage.ColumnInfo);
        }

        /// <inheritdoc/>
        public IEnumerable<TMessage> Decode(QueryResponse storage)
        {
            for (var i = 0; i < storage.Rows.Count; i++)
            {
                var r = storage.Rows[i];

                if (r is null)
                {
                    continue;
                }

                yield return this.Decode((r, storage.ColumnInfo[i]));
            }
        }

        /// <summary>
        /// Logic Hook to implement custom deseriailzation logic
        /// </summary>
        /// <param name="row">row data</param>
        /// <param name="columnInfo">column info data</param>
        /// <returns>TMessage</returns>
        /// <exception cref="NotImplementedException">error</exception>
        protected virtual TMessage OnDecode(Row row, ColumnInfo columnInfo)
        {
            foreach (var d in row.Data)
            {
            }

            throw new NotImplementedException();
        }
    }
}
