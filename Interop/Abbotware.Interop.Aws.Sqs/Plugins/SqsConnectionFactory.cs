// -----------------------------------------------------------------------
// <copyright file="SqsConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using global::Amazon;
    using global::Amazon.Runtime;
    using global::Amazon.SQS;
    using global::Microsoft.Extensions.Logging;

    /// <summary>
    /// Redis Connection Factory via StackExchange
    /// </summary>
    public class SqsConnectionFactory : BaseComponent, ISqsConnectionFactory
    {
        private readonly ILoggerFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqsConnectionFactory"/> class.
        /// </summary>
        /// <param name="defaultConfiguration">injected default configuration</param>
        /// <param name="factory">injected logger factory</param>
        public SqsConnectionFactory(ISqsSettings defaultConfiguration, ILoggerFactory factory)
            : base(factory.CreateLogger<SqsConnectionFactory>())
        {
            this.factory = Arguments.EnsureNotNull(factory, nameof(factory));

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

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.factory.Dispose();

            base.OnDisposeManagedResources();
        }

        /// <summary>
        /// encasulates the create connection logic
        /// </summary>
        /// <param name="config">configuration options</param>
        /// <returns>redis connection</returns>
        private SqsConnection OnCreate(ISqsSettings config)
        {
            var sqsConfig = new AmazonSQSConfig
            {
                ////sqsConfig.Timeout = TimeSpan.FromSeconds(5);

                RegionEndpoint = RegionEndpoint.GetBySystemName(config.Region),
            };

            var awsCreds = new BasicAWSCredentials(config.Username, config.Password);

            AmazonSQSClient? temp = null;

            try
            {
                temp = new AmazonSQSClient(awsCreds, sqsConfig);

                var connection = new SqsConnection(temp, config, this.factory);
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
