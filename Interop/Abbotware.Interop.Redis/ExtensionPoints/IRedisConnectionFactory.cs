// -----------------------------------------------------------------------
// <copyright file="IRedisConnectionFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.ExtensionPoints
{
    using Abbotware.Core.Objects;

    /// <summary>
    /// Interface for a redis connection factory
    /// </summary>
    public interface IRedisConnectionFactory : IConnectionFactory<IRedisConnection, IConnectionOptions>
    {
    }
}