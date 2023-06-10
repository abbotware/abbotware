// -----------------------------------------------------------------------
// <copyright file="BaseSqsClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon.Runtime;
    using global::Amazon.SQS;
    using global::Microsoft.Extensions.Logging;

    /// <summary>
    /// Base SQS client
    /// </summary>
    public abstract class BaseSqsClient : BaseComponent<ISqsSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSqsClient"/> class.
        /// </summary>
        /// <param name="client">aws sqs client</param>
        /// <param name="configuration">sqs config</param>
        /// <param name="logger">injected logger</param>
        protected BaseSqsClient(AmazonSQSClient client, ISqsSettings configuration, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(client, nameof(client));
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));

            this.Client = client;
        }

        /// <summary>
        /// Gets the AWS client
        /// </summary>
        protected AmazonSQSClient Client { get; }

        /// <summary>
        /// Gets the internal sync lock
        /// </summary>
        protected object Mutex { get; } = new object();

        /// <summary>
        /// Verifies the AWS service response is successful
        /// </summary>
        /// <param name="queue">queue name</param>
        /// <param name="action">action</param>
        /// <param name="response">AWS response object</param>
        protected static void VerifyActionResponse(string queue, string action, AmazonWebServiceResponse response)
        {
            response = Arguments.EnsureNotNull(response, nameof(response));

            if (IsSuccess(response))
            {
                return;
            }

            var meta = CreateMetaString(response);

            throw new InvalidOperationException($"action:{action} Queue:{queue} {meta}");
        }

        /// <summary>
        /// Verifies the AWS delete response is successful
        /// </summary>
        /// <param name="queue">queue name</param>
        /// <param name="receiptHandle">receipt handle</param>
        /// <param name="response">AWS response object</param>
        protected static void VerifyDeleteResponse(string queue, string receiptHandle, AmazonWebServiceResponse response)
        {
            response = Arguments.EnsureNotNull(response, nameof(response));

            if (IsSuccess(response))
            {
                return;
            }

            var meta = CreateMetaString(response);

            throw new InvalidOperationException($"receiptHandle:{receiptHandle} queue:{queue} Meta:{meta}");
        }

        /// <summary>
        /// Verifies the AWS service response is successful
        /// </summary>
        /// <param name="response">AWS response object</param>
        /// <returns>true if success</returns>
        protected static bool IsSuccess(AmazonWebServiceResponse response)
        {
            response = Arguments.EnsureNotNull(response, nameof(response));

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        private static string CreateMetaString(AmazonWebServiceResponse response)
        {
            var meta = response.ResponseMetadata.Metadata.StringFormat();

            var data = $"Status Code:{response.HttpStatusCode} Meta:{meta}";

            return data;
        }
    }
}
