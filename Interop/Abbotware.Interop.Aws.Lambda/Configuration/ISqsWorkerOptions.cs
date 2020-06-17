// -----------------------------------------------------------------------
// <copyright file="ISqsWorkerOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda
{
    using Abbotware.Core.Runtime;
    using Abbotware.Interop.Aws.Lambda.Configuration;
    using Abbotware.Interop.Aws.Sqs;
    using global::Amazon.Lambda.Core;

    /// <summary>
    /// read only configuration for sqs work host
    /// </summary>
    public interface ISqsWorkerOptions
    {
        /// <summary>
        /// Gets the lambda host options
        /// </summary>
        ILambdaHostOptions LambdaHostOptions { get; }

        /// <summary>
        /// Gets the monitor shutdown service
        /// </summary>
        IMonitorShutdown MonitorShutdown { get; }

        /// <summary>
        /// Gets the AWS lambda context
        /// </summary>
        ILambdaContext LambdaContext { get; }

        /// <summary>
        /// Gets the sqs connection factory
        /// </summary>
        ISqsConnectionFactory ConnectionFactory { get; }
    }
}