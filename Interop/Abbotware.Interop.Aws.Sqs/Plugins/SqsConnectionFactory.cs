// -----------------------------------------------------------------------
// <copyright file="SqsConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon;
    using global::Amazon.Runtime;
    using global::Amazon.SQS;

    /// <summary>
    /// Redis Connection Factory via StackExchange
    /// </summary>
    public class SqsConnectionFactory : BaseComponent, ISqsConnectionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqsConnectionFactory"/> class.
        /// </summary>
        /// <param name="defaultConfiguration">injected default configuration</param>
        /// <param name="logger">injected logger</param>
        public SqsConnectionFactory(ISqsSettings defaultConfiguration, ILogger logger)
            : base(logger)
        {
            this.DefaultOptions = defaultConfiguration;
        }

        /// <inheritdoc/>
        public ISqsSettings DefaultOptions { get; }

        /// <inheritdoc/>
        public ISqsConnection Create()
        {
            return this.OnCreate(this.DefaultOptions);
        }

        /// <inheritdoc/>
        public ISqsConnection Create(ISqsSettings configuration)
        {
            configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));

            return this.OnCreate(configuration);
        }

        /// <inheritdoc/>
        public void Destroy(ISqsConnection resource)
        {
            resource = Arguments.EnsureNotNull(resource, nameof(resource));

            resource.Dispose();
        }

        /// <summary>
        /// encasulates the create connection logic
        /// </summary>
        /// <param name="config">configuration options</param>
        /// <returns>redis connection</returns>
        private ISqsConnection OnCreate(ISqsSettings config)
        {
            var sqsConfig = new AmazonSQSConfig
            {
                ////sqsConfig.Timeout = TimeSpan.FromSeconds(5);

                RegionEndpoint = RegionEndpoint.GetBySystemName(config.Region),
            };

            var awsCreds = new BasicAWSCredentials(config.Username, config.Password);

            AmazonSQSClient temp = null;

            try
            {
                temp = new AmazonSQSClient(awsCreds, sqsConfig);

                var connection = new SqsConnection(temp, config, this.Logger);
                temp = null;

                return connection;
            }
            finally
            {
                temp?.Dispose();
            }
        }
    }
}
