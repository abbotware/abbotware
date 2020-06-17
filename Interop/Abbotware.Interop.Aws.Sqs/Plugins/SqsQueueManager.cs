// -----------------------------------------------------------------------
// <copyright file="SqsQueueManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon.SQS;

    /// <summary>
    ///     Channel manager used for manage queue operations
    /// </summary>
    public class SqsQueueManager : BaseSqsClient, ISqsQueueManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SqsQueueManager" /> class.
        /// </summary>
        /// <param name="client">SQS client</param>
        /// <param name="configuration">injected configuration</param>
        /// <param name="logger">injected logger</param>
        public SqsQueueManager(AmazonSQSClient client, ISqsSettings configuration, ILogger logger)
            : base(client, configuration, logger)
        {
            Arguments.NotNull(client, nameof(client));
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc/>
        public async Task PurgeAsync(string queue)
        {
            var response = await this.Client.PurgeQueueAsync(queue).ConfigureAwait(false);

            VerifyActionResponse(queue, "purge", response);
        }
    }
}