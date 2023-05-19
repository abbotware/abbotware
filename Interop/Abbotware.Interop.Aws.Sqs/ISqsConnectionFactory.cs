// -----------------------------------------------------------------------
// <copyright file="ISqsConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs
{
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Aws.Sqs.Configuration;

    /// <summary>
    /// Interface for an sqs connection factory
    /// </summary>
    public interface ISqsConnectionFactory : IConnectionFactory<ISqsConnection, ISqsSettings>
    {
    }
}