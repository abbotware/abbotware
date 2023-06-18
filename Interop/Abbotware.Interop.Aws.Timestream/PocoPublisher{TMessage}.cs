// -----------------------------------------------------------------------
// <copyright file="PocoPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Amazon.TimestreamWrite;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class PocoPublisher<TMessage> : TimestreamPublisher<TMessage>
        where TMessage : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PocoPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="logger">injected logger</param>
        public PocoPublisher(TimestreamOptions options, ILogger<PocoPublisher<TMessage>> logger)
            : base(new AmazonTimestreamWriteClient(), options, PocoProtocol.Build<TMessage>(logger), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="client">client </param>
        /// <param name="options">options</param>
        /// <param name="logger">injected logger</param>
        public PocoPublisher(AmazonTimestreamWriteClient client, TimestreamOptions options, ILogger<PocoPublisher<TMessage>> logger)
            : base(client, options, PocoProtocol.Build<TMessage>(logger), logger)
        {
        }
    }
}