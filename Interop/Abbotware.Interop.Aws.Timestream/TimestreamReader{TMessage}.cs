// -----------------------------------------------------------------------
// <copyright file="TimestreamReader{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Abbotware.Core.Extensions;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Amazon.TimestreamQuery;
    using Amazon.TimestreamQuery.Model;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Typed Message Reader
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public partial class TimestreamReader<TMessage> : AwsConnection<AmazonTimestreamQueryClient, AmazonTimestreamQueryConfig, TimestreamOptions>
        where TMessage : notnull
    {
        /// <summary>
        /// Gets the Ilogger for the LoggerMessage
        /// </summary>
        /// <remarks>
        /// TODO: remove this when this is implemented correctly:
        /// https://github.com/dotnet/runtime/issues/87747
        /// </remarks>
        protected readonly ILogger doNotUse;

        private volatile int recordsReceieved;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamReader{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="protocol">message decoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamReader(TimestreamOptions options, ITimestreamQueryProtocol<TMessage> protocol, ILogger<TimestreamReader<TMessage>> logger)
            : this(new AmazonTimestreamQueryClient(), options, protocol, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestreamReader{TMessage}"/> class.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="options">options</param>
        /// <param name="protocol">message decoding protocol</param>
        /// <param name="logger">injected logger</param>
        public TimestreamReader(AmazonTimestreamQueryClient client, TimestreamOptions options, ITimestreamQueryProtocol<TMessage> protocol, ILogger logger)
            : base(client, options, logger)
        {
            this.doNotUse = logger;
            this.Protocol = protocol;
        }

        /// <summary>
        /// gets the count of records recieved
        /// </summary>
        public long RecordsReceieved => this.recordsReceieved;

        /// <summary>
        /// gets the protocol decoder
        /// </summary>
        public ITimestreamQueryProtocol<TMessage> Protocol { get; }

        /// <summary>
        /// Runs a query asynchronously and returns an IAsyncEnumerable
        /// </summary>
        /// <param name="query">query to run</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async enumerable of messages</returns>
        /// <exception cref="InvalidOperationException">an error occured during query running</exception>
        public async IAsyncEnumerable<TMessage> QueryAsync(string query, [EnumeratorCancellation] CancellationToken ct)
        {
            var request = new QueryRequest();
            request.QueryString = query;

            do
            {
                var response = await this.Client.QueryAsync(request, ct)
                    .ConfigureAwait(false);

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new InvalidOperationException($"query failed HTTP Status:{response.HttpStatusCode}");
                }

                using var handle = SetupCancellation(response, ct);

                foreach (var row in this.Protocol.Decode(response))
                {
                    Interlocked.Increment(ref this.recordsReceieved);
                    yield return row;
                }

                request.NextToken = response.NextToken;
            }
            while (request.NextToken.IsNotBlank());

            CancellationTokenRegistration SetupCancellation(QueryResponse response, CancellationToken ct)
            {
                var handle = ct.Register(() =>
                {
                    var cqr = new CancelQueryRequest();
                    cqr.QueryId = response.QueryId;

                    this.LogCancelRequest(cqr.QueryId);

                    //// TODO: observe errors if any
                    // it makes no sense to use a second cancellation token here, so we will not use:
                    _ = this.Client.CancelQueryAsync(cqr, default);
                });
                return handle;
            }
        }

        [LoggerMessage(Level = LogLevel.Information, Message = "Cancelling Request: {QueryId}")]
        private partial void LogCancelRequest(string queryId);
    }
}
