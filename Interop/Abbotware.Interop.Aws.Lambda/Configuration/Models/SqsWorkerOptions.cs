// -----------------------------------------------------------------------
// <copyright file="SqsWorkerOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda.Configuration.Models
{
    using Abbotware.Core.Runtime;
    using Abbotware.Interop.Aws.Sqs;
    using global::Amazon.Lambda.Core;

    /// <summary>
    /// worker configuration class
    /// </summary>
    public class SqsWorkerOptions : ISqsWorkerOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqsWorkerOptions"/> class.
        /// </summary>
        /// <param name="lambdaHostOptions">options</param>
        /// <param name="lambdaContext">aws lambda context</param>
        /// <param name="monitorShutdown">shutdown service</param>
        /// <param name="connectionFactory">connection factory</param>
        public SqsWorkerOptions(ILambdaHostOptions lambdaHostOptions, ILambdaContext lambdaContext, IMonitorShutdown monitorShutdown, ISqsConnectionFactory connectionFactory)
        {
            this.LambdaHostOptions = lambdaHostOptions;
            this.LambdaContext = lambdaContext;
            this.MonitorShutdown = monitorShutdown;
            this.ConnectionFactory = connectionFactory;
        }

        /// <inheritdoc/>
        public ILambdaHostOptions LambdaHostOptions { get; set; }

        /// <inheritdoc/>
        public ILambdaContext LambdaContext { get; set; }

        /// <inheritdoc/>
        public IMonitorShutdown MonitorShutdown { get; set; }

        /// <inheritdoc/>
        public ISqsConnectionFactory ConnectionFactory { get; set; }
    }
}